using Models;

namespace ServerSideAPI.Interfaces;

public interface IProductsRepo
{
    public List<Product> GetProducts();
    public List<Product> GetProducts(DateTime date);
    public Product SaveProduct(Product product);
    public Product EditProduct(Product product);
    public DateTime DeleteProduct(Guid id);
}