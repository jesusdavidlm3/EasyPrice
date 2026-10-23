using System;
using System.Collections.Generic;
using EasyPrice.Views.Desktop;
using Avalonia.Controls;
using EasyPrice.ViewModels.Desktop;
using Models;

namespace EasyPrice.Services.Windows;

public class ShowAdministrationWindowService : ProtectedViewServiceBase
{
    public ShowAdministrationWindowService(Window window)
    {
        _window = window;
    }
    
    public void ShowAdministrationWindow(MainViewModel ownerVm, Action<List<Product>> processUpdates)
    {
        var administrationWindow = new Administration(ownerVm, processUpdates);
        _ = ShowProtected(administrationWindow);
    }
}