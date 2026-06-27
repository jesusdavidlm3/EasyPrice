using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using EasyPrice.NET.ViewModels;

namespace EasyPrice.NET.Views;

public partial class DeleteProduct : Window
{
    public DeleteProduct()
    {
        var ViewModel = new DeleteProductViewModel(this);
        DataContext = ViewModel;
        InitializeComponent();
    }
}