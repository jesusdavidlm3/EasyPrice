using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using EasyPrice.Utils;
using Models;

namespace EasyPrice.Services;

public static class ClientService
{
    private static HttpClient _client = new HttpClient();
    private static Timer? _timer;
    private static Action<List<Product>> _timerCallback;

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
            var urlDate = date.Value.Ticks;
            /*var urlDate = date.Value.ToString("dd-MM-yyyy HH:mm:sszz");*/
            /*var dateString = $"{date.Value.Year}-{date.Value.Month}-{date.Value.Day} {date.Value.Hour}:{date.Value.Minute}:{date.Value.Second}Z";*/
            addressParam = $"{addressParam}/{urlDate}";
        }
        var response = await _client.GetAsync(addressParam);
        if (response.IsSuccessStatusCode)
        {
            var res = await response.Content.ReadFromJsonAsync<List<Product>>();
            return res ?? new List<Product>();
        }
        else
        {
            throw new Exception("Error");
        }
    }

    public static async Task<DateTime> DeleteProduct(Product product)
    {
        var addressParam = $"http://{SettingsService.Settings.ServerIp}/product/{product.Id}";
        var response = await _client.DeleteAsync(addressParam);
        if (response.IsSuccessStatusCode)
        {
            var res = await response.Content.ReadFromJsonAsync<DateTime>();
            return res;
        }
        else
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
            Product resProduct = (await response.Content.ReadFromJsonAsync<Product>())!;
            return resProduct;
        }
        else
        {
            throw new Exception("Error");
        }
    }

    public static async Task<Product> UpdateProduct(Product product)
    {
        var addressParam = $"http://{SettingsService.Settings.ServerIp}/product";
        var response = await _client.PatchAsJsonAsync(addressParam, product);
        if (response.IsSuccessStatusCode)
        {
            Product res = (await response.Content.ReadFromJsonAsync<Product>())!;
            return res;
        }
        else
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

    public static async void UpdateConnPassword(string newPassword)
    {
        var encryptedPassword = Crypto.Hash(newPassword);
        if (encryptedPassword.Equals(SettingsService.Settings.ConnPassword))
            return;
        
        var addressParams = $"http://{SettingsService.Settings.ServerIp}/password/conn";
        var response = await _client.PatchAsJsonAsync(addressParams, new{password = encryptedPassword});
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Error");
        }
    }

    public static async void UpdateAdminPassword(string newPassword)
    {
        var encryptedPassword = Crypto.Hash(newPassword);
        if (encryptedPassword.Equals(SettingsService.Settings.AdminPassword))
            return;
        
        var addressParams = $"http://{SettingsService.Settings.ServerIp}/password/admin";
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

    public static void StartServerWatcher(Action<List<Product>> processUpdates)
    {
        const int coolDown = 15000;
        _timerCallback = processUpdates;
        _timer = new Timer(ServerWatcher, null, 0, coolDown);
    }

    public static void StopServerWatcher()
    {
        _timer?.Dispose();
    }
    
    private static async void ServerWatcher(object? state)
    {
        try
        {
            await GetDollar();
            var products = await GetProducts(SettingsService.Settings.LastUpdate);
            if (products.Count > 0)
            {
                _timerCallback(products);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}