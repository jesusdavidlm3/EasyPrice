using Avalonia.Controls;
using EasyPrice.Services;
using EasyPrice.ViewModels;

namespace EasyPrice.Views;

public partial class MainView : Window
{
    private MainViewModel? viewModel;
    
    public MainView()
    {
        viewModel = new MainViewModel(this);
        DataContext = viewModel;
        InitializeComponent();

    }
}