using System.Threading.Tasks;
using Avalonia.Controls;
using EasyPrice.Models;
using EasyPrice.ViewModels;
using EasyPrice.Views;

namespace EasyPrice.Services.Windows;

public class ShowAddProductService
{
    private AdministrationViewModel _ownerVm;
    private readonly Window _window;
    
    public ShowAddProductService(Window window, AdministrationViewModel ownerVm)
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