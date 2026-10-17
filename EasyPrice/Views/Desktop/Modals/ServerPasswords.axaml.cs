using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using EasyPrice.ViewModels.Desktop;

namespace EasyPrice.Views.Desktop.Modals;

public partial class ServerPasswords : Window
{
    public ServerPasswords()
    {
        var ViewModel = new ServerPasswordsViewModel(this);
        DataContext = ViewModel;
        InitializeComponent();
    }
}