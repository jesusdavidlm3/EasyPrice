using Avalonia.Controls;
using EasyPrice.NET.Views;

namespace EasyPrice.NET.Services.Windows;

public class ShowSettingsService
{
    private readonly Window _window;

    public ShowSettingsService(Window window)
    {
        _window = window;
    }
    
    public void ShowWindow()
    {
        var window = new SettingsWindow();
        window.ShowDialog(_window);
    }
}