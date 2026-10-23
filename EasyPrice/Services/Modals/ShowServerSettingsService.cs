using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Controls;
using EasyPrice.Views.Desktop.Modals;
using Models;

namespace EasyPrice.Services.Windows;

public class ShowServerSettingsService
{
    private readonly Window _window;
    public ShowServerSettingsService(Window window)
    {
        _window = window;
    }

    public async Task<List<Product>?> ShowServerSettings()
    {
        var window = new ServerSettings();
        var res = await window.ShowDialog<List<Product>>(_window);
        return res;
    }
}