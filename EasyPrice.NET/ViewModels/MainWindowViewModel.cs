using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using EasyPrice.NET.Models;
using EasyPrice.NET.Services;

namespace EasyPrice.NET.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<Product> Products { get; set; }
    public MainWindowViewModel()
    {
        UpdateProducts();
    }
    
    [RelayCommand]
    private static void ShowSettingsWindow()
    {
        ShowSettingsService.ShowWindow();
    }

    [RelayCommand]
    private static void ShowAddProductWindow()
    {
        ShowAddProductService.showAddProduct();
    }

    [RelayCommand]
    private static void ShowEditProductWindow()
    {
        ShowEditProductService.ShowEditProduct();
    }

    private void UpdateProducts()
    {
        Products = new ObservableCollection<Product>(Product.GetAll());
    }
}