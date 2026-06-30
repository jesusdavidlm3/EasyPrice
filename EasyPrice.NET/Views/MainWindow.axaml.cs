using Avalonia.Controls;
using EasyPrice.NET.Services;
using EasyPrice.NET.ViewModels;

namespace EasyPrice.NET.Views;

public partial class MainWindow : Window
{
    private MainWindowViewModel? viewModel;
    
    public MainWindow()
    {
        viewModel = new MainWindowViewModel(this);
        DataContext = viewModel;
        InitializeComponent();

    }
}