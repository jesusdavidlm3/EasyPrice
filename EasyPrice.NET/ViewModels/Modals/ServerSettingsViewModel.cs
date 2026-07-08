using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EasyPrice.NET.Services;

namespace EasyPrice.NET.ViewModels;

public partial class ServerSettingsViewModel : ViewModelBase
{
    private Window _window;
    private Dictionary<string, int> WorkModes { get; } = new Dictionary<string, int>{
        { "Individual", 0 },
        { "Servidor", 1 },
        { "Cliente", 2 }
    };
    
    public ObservableCollection<string> WorkModeNames { get; set; } =
    [
        "Individual",
        "Servidor",
        "Cliente"
    ];

    [ObservableProperty] 
    [NotifyPropertyChangedFor(nameof(NonServer))]
    public partial string SelectedWorkMode { get; set; }
    
    [ObservableProperty]
    public partial string ServerPassword { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string ServerIp { get; set; } = string.Empty;
    
    public bool NonServer => SelectedWorkMode == "Cliente";

    public ServerSettingsViewModel(Window window)
    {
        _window = window;
    }
    
    [RelayCommand]
    public void Cancel()
    {
        _window.Close();
    }

    [RelayCommand]
    public void Save()
    {
        
    }
}