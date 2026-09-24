using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EasyPrice.Services;
using Models;

namespace EasyPrice.ViewModels.Desktop;

public partial class DeleteProductViewModel : ViewModelBase
{
    private Window _window;
    
    [ObservableProperty]
    private Product _productToDelete;

    public DeleteProductViewModel(Window window, Product productToDelete)
    {
        _window = window;
        ProductToDelete = productToDelete;
    }

    [RelayCommand]
    public void Delete()
    {
        ProductService.DeleteProduct(ProductToDelete);
        _window.Close(true);
    }

    [RelayCommand]
    public void Cancel()
    {
        _window.Close(false);
    }
}