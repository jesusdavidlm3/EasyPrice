using System;
using System.Threading.Tasks;
using EasyPrice.NET.ViewModels;
using EasyPrice.NET.Views;
using Avalonia.Controls;
using EasyPrice.NET.Models;

namespace EasyPrice.NET.Services;

public class ShowAddProductService
{
    private static MainWindowViewModel _OwnerVM;
    private static Window _window;
    
    public ShowAddProductService(Window window, MainWindowViewModel OwnerVM)
    {
        _window = window;
        _OwnerVM = OwnerVM;
    }

    public static async Task<Product> showAddProduct()
    {
        var window = new AddProduct(_OwnerVM);
        var res = await window.ShowDialog<Product>(_window);
        return res;
    }
}