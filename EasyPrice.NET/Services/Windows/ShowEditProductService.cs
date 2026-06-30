using Avalonia.Controls;
using EasyPrice.NET.Views;

namespace EasyPrice.NET.Services.Windows;

public class ShowEditProductService
{
    private readonly Window _window;
    public ShowEditProductService(Window window)
    {
        _window = window;
    }

    public void ShowEditProduct()
    {
        var window = new EditProduct();
        window.ShowDialog(_window);
    }
}