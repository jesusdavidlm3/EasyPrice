using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EasyPrice.NET.Models;
using EasyPrice.NET.Services;

namespace EasyPrice.NET.ViewModels;

public partial class ChangeDolarPrice : ViewModelBase
{
    private Window _ownerWindow;
    private AdministrationViewModel _ownerVm { get; set; }
    
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    public partial float? DolarPrice { get; set; } = DBService.settings.DolarPrice;
    
    public ChangeDolarPrice(Window ownerWindow, AdministrationViewModel ownerVm)
    {
        _ownerWindow = ownerWindow;
        _ownerVm = ownerVm;
    }
    
    [RelayCommand(CanExecute = nameof(CanSave))]
    public void Save()
    {
        DBService.settings.UpdateDolarPrice((float)DolarPrice!);
        _ownerVm.Reload();
        _ownerWindow.Close();
    }
    
    private bool CanSave() => DolarPrice > 0;
    
    [RelayCommand]
    public void Cancel()
    {
        _ownerWindow.Close();
    }
}