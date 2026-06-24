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
        var settingsWindow = new ShowSettingsService();
        settingsWindow.ShowWindow();
    }

    [RelayCommand]
    private static void ShowAddProductWindow()
    {
        var addProduct = new ShowAddProductService();
        addProduct.showAddProduct();
    }

    [RelayCommand]
    private static void ShowEditProductWindow()
    {
        var editProduct = new ShowEditProductService();
        editProduct.ShowEditProduct();
    }

    private void UpdateProducts()
    {
        Products = new ObservableCollection<Product>(Product.GetAll());
    }
}