using Avalonia.Controls;
using EasyPrice.NET.ViewModels;
using EasyPrice.NET.Views;
using ChangeDolarPrice = EasyPrice.NET.Views.ChangeDolarPrice;

namespace EasyPrice.NET.Services.Windows;

public class ShowSettingsService
{
    private readonly Window _window;
    private AdministrationViewModel _ownerVm;

    public ShowSettingsService(Window window, AdministrationViewModel ownerVm)
    {
        _window = window;
        _ownerVm = ownerVm;
    }
    
    public void ShowWindow()
    {
        var window = new ChangeDolarPrice(_ownerVm);
        window.ShowDialog(_window);
    }
}