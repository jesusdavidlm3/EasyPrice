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
        Products.Add(product);
    }

    [RelayCommand]
    private static void ShowEditProductWindow()
    {
        ShowEditProductService.ShowEditProduct();
    }

    [RelayCommand]
    private void ShowDeleteProductWindow()
    {
        ShowDeleteProductService.ShowDeleteProduct();
    }
    
    private void GetProducts()
    {
        Products = Product.GetAll();
    }

    public void UpdateProducts(Product product)
    {
        Products.Add(product);
    }
}