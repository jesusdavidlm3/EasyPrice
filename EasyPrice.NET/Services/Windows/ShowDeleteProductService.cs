using Avalonia.Controls;
using EasyPrice.NET.ViewModels;
using EasyPrice.NET.Views;

namespace EasyPrice.NET.Services;

public class ShowDeleteProductService
{
    private static Window _window;
    public MainWindowViewModel _ownerVm;

    public ShowDeleteProductService(Window owner, MainWindowViewModel ownerVm)
    {
        _window = owner;
        _ownerVm = ownerVm;
    }

    public static void ShowDeleteProduct()
    {
        var window = new DeleteProduct();
        window.ShowDialog(_window);
    }
}