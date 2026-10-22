using System;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EasyPrice.Services;
using EasyPrice.Views.Mobile;

namespace EasyPrice.ViewModels.Mobile;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty] public partial bool OpenMenu { get; set; } = false;
    [ObservableProperty] public partial string Title { get; set; } = "";
    public Bitmap MenuIcon { get; } = new Bitmap(AssetLoader.Open(new Uri("avares://EasyPrice/Assets/icons/menudots.png")));
    [ObservableProperty] 
    public partial bool NavBarVisibility { get; set; } = false;

    [ObservableProperty] 
    private ObservableObject _currentPage;
    
    [RelayCommand]
    public void HandleMenu()
    {
        if (OpenMenu)
        {
            OpenMenu = !OpenMenu;
        }
        OpenMenu = !OpenMenu;
    }

    [RelayCommand]
    public void NavigateSettings()
    {
        NavBarVisibility = true;
        Title = "Ajustes";
        CurrentPage = new SettingsViewModel(() =>  NavigateLogin());
        OpenMenu = false;
    }

    [RelayCommand]
    public void NavigateHome()
    {
        NavBarVisibility = true;
        Title = "Lista de precios";
        CurrentPage = new HomeViewModel();
        OpenMenu = false;
    }

    public void NavigateLogin()
    {
        ClientService.StopServerWatcher();
        SettingsService.UpdateConnPassword("");
        SettingsService.UpdateServerIp("");
        ProductService.DeleteAllProducts();
        NavBarVisibility = false;
        CurrentPage = new InitialSettingsViewModel(() => NavigateHome());
        OpenMenu = false;
    }
    
    public MainViewModel()
    {
        if (SettingsService.Settings.ServerIp == null)
        {
            NavigateLogin();
        }
        else
        {
            NavigateHome();
        }
    }
}