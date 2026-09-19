using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EasyPrice.Models;
using EasyPrice.Services;

namespace EasyPrice.ViewModels.Desktop;

public partial class ChangeDolarPriceViewModel : ViewModelBase
{
    private Window _ownerWindow;
    private AdministrationViewModel _ownerVm { get; set; }
    
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    public partial float? DolarPrice { get; set; } = DBService.settings.DolarPrice;
    
    public ChangeDolarPriceViewModel(Window ownerWindow, AdministrationViewModel ownerVm)
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