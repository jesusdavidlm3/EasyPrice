using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EasyPrice.Services;
using Models;

namespace EasyPrice.ViewModels.Desktop;

public partial class AddProductViewModel : ViewModelBase
{
    private readonly Window _window;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(FinishCommand))]
    public partial string Name { get; set; } = string.Empty;
    
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(FinishCommand))]
    public partial double? PackPrice { get; set; }
    
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(FinishCommand))]
    public partial int? PackQuantity { get; set; }
    
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(FinishCommand))]
    public partial int? Gain { get; set; }

    public AddProductViewModel(Window window)
    {
        _window = window;
    }

    [RelayCommand(CanExecute = nameof(CanFinish))]
    public void Finish()
    {
        var product = ProductService.AddProduct(Name, (double)PackPrice!, (int)PackQuantity!, (int)Gain!);
        _window.Close(product);
    }
    
    private bool CanFinish() => Name != "" && PackPrice != null && PackQuantity != null && Gain != null;

    [RelayCommand]
    public void Cancel()
    {
        _window.Close();
    }
}