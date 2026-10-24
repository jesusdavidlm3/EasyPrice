using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Models;
using EasyPrice.Services;
using EasyPrice.Services.Windows;

namespace EasyPrice.ViewModels.Desktop;

public partial class MainViewModel : ViewModelBase
{
    public ObservableCollection<Product> FullList { get; set; } = [];
    public ObservableCollection<Product> ShownList { get; set; } = [];
    private ShowAdministrationWindowService _showAdministrationWindowService;
    
    [ObservableProperty]
    public partial string SearchText { get; set; } = string.Empty;

    [ObservableProperty]
    public partial double DolarPrice { get; private set; } = SettingsService.Settings.DolarPrice;

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
            Reload();
        }
    }
    
    public MainViewModel(Window window)
    {
        GetProducts();
        ProductService.ProductList = FullList;
        _showAdministrationWindowService = new ShowAdministrationWindowService(window);
        if (SettingsService.Settings.WorkMode == 2)
            ClientService.StartServerWatcher((List<Product> receivedProducts) => ProcessUpdates(receivedProducts));
        
    }

    [RelayCommand]
    private async Task ShowAdministration()
    {
        _showAdministrationWindowService.ShowAdministrationWindow(this, (List<Product> rec) => ProcessUpdates(rec));
    }
    
    private void GetProducts()
    {
        FullList = ProductService.GetAll();
        ShownList = new ObservableCollection<Product>(FullList);
    }

    private void Reload()
    {
        var newList = ProductService.GetAll();
        FullList.Clear();
        ShownList.Clear();
        foreach (var item in newList)
        {
            FullList.Add(item);
            ShownList.Add(item);
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

            if (existingProduct != null && p is { Deleted: true })
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