using Avalonia.Controls;
using EasyPrice.NET.ViewModels;
using EasyPrice.NET.Views;

namespace EasyPrice.NET.Services.Windows;

public class ShowSettingsService
{
    private readonly Window _window;
    private MainWindowViewModel _ownerVm;

    public ShowSettingsService(Window window, MainWindowViewModel ownerVm)
    {
        _window = window;
        _ownerVm = ownerVm;
    }
    
    public void ShowWindow()
    {
        var window = new SettingsWindow(_ownerVm);
        window.ShowDialog(_window);
    }
}