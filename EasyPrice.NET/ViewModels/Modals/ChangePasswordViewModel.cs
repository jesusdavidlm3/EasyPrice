using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EasyPrice.NET.Services;
using EasyPrice.NET.Utils;

namespace EasyPrice.NET.ViewModels;

public partial class ChangePasswordViewModel : ViewModelBase
{
    private Window _window;
    
    [ObservableProperty]
    public partial string NewPassword { get; set; }

    public ChangePasswordViewModel(Window window)
    {
        _window = window;
    }
    
    [RelayCommand]
    public void Save()
    {
        DBService.settings.UpdatePassword(NewPassword);
        _window.Close();
    }
}