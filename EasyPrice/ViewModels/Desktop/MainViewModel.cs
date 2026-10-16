using System;
using System.Collections.ObjectModel;
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
        ProductService.productList = FullList;
        _showAdministrationWindowService = new ShowAdministrationWindowService(window);
        if (SettingsService.Settings.WorkMode == 2)
            ClientService.StartServerWatcher(() => Reload());
        
    }

    [RelayCommand]
    private async Task ShowAdministration()
    {
        _showAdministrationWindowService.ShowAdministrationWindow(this);
    }
    
    private void GetProducts()
    {
        FullList = ProductService.GetAll();
        ShownList = new ObservableCollection<Product>(FullList);
    }

    private Action? Reload()
    {
        var newList = ProductService.GetAll();
        FullList.Clear();
        ShownList.Clear();
        foreach (var item in newList)
        {
            FullList.Add(item);
            ShownList.Add(item);
        }

        return null;
    }
}