using Avalonia.Controls;
using EasyPrice.Views.Desktop.Modals;

namespace EasyPrice.Services.Windows;

public class ShowServerPasswordsService
{
    private Window _window { get; set; }
    
    public ShowServerPasswordsService(Window window)
    {
        _window = window;
    }

    public void ShowServerPassword()
    {
        var window = new ServerPasswords();
        window.ShowDialog(_window);
    }
}