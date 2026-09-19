using System.Threading.Tasks;
using Avalonia.Controls;
using EasyPrice.Views.Desktop.Modals;

namespace EasyPrice.Services;

public class ProtectedViewServiceBase
{
    protected Window _window;
    
    protected async Task ShowProtected(Window ProtectedWindow)
    {
        var passwordModal = new Password();
        var res = await passwordModal.ShowDialog<bool?>(_window);
        if (res is true)
        {
            await ProtectedWindow.ShowDialog(_window);
        }
    }
}