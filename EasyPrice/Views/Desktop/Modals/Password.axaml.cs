using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using EasyPrice.ViewModels;

namespace EasyPrice.Views.Modals;

public partial class Password : Window
{
    public Password()
    {
        var viewModel = new PasswordViewModel(this);
        DataContext = viewModel;
        InitializeComponent();
    }
}