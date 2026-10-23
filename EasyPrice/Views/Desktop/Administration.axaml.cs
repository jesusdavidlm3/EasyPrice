using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using EasyPrice.ViewModels.Desktop;
using Models;

namespace EasyPrice.Views.Desktop;

public partial class Administration : Window
{
    public Administration(MainViewModel ownerVm, Action<List<Product>> processUpdates)
    {
        var viewModel = new AdministrationViewModel(this, ownerVm, processUpdates);
        DataContext = viewModel;
        InitializeComponent();
    }
}