using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using EasyPrice.NET.ViewModels;

namespace EasyPrice.NET.Views;

public partial class ChangeDolarPrice : Window
{
    public ChangeDolarPrice(AdministrationViewModel ownerVm)
    {
        var viewModel = new ViewModels.ChangeDolarPrice(this,  ownerVm);
        DataContext = viewModel;
        InitializeComponent();
    }
}