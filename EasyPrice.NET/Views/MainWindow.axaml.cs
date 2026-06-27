using Avalonia.Controls;
using EasyPrice.NET.Services;
using EasyPrice.NET.ViewModels;

namespace EasyPrice.NET.Views;

public partial class MainWindow : Window
{
    private MainWindowViewModel? viewModel;
    
    public MainWindow()
    {
        viewModel = new MainWindowViewModel();
        DataContext = viewModel;
        var showEditProduct = new ShowEditProductService(this);
        var showAddProduct = new ShowAddProductService(this, viewModel);
        var showSettings = new ShowSettingsService(this);
        var showDeleteProduct = new ShowDeleteProductService(this, viewModel);
        InitializeComponent();

    }
}