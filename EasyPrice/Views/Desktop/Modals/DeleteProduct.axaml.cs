using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using EasyPrice.Models;
using EasyPrice.ViewModels.Desktop;

namespace EasyPrice.Views.Desktop;

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