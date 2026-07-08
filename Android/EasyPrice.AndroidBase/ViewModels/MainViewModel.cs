using CommunityToolkit.Mvvm.ComponentModel;

namespace EasyPrice.AndroidBase.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty] private string _greeting = "Easy Price Movil";
}