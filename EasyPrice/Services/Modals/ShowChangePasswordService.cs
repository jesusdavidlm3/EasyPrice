using Avalonia.Controls;
using EasyPrice.Views.Modals;

namespace EasyPrice.Services.Windows;

public class ShowChangePasswordService
{
    private Window _window { get; set; }
    
    public ShowChangePasswordService(Window window)
    {
        _window = window;
    }

    public void ShowChangePassword()
    {
        var window = new ChangePassword();
        window.ShowDialog(_window);
    }
}