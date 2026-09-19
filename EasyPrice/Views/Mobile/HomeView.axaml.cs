using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using EasyPrice.ViewModels.Mobile;

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