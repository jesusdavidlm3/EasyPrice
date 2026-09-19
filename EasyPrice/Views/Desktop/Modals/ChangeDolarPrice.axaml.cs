using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using EasyPrice.ViewModels.Desktop;

namespace EasyPrice.Views.Desktop;

public partial class ChangeDolarPrice : Window
{
    public ChangeDolarPrice(AdministrationViewModel ownerVm)
    {
        var viewModel = new ChangeDolarPriceViewModel(this,  ownerVm);
        DataContext = viewModel;
        InitializeComponent();
    }
}