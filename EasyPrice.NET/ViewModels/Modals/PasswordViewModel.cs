using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EasyPrice.NET.Services;
using EasyPrice.NET.Utils;

namespace EasyPrice.NET.ViewModels;

public partial class PasswordViewModel : ViewModelBase
{
    private Window _window { get; set; }

    [ObservableProperty] 
    public partial string Password { get; set; } = string.Empty;

    public PasswordViewModel(Window window)
    {
        _window = window;
    }
    
    [RelayCommand]
    public void Login()
    {
        if (Crypto.Hash(Password) != DBService.settings.Password)
        {
            _window.Close();
        }
        else
        {
            _window.Close(true);
        }
    }
}