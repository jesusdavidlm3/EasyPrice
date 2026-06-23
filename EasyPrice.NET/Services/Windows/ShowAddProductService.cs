using EasyPrice.NET.Views;

namespace EasyPrice.NET.Services;

public class ShowAddProductService
{
    public ShowAddProductService()
    {
        
    }

    public void showAddProduct()
    {
        var window = new AddProduct();
        window.Show();
    }
}