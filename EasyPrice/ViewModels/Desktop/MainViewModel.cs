using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EasyPrice.Models;
using EasyPrice.Services;
using EasyPrice.Services.Windows;

namespace EasyPrice.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public ObservableCollection<Product> FullList { get; set; } = [];
    public ObservableCollection<Product> ShownList { get; set; } = [];

    private ShowAdministrationWindowService _showAdministrationWindowService;
    
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
    
    public MainViewModel(Window window)
    {
        GetProducts();
        _showAdministrationWindowService = new ShowAdministrationWindowService(window);
    }

    [RelayCommand]
    private async Task ShowAdministration()
    {
        _showAdministrationWindowService.ShowAdministrationWindow(this);
    }
    
    private void GetProducts()
    {
        FullList = Product.GetAll();
        ShownList = new ObservableCollection<Product>(FullList);
    }

    public void Reload()
    {
        var newList = Product.GetAll();
        FullList.Clear();
        ShownList.Clear();
        foreach (var item in newList)
        {
            FullList.Add(item);
            ShownList.Add(item);
        }
    }
}