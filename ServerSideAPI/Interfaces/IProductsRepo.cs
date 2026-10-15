using Models;

namespace ServerSideAPI.Interfaces;

public interface IProductsRepo
{
    public List<Product> GetProducts();
    public Product SaveProduct(Product product);
    public Product EditProduct(Product product);
    public List<Product> FilterProductsByName(string name);
    public void DeleteProduct(Guid id);
}