using Avalonia.Controls;
using EasyPrice.ViewModels.Desktop;
using EasyPrice.Views.Desktop;
using ChangeDolarPrice = EasyPrice.Views.Desktop.ChangeDolarPrice;

namespace EasyPrice.Services.Windows;

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