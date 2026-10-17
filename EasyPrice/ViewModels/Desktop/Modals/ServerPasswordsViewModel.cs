using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EasyPrice.Services;

namespace EasyPrice.ViewModels.Desktop;

public partial class ServerPasswordsViewModel : ViewModelBase
{
    private Window _window;
    
    public ObservableCollection<string> PasswordTypes { get; set; } =
    [
        "Coneccion",
        "Administracion"
    ];
    
    [ObservableProperty]
    public partial string? SelectedPasswordType { get; set; }
    
    [ObservableProperty] 
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    public partial string NewPassword { get; set; } = string.Empty;
    
    public ServerPasswordsViewModel(Window window)
    {
        SelectedPasswordType = PasswordTypes.FirstOrDefault();
        _window = window;
    }
    
    [RelayCommand(CanExecute = nameof(CanSave))]
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