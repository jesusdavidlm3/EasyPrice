using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Models;
using EasyPrice.Services;

namespace EasyPrice.ViewModels.Desktop;

public partial class EditProductViewModel : ViewModelBase
{
    private  readonly Window _window;
    
    [ObservableProperty]
    public partial Product CurrentProduct { get; set; }
    
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    public partial double? PackPrice { get; set; }
    
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    public partial int? PackQuantity { get; set; }
    
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    public partial int? Gain { get; set; }
    
    [ObservableProperty]
    public partial double? NewPriceDolar { get; set; }
    
    [ObservableProperty]
    public partial double? NewPriceBs { get; set; }
    
    public double DolarPrice { get; } = SettingsService.Settings.DolarPrice;
    
    public EditProductViewModel(Window window, Product product)
    {
        CurrentProduct = product;
        _window = window;
        PackPrice = product.PackPrice;
        PackQuantity = product.PackQuantity;
        Gain = product.Gain;
    }

    public void CalculateNewPrices()
    {
        double? unitPrice = PackPrice / PackQuantity;
        double? operableGain = Gain / 100d;
        double? unitValueDolar = unitPrice + (operableGain * unitPrice);
        NewPriceDolar = unitValueDolar;
        NewPriceBs = unitValueDolar * SettingsService.Settings.DolarPrice;
    }

    partial void OnPackPriceChanged(double? value)
    {
        CalculateNewPrices();
    }

    partial void OnPackQuantityChanged(int? value)
    {
        CalculateNewPrices();
    }

    partial void OnGainChanged(int? value)
    {
        CalculateNewPrices();
    }

    [RelayCommand]
    public void Cancel()
    {
        _window.Close();
    }

    [RelayCommand(CanExecute = nameof(CanSave))]
    public void Save()
    {
        CurrentProduct.PackPrice = (double)PackPrice!;
        CurrentProduct.PackQuantity = (int)PackQuantity!;
        CurrentProduct.Gain = (int)Gain!;
        ProductService.UpdateProduct(CurrentProduct);
        _window.Close(CurrentProduct);
    }

    private bool CanSave() => PackPrice != null && PackQuantity != null && Gain != null;
}