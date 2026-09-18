using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using EasyPrice.ViewModels;

namespace EasyPrice.Views.Modals;

public partial class ChangePassword : Window
{
    public ChangePassword()
    {
        var viewModel = new ChangePasswordViewModel(this);
        DataContext = viewModel;
        InitializeComponent();
    }
}