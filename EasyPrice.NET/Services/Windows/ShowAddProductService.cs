using System.Threading.Tasks;
using Avalonia.Controls;
using EasyPrice.NET.Models;
using EasyPrice.NET.ViewModels;
using EasyPrice.NET.Views;

namespace EasyPrice.NET.Services.Windows;

public class ShowAddProductService
{
    private MainWindowViewModel _ownerVm;
    private readonly Window _window;
    
    public ShowAddProductService(Window window, MainWindowViewModel ownerVm)
    {
        _window = window;
        _ownerVm = ownerVm;
    }

    public async Task<Product?> ShowAddProduct()
    {
        var window = new AddProduct();
        var res = await window.ShowDialog<Product>(_window);
        return res;
    }
}