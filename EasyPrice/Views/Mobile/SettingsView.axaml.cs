using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using EasyPrice.ViewModels.Mobile;

namespace EasyPrice.Views.Mobile;

public partial class SettingsView : UserControl
{
    public SettingsView()
    {
        var vm = new SettingsViewModel();
        DataContext = vm;
        InitializeComponent();
    }
}