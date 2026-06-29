using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EasyPrice.NET.Models;
using EasyPrice.NET.Services;

namespace EasyPrice.NET.ViewModels;

public partial class SettingsWindowViewModel : ViewModelBase
{
    private Window _ownerWindow;
    [ObservableProperty]
    private partial float _dolarPrice { get; set; } = DBService.settings.DolarPrice;
    public float DolarPrice
    {
        get => _dolarPrice;
        set
        {
            if (value != _dolarPrice)
            {
                _dolarPrice = value;
            }
        }
    }

    public SettingsWindowViewModel(Window ownerWindow)
    {
        _ownerWindow = ownerWindow;
    }

    [RelayCommand]
    public void Save()
    {
        DBService.settings.UpdateDolarPrice(DolarPrice);
        _ownerWindow.Close();
    }
}