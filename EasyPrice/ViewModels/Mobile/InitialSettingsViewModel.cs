using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EasyPrice.Services;

namespace EasyPrice.ViewModels.Mobile;

public partial class InitialSettingsViewModel : ViewModelBase
{
    public Action LoginSuccess {get; set;}
    
    public InitialSettingsViewModel(Action loginSuccess)
    {
        LoginSuccess = loginSuccess;
    }

    [ObservableProperty] public partial string ServerIp { get; set; }
    [ObservableProperty] public partial string ServerPassword { get; set; }

    [RelayCommand]
    public async Task AttempLogin()
    {
        var response = await ClientService.Login(ServerIp, ServerPassword);
        if (response)
        {
            LoginSuccess();
        }
    }
}