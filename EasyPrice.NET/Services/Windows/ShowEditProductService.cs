using Avalonia.Controls;
using EasyPrice.NET.Models;
using EasyPrice.NET.Views;

namespace EasyPrice.NET.Services.Windows;

public class ShowEditProductService
{
    private readonly Window _window;
    public ShowEditProductService(Window window)
    {
        _window = window;
    }

    public void ShowEditProduct(Product product)
    {
        var window = new EditProduct(product);
        window.ShowDialog(_window);
    }
}