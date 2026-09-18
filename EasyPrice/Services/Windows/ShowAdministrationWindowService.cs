using EasyPrice.Views;
using Avalonia.Controls;
using EasyPrice.ViewModels;

namespace EasyPrice.Services.Windows;

public class ShowAdministrationWindowService : ProtectedViewServiceBase
{
    public ShowAdministrationWindowService(Window window)
    {
        _window = window;
    }
    
    public void ShowAdministrationWindow(MainViewModel ownerVm)
    {
        var administrationWindow = new Administration(ownerVm);
        _ = ShowProtected(administrationWindow);
    }
}