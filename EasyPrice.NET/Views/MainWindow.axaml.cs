using ShadUI;
using EasyPrice.NET.Services;

namespace EasyPrice.NET.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        var showEditProduct = new ShowEditProductService(this);
        var showAddProduct = new ShowAddProductService(this);
        var showSettings = new ShowSettingsService(this);
    }
}