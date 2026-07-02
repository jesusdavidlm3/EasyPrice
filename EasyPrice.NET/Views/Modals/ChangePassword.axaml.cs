using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using EasyPrice.NET.ViewModels;

namespace EasyPrice.NET.Views.Modals;

public partial class ChangePassword : Window
{
    public ChangePassword()
    {
        var viewModel = new ChangePasswordViewModel(this);
        DataContext = viewModel;
        InitializeComponent();
    }
}