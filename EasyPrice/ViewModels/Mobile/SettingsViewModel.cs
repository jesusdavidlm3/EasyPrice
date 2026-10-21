using System;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace EasyPrice.ViewModels.Mobile;

public partial class SettingsViewModel : ViewModelBase
{
    public Action Logout { get; set; }

    public SettingsViewModel(Action logout)
    {
        Logout = logout;
    }

    [RelayCommand]
    public void HandleLogout()
    {
        Logout();
    }

    [RelayCommand]
    public void HandleLogin()
    {
        
    }
}