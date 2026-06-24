using EasyPrice.NET.Views;
using ShadUI;

namespace EasyPrice.NET.Services;

public class ShowSettingsService
{
    private static Window _window;

    public ShowSettingsService(Window window)
    {
        _window = window;
    }
    
    public static void ShowWindow()
    {
        var window = new SettingsWindow();
        window.ShowDialog(_window);
    }
}