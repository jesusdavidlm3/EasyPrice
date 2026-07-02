using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using EasyPrice.NET.ViewModels;

namespace EasyPrice.NET.Views;

public partial class Administration : Window
{
    public Administration(MainWindowViewModel ownerVm)
    {
        var viewModel = new AdministrationViewModel(this, ownerVm);
        DataContext = viewModel;
        InitializeComponent();
    }
}