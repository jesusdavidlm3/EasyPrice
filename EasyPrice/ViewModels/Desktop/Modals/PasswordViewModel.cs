using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EasyPrice.Services;
using EasyPrice.Utils;

namespace EasyPrice.ViewModels.Desktop;

public partial class PasswordViewModel : ViewModelBase
{
    private Window _window { get; set; }

    [ObservableProperty] 
    [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
    public partial string Password { get; set; } = string.Empty;

    public PasswordViewModel(Window window)
    {
        _window = window;
    }
    
    [RelayCommand(CanExecute = nameof(CanLogin))]
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

    private bool CanLogin() => Password != "";
    
    [RelayCommand]
    public void Exit()
    {
        _window.Close();
    }
}