using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using EasyPrice.NET.Models;
using EasyPrice.NET.Services;
using EasyPrice.NET.Services.Windows;

namespace EasyPrice.NET.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<Product> Products { get; set; } = [];

    private ShowAddProductService _showAddProductService;
    private ShowEditProductService _showEditProductService;
    private ShowDeleteProductService _showDeleteProductService;
    private ShowSettingsService _showSettingsService;
    
    public MainWindowViewModel(Window  window)
    {
        GetProducts();
        _showAddProductService = new ShowAddProductService(window, this);
        _showDeleteProductService = new ShowDeleteProductService(window, this);
        _showEditProductService = new ShowEditProductService(window);
        _showSettingsService = new ShowSettingsService(window, this);
    }
    
    [RelayCommand]
    private void ShowSettingsWindow()
    {
        _showSettingsService.ShowWindow();
    }

    [RelayCommand]
    private async Task ShowAddProductWindow()
    {
        var product = await _showAddProductService.ShowAddProduct();
        if (product != null)
        {
            Products.Add(product);
        }
    }

    [RelayCommand]
    private void ShowEditProductWindow()
    {
        _showEditProductService.ShowEditProduct();
    }

    [RelayCommand]
    private async Task ShowDeleteProductWindow(Product product)
    {
        var res = await _showDeleteProductService.ShowDeleteProduct(product);
        if (res == true)
        {
            Products.Remove(product);
        }
    }
    
    private void GetProducts()
    {
        Products = Product.GetAll();
    }

    public void Reload()
    {
        var newList = Product.GetAll();
        Products.Clear();
        foreach (var item in newList)
        {
            Products.Add(item);
        }
    }
}