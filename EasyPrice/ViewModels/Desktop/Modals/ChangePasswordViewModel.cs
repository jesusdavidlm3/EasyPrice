using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EasyPrice.Services;
using EasyPrice.Utils;

namespace EasyPrice.ViewModels.Desktop;

public partial class ChangePasswordViewModel : ViewModelBase
{
    private Window _window;

    [ObservableProperty] 
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    public partial string NewPassword { get; set; } = string.Empty;

    public ChangePasswordViewModel(Window window)
    {
        _window = window;
    }
    
    [RelayCommand(CanExecute =  nameof(CanSave))]
    public void Save()
    {
        SettingsService.UpdateAdminPassword(NewPassword);
        _window.Close();
    }
    
    private bool CanSave() => NewPassword != "";

    [RelayCommand]
    public void Cancel()
    {
        _window.Close();
    }
}