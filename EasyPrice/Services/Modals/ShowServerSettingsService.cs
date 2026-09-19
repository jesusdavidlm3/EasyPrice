using System.Threading.Tasks;
using Avalonia.Controls;
using EasyPrice.Models;
using EasyPrice.Views.Desktop;
using EasyPrice.Views.Desktop.Modals;

namespace EasyPrice.Services.Windows;

public class ShowServerSettingsService
{
    private readonly Window _window;
    public ShowServerSettingsService(Window window)
    {
        _window = window;
    }

    public Task ShowServerSettings()
    {
        var window = new ServerSettings();
        window.ShowDialog(_window);
        return Task.CompletedTask;
    }
}