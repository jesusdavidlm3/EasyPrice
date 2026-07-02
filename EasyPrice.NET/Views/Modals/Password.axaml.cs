using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using EasyPrice.NET.ViewModels;

namespace EasyPrice.NET.Views.Modals;

public partial class Password : Window
{
    public Password()
    {
        var viewModel = new PasswordViewModel(this);
        DataContext = viewModel;
        InitializeComponent();
    }
}