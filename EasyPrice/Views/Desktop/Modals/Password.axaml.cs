using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using EasyPrice.ViewModels.Desktop;

namespace EasyPrice.Views.Desktop.Modals;

public partial class Password : Window
{
    public Password()
    {
        var viewModel = new PasswordViewModel(this);
        DataContext = viewModel;
        InitializeComponent();
    }
}