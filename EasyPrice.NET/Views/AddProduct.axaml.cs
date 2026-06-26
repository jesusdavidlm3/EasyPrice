using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using EasyPrice.NET.ViewModels;

namespace EasyPrice.NET.Views;

public partial class AddProduct : Window
{
    private AddProductViewModel viewModel;
    
    public AddProduct(MainWindowViewModel ownerVm)
    {
        InitializeComponent();
        viewModel = new AddProductViewModel(this, ownerVm);
        DataContext = viewModel;
    }
}