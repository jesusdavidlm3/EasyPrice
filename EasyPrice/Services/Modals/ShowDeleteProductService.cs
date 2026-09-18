using System.Threading.Tasks;
using Avalonia.Controls;
using EasyPrice.Models;
using EasyPrice.ViewModels;
using EasyPrice.Views;

namespace EasyPrice.Services.Windows;

public class ShowDeleteProductService
{
    private readonly Window _window;
    private AdministrationViewModel _ownerVm;

    public ShowDeleteProductService(Window window, AdministrationViewModel ownerVm)
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