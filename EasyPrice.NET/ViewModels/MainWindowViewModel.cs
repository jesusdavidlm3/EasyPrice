using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using EasyPrice.NET.Models;
using EasyPrice.NET.Services;

namespace EasyPrice.NET.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<Product> Products { get; set; } = [];
    
    public MainWindowViewModel()
    {
        GetProducts();
    }
    
    [RelayCommand]
    private static void ShowSettingsWindow()
    {
        ShowSettingsService.ShowWindow();
    }

    [RelayCommand]
    private async Task ShowAddProductWindow()
    {
        var product = await ShowAddProductService.showAddProduct();
        if (product != null)
        {
            Products.Add(product);
        }
    }

    [RelayCommand]
    private static void ShowEditProductWindow()
    {
        ShowEditProductService.ShowEditProduct();
    }

    [RelayCommand]
    private async Task ShowDeleteProductWindow(Product product)
    {
        var res = await ShowDeleteProductService.ShowDeleteProduct(product);
        if (res == true)
        {
            Products.Remove(product);
        }
    }
    
    private void GetProducts()
    {
        Products = Product.GetAll();
    }
}