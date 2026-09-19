using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using EasyPrice.ViewModels.Desktop;

namespace EasyPrice.Views.Desktop;

public partial class AddProduct : Window
{
    private AddProductViewModel _viewModel;
    
    public AddProduct()
    {
        InitializeComponent();
        _viewModel = new AddProductViewModel(this);
        DataContext = _viewModel;
    }
}