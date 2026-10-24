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
    public ObservableCollection<Product> FullList { get; set; } = [];
    public ObservableCollection<Product> ShownList { get; set; } = [];

    [ObservableProperty]
    public partial string SearchText { get; set; } = string.Empty;

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
            ShownList = FullList;
        }
    }
    
    public HomeViewModel()
    {
        GetProducts();
        ProductService.ProductList = FullList;
        if (SettingsService.Settings.WorkMode == 2)
            ClientService.StartServerWatcher((List<Product> receivedProducts) => ProcessUpdates(receivedProducts));
        
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

            if (p is { Deleted: true })
            {
                ProductService.DeleteProduct(existingProduct!);
                ProductService.ProductList.Remove(existingProduct!);
            }else if (existingProduct is {Deleted: false})
            {
                ProductService.UpdateProduct(p);
                ProductService.ProductList.Remove(existingProduct);
            }
            else
            {
                ProductService.AddProduct(p);
                ProductService.ProductList.Add(p);
            }
            
            if (p.LastUpdate > newLastDate)
                newLastDate = p.LastUpdate;
        }

        if(!SettingsService.Settings.LastUpdate.Equals(newLastDate))
            SettingsService.UpdateLastUpdate((DateTime)newLastDate!);
        
        Reload();
    }
}