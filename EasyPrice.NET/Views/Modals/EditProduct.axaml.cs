using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using EasyPrice.NET.Models;
using EasyPrice.NET.ViewModels;

namespace EasyPrice.NET.Views;

public partial class EditProduct : Window
{
    public EditProduct(Product product)
    {
        var ViewModel = new EditProductViewModel(this, product);
        DataContext = ViewModel;
        InitializeComponent();
    }
}