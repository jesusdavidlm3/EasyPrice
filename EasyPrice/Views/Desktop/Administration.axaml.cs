using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using EasyPrice.ViewModels.Desktop;

namespace EasyPrice.Views.Desktop;

public partial class Administration : Window
{
    public Administration(MainViewModel ownerVm)
    {
        var viewModel = new AdministrationViewModel(this, ownerVm);
        DataContext = viewModel;
        InitializeComponent();
    }
}