using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using EasyPrice.NET.ViewModels;

namespace EasyPrice.NET.Views;

public partial class SettingsWindow : Window
{
    public SettingsWindow()
    {
        var viewModel = new SettingsWindowViewModel(this);
        DataContext = viewModel;
        InitializeComponent();
    }
}