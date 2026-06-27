using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using EasyPrice.NET.Models;

namespace EasyPrice.NET.ViewModels;

public partial class DeleteProductViewModel : ViewModelBase
{
    private Window _window;

    public DeleteProductViewModel(Window window)
    {
        _window = window;
    }

    [RelayCommand]
    public void Delete(Product product)
    {
        Product.DeleteProduct(product);
        _window.Close(product);
    }

    [RelayCommand]
    public void Cancel()
    {
        _window.Close();
    }
}