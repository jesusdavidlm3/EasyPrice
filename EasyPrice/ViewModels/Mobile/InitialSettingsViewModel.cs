using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace EasyPrice.ViewModels.Mobile;

public partial class InitialSettingsViewModel : ViewModelBase
{
    [ObservableProperty] public partial string ServerIp { get; set; }
    [ObservableProperty] public partial string ServerPassword { get; set; }

    [RelayCommand]
    public void AttempLogin()
    {
        
    }
}