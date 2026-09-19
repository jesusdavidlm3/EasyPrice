using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using EasyPrice.ViewModels.Mobile;

namespace EasyPrice.Views.Mobile;

public partial class MainView : UserControl
{
    public MainView()
    {
        var vm = new MainViewModel();
        DataContext = vm;
        InitializeComponent();
    }
}