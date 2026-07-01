using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EasyPrice.NET.Models;

namespace EasyPrice.NET.ViewModels;

public partial class AddProductViewModel : ViewModelBase
{
    private readonly Window _window;

    [ObservableProperty]
    private partial string _name { get; set; }
    public string Name
    {
        get => _name;
        set
        {
            if (value != _name)
            {
                _name = value;
                OnPropertyChanged();
            }
        }
    }
    
    [ObservableProperty]
    private partial double? _packPrice { get; set; }
    public double? PackPrice
    {
        get => _packPrice;
        set
        {
            if (value != _packPrice)
            {
                _packPrice = value;
                OnPropertyChanged();
            }
        }
    }
    
    [ObservableProperty]
    private partial int? _packQuantity { get; set; }
    public int? PackQuantity
    {
        get => _packQuantity;
        set
        {
            if (value != _packQuantity)
            {
                _packQuantity = value;
                OnPropertyChanged();
            }
        }
    }
    
    [ObservableProperty]
    private partial int? _gain { get; set; }
    public int? Gain
    {
        get => _gain;
        set
        {
            if (value != _gain)
            {
                _gain = value;
                OnPropertyChanged();
            }
        }
    }

    public AddProductViewModel(Window window)
    {
        _window = window;
    }

    [RelayCommand]
    public void Finish()
    {
        var product = Product.AddProduct(Name, (double)PackPrice!, (int)PackQuantity!, (int)Gain!);
        _window.Close(product);
    }

    [RelayCommand]
    public void Cancel()
    {
        _window.Close();
    }
}