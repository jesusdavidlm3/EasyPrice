using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EasyPrice.NET.Models;
using EasyPrice.NET.Services;

namespace EasyPrice.NET.ViewModels;

public partial class EditProductViewModel : ViewModelBase
{
    private  readonly Window _window;
    [ObservableProperty]
    public Product _product;
    
    [ObservableProperty]
    public partial double PackPrice { get; set; }
    
    [ObservableProperty]
    public partial int PackQuantity { get; set; }
    
    [ObservableProperty]
    public partial int Gain { get; set; }
    
    [ObservableProperty]
    public partial double NewPriceDolar { get; set; }
    
    [ObservableProperty]
    public partial double NewPriceBs { get; set; }
    
    public EditProductViewModel(Window window, Product product)
    {
        _product = product;
        _window = window;
        PackPrice = product.PackPrice;
        PackQuantity = product.PackQuantity;
        Gain = product.Gain;
    }

    public void CalculateNewPrices()
    {
        double unitPrice = PackPrice / PackQuantity;
        double operableGain = Gain / 100d;
        double unitValueDolar = unitPrice + (operableGain * unitPrice);
        NewPriceDolar = unitValueDolar;
        NewPriceBs = unitValueDolar * DBService.settings.DolarPrice;
    }

    partial void OnPackPriceChanged(double value)
    {
        CalculateNewPrices();
    }

    partial void OnPackQuantityChanged(int value)
    {
        CalculateNewPrices();
    }

    partial void OnGainChanged(int value)
    {
        CalculateNewPrices();
    }

    [RelayCommand]
    public void Cancel()
    {
        _window.Close();
    }
    
}