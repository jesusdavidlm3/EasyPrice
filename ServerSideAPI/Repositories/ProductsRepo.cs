using System.Data;
using Models;
using ServerSideAPI.Interfaces;
using ServerSideAPI.Services;

namespace ServerSideAPI.Repositories;

public class ProductsRepo : IProductsRepo
{
    private DbService _dbService { get; set; }
    
    public ProductsRepo(DbService dbService)
    {
        _dbService = dbService;
    }
    
    public List<Product> GetProducts()
    {
        var res = new List<Product>();
        var dbResponse = _dbService.Query("SELECT * FROM Products");
        while (dbResponse.Read())
        {
            var id = dbResponse.GetGuid("Id");
            var name = dbResponse.GetString("Name");
            var packPrice = dbResponse.GetDouble("PackPrice");
            var packQuantity = dbResponse.GetInt32("PackQuantity");
            var gain = dbResponse.GetInt32("Gain");
            var product = new Product(id, name, packPrice, packQuantity, gain);
            res.Add(product);
        }
        return res;
    }

    public Product SaveProduct(Product product)
    {
        product.Id = Guid.NewGuid();
        var queryParams = new List<(string, object)>
        {
            ("@Id", product.Id.ToString())!,
            ("@Name", product.Name),
            ("@PackPrice", product.PackPrice),
            ("@PackQuantity", product.PackQuantity),
            ("@Gain", product.Gain)
        };
        _dbService.Execute("INSERT INTO products(Id, Name, PackPrice, PackQuantity, Gain) VALUES(@Id, @Name, @PackPrice, @PackQuantity, @Gain)", queryParams);
        return product;
    }

    public Product EditProduct(Product product)
    {
        var queryParams = new List<(string, object)>
        {
            ("@Id", product.Id.ToString()!),
            ("@Name", product.Name),
            ("@PackPrice", product.PackPrice),
            ("@PackQuantity", product.PackQuantity),
            ("@Gain", product.Gain)
        };
        _dbService.Execute("UPDATE products SET Name=@Name, PackPrice=@PackPrice, PackQuantity=@PackQuantity, Gain=@Gain WHERE Id=@Id", queryParams);
        return product;
    }

    public List<Product> FilterProductsByName(string productName)
    {
        var res = new List<Product>();
        var queryParams = new List<(string, object)>
        {
            ("@Name", productName)
        };
        var dbResponse = _dbService.Query("SELECT * FROM products WHERE Name = @Name", queryParams);
        while (dbResponse.Read())
        {
            var id = dbResponse.GetGuid("Id");
            var name = dbResponse.GetString("Name");
            var packPrice = dbResponse.GetDouble("PackPrice");
            var packQuantity = dbResponse.GetInt32("PackQuantity");
            var gain = dbResponse.GetInt32("Gain");
            var product = new Product(id, name, packPrice, packQuantity, gain);
            res.Add(product);
        }
        return res;
    }

    public void DeleteProduct(Guid id)
    {
        var queryParams = new List<(string, object)>
        {
            ("@Id", id)
        };
        _dbService.Execute("DELETE FROM products WHERE Id = @Id", queryParams);
    }
}
