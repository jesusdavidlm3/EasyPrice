using System;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EasyPrice.Views.Mobile;

namespace EasyPrice.ViewModels.Mobile;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty] public partial bool OpenMenu { get; set; } = false;
    [ObservableProperty] public partial string Title { get; set; } = "Easy Price";
    public Bitmap MenuIcon { get; } = new Bitmap(AssetLoader.Open(new Uri("avares://EasyPrice/Assets/icons/menudots.png")));
    
    [RelayCommand]
    public void HandleMenu()
    {
        OpenMenu = !OpenMenu;
    }
}