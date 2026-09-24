using System.Threading.Tasks;
using Avalonia.Controls;
using Models;
using EasyPrice.Views.Desktop;

namespace EasyPrice.Services.Windows;

public class ShowEditProductService
{
    private readonly Window _window;
    public ShowEditProductService(Window window)
    {
        _window = window;
    }

    public async Task<Product?> ShowEditProduct(Product product)
    {
        var window = new EditProduct(product);
        var res = await window.ShowDialog<Product>(_window);
        return res;
    }
}