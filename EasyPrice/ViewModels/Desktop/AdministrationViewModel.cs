using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Models;
using EasyPrice.Services;
using EasyPrice.Services.Windows;

namespace EasyPrice.ViewModels.Desktop;

public partial class AdministrationViewModel : ViewModelBase
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
            Reload();
        }
    }

    private ShowAddProductService _showAddProductService;
    private ShowEditProductService _showEditProductService;
    private ShowDeleteProductService _showDeleteProductService;
    private ShowSettingsService _showSettingsService;
    private ShowChangePasswordService _showChangePasswordService;
    private ShowServerSettingsService _showServerSettingsService;
    private ShowServerPasswordsService _showServerPasswordsService;
    private Action<List<Product>> _processUpdates;
    
    public AdministrationViewModel(Window window, MainViewModel ownerVm, Action<List<Product>> processUpdates)
    {
        GetProducts();
        FullList = ownerVm.FullList;
        ShownList = ownerVm.ShownList;
        _showAddProductService = new ShowAddProductService(window, this);
        _showDeleteProductService = new ShowDeleteProductService(window, this);
        _showEditProductService = new ShowEditProductService(window);
        _showSettingsService = new ShowSettingsService(window, this);
        _showChangePasswordService = new ShowChangePasswordService(window);
        _showServerSettingsService = new ShowServerSettingsService(window);
        _showServerPasswordsService = new ShowServerPasswordsService(window);

        _processUpdates = processUpdates;
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
            FullList.Add(product);
            ShownList.Add(product);
        }
    }

    [RelayCommand]
    private async Task ShowEditProductWindow(Product product)
    {
        var newProduct = await _showEditProductService.ShowEditProduct(product);
        if (newProduct != null)
        {
            FullList.Remove(product);
            FullList.Add(newProduct);
            ShownList.Remove(product);
            ShownList.Add(newProduct);
        }
    }

    [RelayCommand]
    private async Task ShowDeleteProductWindow(Product product)
    {
        var res = await _showDeleteProductService.ShowDeleteProduct(product);
        if (res == true)
        {
            FullList.Remove(product);
            ShownList.Remove(product);
        }
    }

    [RelayCommand]
    private void ShowChangePassword()
    {
        _showChangePasswordService.ShowChangePassword();
    }
    
    private void GetProducts()
    {
        FullList = ProductService.GetAll();
        ShownList = new ObservableCollection<Product>(FullList);
    }

    [RelayCommand]
    private async void ShowServerSettings()
    {
        var serverFullList = await _showServerSettingsService.ShowServerSettings();
        if (serverFullList != null)
        {
            ClientService.StartServerWatcher(_processUpdates);
            foreach (var item in serverFullList)
            {
                ProductService.AddProduct(item);
                FullList.Add(item);
                ShownList.Add(item);
            }
        }
    }

    [RelayCommand]
    private void ShowServerPasswords()
    {
        _showServerPasswordsService.ShowServerPassword();
    }

    public void Reload()
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
}