using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Models;
using EasyPrice.Services;
using System.Threading.Tasks;

namespace EasyPrice.ViewModels.Desktop;

public partial class ChangeDolarPriceViewModel : ViewModelBase
{
    private Window _ownerWindow;
    private AdministrationViewModel _ownerVm { get; set; }
    
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    public partial double DolarPrice { get; set; } = SettingsService.Settings.DolarPrice;
    
    public ChangeDolarPriceViewModel(Window ownerWindow, AdministrationViewModel ownerVm)
    {
        _ownerWindow = ownerWindow;
        _ownerVm = ownerVm;
    }
    
    [RelayCommand(CanExecute = nameof(CanSave))]
    public async Task Save()
    {
        if (SettingsService.Settings.WorkMode == 2)
        {
            await ClientService.UpdateDollar((double)DolarPrice);
        }
        else
        {
            SettingsService.UpdateDolarPrice((double)DolarPrice!);
        }
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