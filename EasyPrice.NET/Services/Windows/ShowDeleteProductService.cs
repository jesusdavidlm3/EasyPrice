using System.Threading.Tasks;
using Avalonia.Controls;
using EasyPrice.NET.Models;
using EasyPrice.NET.ViewModels;
using EasyPrice.NET.Views;

namespace EasyPrice.NET.Services;

public class ShowDeleteProductService
{
    private static Window _window;

    public ShowDeleteProductService(Window owner, MainWindowViewModel ownerVm)
    {
        _window = owner;
    }

    public static async Task<bool?> ShowDeleteProduct(Product product)
    {
        var window = new DeleteProduct(product);
        var res =  await window.ShowDialog<bool>(_window);
        return res;
    }
}