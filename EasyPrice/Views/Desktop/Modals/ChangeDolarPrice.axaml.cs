using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using EasyPrice.ViewModels;

namespace EasyPrice.Views;

public partial class ChangeDolarPrice : Window
{
    public ChangeDolarPrice(AdministrationViewModel ownerVm)
    {
        var viewModel = new ViewModels.ChangeDolarPrice(this,  ownerVm);
        DataContext = viewModel;
        InitializeComponent();
    }
}