using EasyPrice.NET.Views;
using ShadUI;

namespace EasyPrice.NET.Services;

public class ShowEditProductService
{
    private static Window _window;
    public ShowEditProductService(Window window)
    {
        _window = window;
    }

    public static void ShowEditProduct()
    {
        var window = new EditProduct();
        window.ShowDialog(_window);
    }
}