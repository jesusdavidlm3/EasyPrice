using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EasyPrice.Services;
using Models;

namespace EasyPrice.ViewModels.Mobile;

public partial class HomeViewModel : ViewModelBase
{
    [ObservableProperty]
    public ObservableCollection<Product> _fullList = [];
    [ObservableProperty]
    public ObservableCollection<Product> _shownList = [];

    [ObservableProperty]
    public string _searchText = string.Empty;

    partial void OnSearchTextChanged(string value)
    {
        ShownList.Clear();
        if (value != "")
        {
            foreach (var product in FullList)
            {
                if (product.Name.ToLower().Contains(value.ToLower()))
                {
                    ShownList.Add(product);
                }
            }
        }
        else
        {
            foreach (var item in FullList)
            {
                ShownList.Add(item);
            }
        }
    }
    
    public HomeViewModel()
    {
        GetProducts();
        ProductService.ProductList = FullList;
        if (!ClientService.isRunning)
            ClientService.StartServerWatcher(receivedProducts => ProcessUpdates(receivedProducts));
    }
    
    private async void GetProducts()
    {
        FullList = ProductService.GetAll();
        if (FullList.Count == 0)
        {
            var receivedList = await ClientService.GetProducts();
            foreach (var product in receivedList)
            {
                ProductService.AddProduct(product);
                FullList.Add(product);
                ShownList.Add(product);
            }
        }
        else
        {
            foreach (var item in FullList)
            {
                ShownList.Add(item);
            }
        }
    }

    private void ProcessUpdates(List<Product> receivedProducts)
    {
        DateTime? newLastDate = SettingsService.Settings.LastUpdate;
        if(receivedProducts.Count > 0)
            newLastDate = receivedProducts[0].LastUpdate;
        
        foreach (var p in receivedProducts)
        {
            Product? existingProduct = null;
            try
            {
                existingProduct = FullList.FirstOrDefault(pl => pl.Id == p.Id);
            }
            catch (Exception e)
            {
                // ignored
            }

            if (p is { Deleted: true })
            {
                ProductService.DeleteProduct(existingProduct!);
                ProductService.ProductList.Remove(existingProduct!);
                FullList.Remove(existingProduct!);
                if(ShownList.Contains(existingProduct!))
                    ShownList.Remove(existingProduct!);
            }else if (existingProduct != null && p is {Deleted: false})
            {
                ProductService.UpdateProduct(p);
                ProductService.ProductList.Remove(existingProduct);
                FullList.Remove(existingProduct);
                FullList.Add(p);
                if (ShownList.Contains(existingProduct))
                {
                    ShownList.Remove(existingProduct);
                    ShownList.Add(p);
                }
            }
            else if(p is {Deleted: false})
            {
                ProductService.AddProduct(p);
                ProductService.ProductList.Add(p);
                FullList.Add(p);
                ShownList.Add(p);
            }
            
            if (p.LastUpdate > newLastDate)
                newLastDate = p.LastUpdate;
        }

        if(!SettingsService.Settings.LastUpdate.Equals(newLastDate))
            SettingsService.UpdateLastUpdate((DateTime)newLastDate!);
    }
}