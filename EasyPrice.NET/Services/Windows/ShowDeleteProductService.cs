using System.Threading.Tasks;
using Avalonia.Controls;
using EasyPrice.NET.Models;
using EasyPrice.NET.ViewModels;
using EasyPrice.NET.Views;

namespace EasyPrice.NET.Services.Windows;

public class ShowDeleteProductService
{
    private readonly Window _window;
    private MainWindowViewModel _ownerVm;

    public ShowDeleteProductService(Window window, MainWindowViewModel ownerVm)
    {
        _window = window;
        _ownerVm = ownerVm;
    }

    public async Task<bool?> ShowDeleteProduct(Product product)
    {
        var window = new DeleteProduct(product);
        var res =  await window.ShowDialog<bool>(_window);
        return res;
    }
}