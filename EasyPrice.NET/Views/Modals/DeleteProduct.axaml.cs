using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using EasyPrice.NET.Models;
using EasyPrice.NET.ViewModels;

namespace EasyPrice.NET.Views;

public partial class DeleteProduct : Window
{
    private DeleteProductViewModel _viewModel;
    
    public DeleteProduct(Product product)
    {
        InitializeComponent();
        _viewModel = new DeleteProductViewModel(this, product);
        DataContext = _viewModel;
    }
}