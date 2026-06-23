using EasyPrice.NET.Views;

namespace EasyPrice.NET.Services;

public class ShowEditProductService
{
    public ShowEditProductService()
    {
    }

    public void ShowEditProduct()
    {
        var window = new EditProduct();
        window.Show();
    }
}