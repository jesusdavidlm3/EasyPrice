using EasyPrice.NET.Views;
using ShadUI;

namespace EasyPrice.NET.Services;

public class ShowAddProductService
{
    private static Window _window;
    
    public ShowAddProductService(Window window)
    {
        _window = window;
    }

    public static void showAddProduct()
    {
        var window = new AddProduct();
        window.ShowDialog(_window);
    }
}