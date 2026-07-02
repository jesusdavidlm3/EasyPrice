using EasyPrice.NET.Views;
using Avalonia.Controls;
using EasyPrice.NET.ViewModels;

namespace EasyPrice.NET.Services.Windows;

public class ShowAdministrationWindowService : ProtectedViewServiceBase
{
    public ShowAdministrationWindowService(Window window)
    {
        _window = window;
    }
    
    public void ShowAdministrationWindow(MainWindowViewModel ownerVm)
    {
        var administrationWindow = new Administration(ownerVm);
        _ = ShowProtected(administrationWindow);
    }
}