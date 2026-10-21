using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using EasyPrice.ViewModels.Mobile;
using EasyPrice.ViewModels.Desktop;
using MainViewModel = EasyPrice.ViewModels.Desktop.MainViewModel;

namespace EasyPrice.Views.Mobile;

public partial class HomeView : UserControl
{
    public HomeView()
    {
        var vm = new HomeViewModel();
        DataContext = vm;
        InitializeComponent();
    }
}