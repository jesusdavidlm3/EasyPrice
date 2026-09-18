using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using EasyPrice.ViewModels;

namespace EasyPrice.Views.Modals;

public partial class ServerSettings : Window
{
    public ServerSettings()
    {
        var viewModel = new ServerSettingsViewModel(this);
        DataContext = viewModel;
        InitializeComponent();
    }
}