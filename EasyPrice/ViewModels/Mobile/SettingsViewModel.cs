using System;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace EasyPrice.ViewModels.Mobile;

public partial class SettingsViewModel : ViewModelBase
{
    [ObservableProperty] public partial bool OpenMenu { get; set; } = false;

    [RelayCommand]
    public void HandleMenu()
    {
        OpenMenu = !OpenMenu;
    }

    public Bitmap MenuIcon { get; } = new Bitmap(AssetLoader.Open(new Uri("avares://EasyPrice/Assets/icons/menudots.png")));
}