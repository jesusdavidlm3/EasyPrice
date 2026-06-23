using CommunityToolkit.Mvvm.Input;
using EasyPrice.NET.Services;

namespace EasyPrice.NET.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [RelayCommand]
    private static void ShowSettingsWindow()
    {
        var settingsWindow = new ShowSettingsService();
        settingsWindow.ShowWindow();
    }

    [RelayCommand]
    private static void ShowAddProductWindow()
    {
        var addProduct = new ShowAddProductService();
        addProduct.showAddProduct();
    }
}