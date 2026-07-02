using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace EasyPrice.NET.ViewModels;

public partial class PasswordViewModel : ViewModelBase
{
    private Window _window { get; set; }
    
    [ObservableProperty]
    public partial string Password { get; set; }

    public PasswordViewModel(Window window)
    {
        _window = window;
    }
    
    [RelayCommand]
    public void Login()
    {
        _window.Close(true);
    }
}