using EasyPrice.NET.Views;
using Avalonia.Controls;
using EasyPrice.NET.ViewModels;

namespace EasyPrice.NET.Services.Windows;

public class ShowAdministrationWindowService
{
    private Window _window;

    public ShowAdministrationWindowService(Window window)
    {
        _window = window;
    }
    
    public void ShowAdministrationWindow(MainWindowViewModel ownerVm)
    {
        var administrationWindow = new Administration(ownerVm);
        administrationWindow.ShowDialog(_window);
    }
}