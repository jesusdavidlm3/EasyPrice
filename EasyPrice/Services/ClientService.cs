using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using EasyPrice.Utils;
using Models;

namespace EasyPrice.Services;

public static class ClientService
{
    private static HttpClient _client = new HttpClient();
    private static Timer? _timer;

    static ClientService()
    {
        if (SettingsService.Settings.ConnPassword != null)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(SettingsService.Settings.ConnPassword);
        }
    }

    public static async Task<bool> Login(string serverIp, string password)
    {
        var addressParam = $"http://{serverIp}/login";
        var response = await _client.PostAsJsonAsync(addressParam, new {Password = password});
        if (response.IsSuccessStatusCode)
        {
            SettingsService.UpdateConnPassword(password);
            SettingsService.UpdateServerIp(serverIp);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(SettingsService.Settings.ConnPassword!);
            return true;
        }
        return false;
    }

    public static async Task<List<Product>> GetProducts(DateTime? date = null)
    {
        var addressParam = $"http://{SettingsService.Settings.ServerIp}/product";
        if (date != null)
        {
            var dateString = $"{date.Value.Year}-{date.Value.Month}-{date.Value.Day} {date.Value.Hour}:{date.Value.Minute}:{date.Value.Second}";
            addressParam = $"{addressParam}/{dateString}";
        }
        var response = await _client.GetAsync(addressParam);
        if (response.IsSuccessStatusCode)
        {
            var res = response.Content.ReadFromJsonAsync<List<Product>>().Result;
            return res ?? new List<Product>();
        }
        else
        {
            throw new Exception("Error");
        }
    }

    public static async Task DeleteProduct(Guid productId)
    {
        var addressParam = $"http://{SettingsService.Settings.ServerIp}/product/{productId}";
        var response = await _client.DeleteAsync(addressParam);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Error");
        }
    }

    public static async Task<Product> CreateProduct(Product product)
    {
        var addressParam = $"http://{SettingsService.Settings.ServerIp}/product";
        var response = await _client.PostAsJsonAsync(addressParam, product);
        if (response.IsSuccessStatusCode)
        {
            Product? resProduct = response.Content.ReadFromJsonAsync<Product>().Result;
            return resProduct!;
        }
        else
        {
            throw new Exception("Error");
        }
    }

    public static async Task UpdateProduct(Product product)
    {
        var addressParam = $"http://{SettingsService.Settings.ServerIp}/product";
        var response = await _client.PatchAsJsonAsync(addressParam, product);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Error");
        }
    }

    public static async Task GetDollar()
    {
        var addressParam = $"http://{SettingsService.Settings.ServerIp}/dollar";
        var response = await _client.GetAsync(addressParam);
        if (response.IsSuccessStatusCode)
        {   
            var receivedValue = response.Content.ReadFromJsonAsync<double>().Result;
            if (!receivedValue.Equals(SettingsService.Settings.DolarPrice))
            {
                SettingsService.UpdateDolarPrice(receivedValue);
            }
        }
        else
        {
            throw new Exception("Error");
        }
    }

    public static async Task UpdateDollar(double newDollarPrice)
    {
        if(newDollarPrice.Equals(SettingsService.Settings.DolarPrice))
            return;
        
        var addressParam = $"http://{SettingsService.Settings.ServerIp}/dollar";
        var response = await _client.PatchAsJsonAsync(addressParam, new { DolarPrice = newDollarPrice });
        if (response.IsSuccessStatusCode)
        {
            SettingsService.UpdateDolarPrice(newDollarPrice);
        }
        else
        {
            throw new Exception("Error");
        }
    }

    public static async Task UpdateConnPassword(string newPassword)
    {
        var encryptedPassword = Crypto.Hash(newPassword);
        if (encryptedPassword.Equals(SettingsService.Settings.ConnPassword))
            return;
        
        var addressParams = $"http://{SettingsService.Settings.ServerIp}/password/conn";
        var response = await _client.PatchAsJsonAsync(addressParams, new{password = encryptedPassword});
        if (response.IsSuccessStatusCode)
        {
            SettingsService.UpdateConnPassword(encryptedPassword);
        }
        else
        {
            throw new Exception("Error");
        }
    }

    public static async Task UpdateAdminPassword(string newPassword)
    {
        var encryptedPassword = Crypto.Hash(newPassword);
        if (encryptedPassword.Equals(SettingsService.Settings.AdminPassword))
            return;
        
        var addressParams = $"http://{SettingsService.Settings.ServerIp}/password/admin";
        var response = await _client.PatchAsJsonAsync(addressParams, new{password = encryptedPassword});
        if (response.IsSuccessStatusCode)
        {
            SettingsService.UpdateAdminPassword(encryptedPassword);
        }
        else
        {
            throw new Exception("Error");
        }
    }

    public static void StartServerWatcher(Action<List<Product>> processUpdates)
    {
        /*_timer = new Timer(ServerWatcher!, processUpdates, 0, 600000);*/
        _timer = new Timer(ServerWatcher!, processUpdates, 0, 1500);
    }

    public static void StopServerWatcher()
    {
        _timer?.Dispose();
    }
    
    private static async void ServerWatcher(object state)
    {
        try
        {
            await GetDollar();
            var products = await GetProducts(SettingsService.Settings.LastUpdate);
            if (products.Count > 0)
            {
                SettingsService.UpdateLastUpdate(DateTime.Now);
                var a = (Action<List<Product>>)state;
                a(products);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}