using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using EasyPrice.Models;
using EasyPrice.ViewModels;

namespace EasyPrice.Views;

public partial class EditProduct : Window
{
    public EditProduct(Product product)
    {
        var ViewModel = new EditProductViewModel(this, product);
        DataContext = ViewModel;
        InitializeComponent();
    }
}