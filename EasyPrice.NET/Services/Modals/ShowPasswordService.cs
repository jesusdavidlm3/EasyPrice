using System.Threading.Tasks;
using Avalonia.Controls;
using EasyPrice.NET.Views.Modals;

namespace EasyPrice.NET.Services.Windows;

public class ShowPasswordService
{
    private Window _ownerWindow;
    
    public ShowPasswordService(Window ownerWindow)
    {
        _ownerWindow = ownerWindow;
    }
    
    public async Task<bool?> ShowPassword()
    {
        var window = new Password();
        var res = await window.ShowDialog<bool?>(_ownerWindow);
        return res;
    }
}