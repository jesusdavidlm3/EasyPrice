using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System;
/*using SystemworkInformation;
using System.Sockets;*/
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EasyPrice.Services;

namespace EasyPrice.ViewModels;

public partial class ServerSettingsViewModel : ViewModelBase
{
    private Window _window;

    private static Dictionary<string, int> WorkModes { get; } = new Dictionary<string, int>{
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
    [NotifyPropertyChangedFor(nameof(ClientMode))]
    [NotifyPropertyChangedFor(nameof(ServerMode))]
    public partial string SelectedWorkMode { get; set; } = WorkModes.FirstOrDefault(x => DBService.settings.Server == x.Value).Key.ToString();
    
    [ObservableProperty]
    public partial string ServerPassword { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string ServerIp { get; set; } = string.Empty;
    
    public bool ClientMode => SelectedWorkMode == "Cliente";
    public bool ServerMode =>  SelectedWorkMode == "Servidor";

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