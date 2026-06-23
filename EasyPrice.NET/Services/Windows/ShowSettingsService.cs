using EasyPrice.NET.Views;
using ShadUI;

namespace EasyPrice.NET.Services;

public class ShowSettingsService
{
    private static Window _owner;

    public ShowSettingsService()
    {
    }
    
    public void ShowWindow()
    {
        var window = new SettingsWindow();
        window.Show();
    }
}