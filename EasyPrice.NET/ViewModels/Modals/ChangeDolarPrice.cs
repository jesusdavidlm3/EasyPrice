using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EasyPrice.NET.Models;
using EasyPrice.NET.Services;

namespace EasyPrice.NET.ViewModels;

public partial class ChangeDolarPrice : ViewModelBase
{
    private Window _ownerWindow;
    public AdministrationViewModel _ownerVm { get; set; }
    
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

    public ChangeDolarPrice(Window ownerWindow, AdministrationViewModel ownerVm)
    {
        _ownerWindow = ownerWindow;
        _ownerVm = ownerVm;
    }
    
    [RelayCommand]
    public void Save()
    {
        DBService.settings.UpdateDolarPrice(DolarPrice);
        _ownerVm.Reload();
        _ownerWindow.Close();
    }
    
    [RelayCommand]
    public void Cancel()
    {
        _ownerWindow.Close();
    }
}