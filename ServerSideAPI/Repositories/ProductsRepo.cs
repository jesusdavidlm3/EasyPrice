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
        var dbResponse = _dbService.Query("SELECT * FROM Products WHERE Deleted = 0");
        while (dbResponse.Read())
        {
            var id = dbResponse.GetGuid("Id");
            var name = dbResponse.GetString("Name");
            var packPrice = dbResponse.GetDouble("PackPrice");
            var packQuantity = dbResponse.GetInt32("PackQuantity");
            var gain = dbResponse.GetInt32("Gain");
            var lastUpdate = dbResponse.GetDateTime("LastUpdate");
            
            lastUpdate.Deconstruct(out DateOnly date, out TimeOnly time);
            var product = new Product(id, name, packPrice, packQuantity, gain, new DateTime(date, time, DateTimeKind.Utc));
            res.Add(product);
        }
        return res;
    }
    
    public List<Product> GetProducts(DateTime date)
    {
        var res = new List<Product>();
        var queryParams = new List<(string, object)>
        {
            ("@Date", date)
        };
        var dbResponse = _dbService.Query("SELECT * FROM Products WHERE LastUpdate > @Date", queryParams);
        while (dbResponse.Read())
        {
            var id = dbResponse.GetGuid("Id");
            var name = dbResponse.GetString("Name");
            var packPrice = dbResponse.GetDouble("PackPrice");
            var packQuantity = dbResponse.GetInt32("PackQuantity");
            var gain = dbResponse.GetInt32("Gain");
            var lastUpdate = dbResponse.GetDateTime("LastUpdate");
            var deleted = dbResponse.GetBoolean("Deleted");
            
            lastUpdate.Deconstruct(out DateOnly dateOnly, out TimeOnly timeOnly);
            var product = new Product(id, name, packPrice, packQuantity, gain, new DateTime(dateOnly, timeOnly, DateTimeKind.Utc), deleted);
            res.Add(product);
        }
        return res;
    }

    public Product SaveProduct(Product product)
    {
        product.Id = Guid.NewGuid();
        product.LastUpdate = DateTime.Now;
        var queryParams = new List<(string, object)>
        {
            ("@Id", product.Id.ToString())!,
            ("@Name", product.Name),
            ("@PackPrice", product.PackPrice),
            ("@PackQuantity", product.PackQuantity),
            ("@Gain", product.Gain),
            ("@LastUpdate", DateTime.Now.ToUniversalTime()),
        };
        _dbService.Execute("INSERT INTO products(Id, Name, PackPrice, PackQuantity, Gain, LastUpdate) VALUES(@Id, @Name, @PackPrice, @PackQuantity, @Gain, @LastUpdate)", queryParams);
        return product;
    }

    public Product EditProduct(Product product)
    {
        product.LastUpdate = DateTime.Now;
        var queryParams = new List<(string, object)>
        {
            ("@Id", product.Id.ToString()!),
            ("@PackPrice", product.PackPrice),
            ("@PackQuantity", product.PackQuantity),
            ("@Gain", product.Gain),
            ("@LastUpdate", product.LastUpdate.Value.ToUniversalTime()),
        };
        _dbService.Execute("UPDATE products SET PackPrice=@PackPrice, PackQuantity=@PackQuantity, Gain=@Gain, LastUpdate=@LastUpdate WHERE Id=@Id", queryParams);
        return product;
    }

    public DateTime DeleteProduct(Guid id)
    {
        var updated = DateTime.Now.ToUniversalTime();
        var queryParams = new List<(string, object)>
        {
            ("@Id", id.ToString()),
            ("@Deleted", true),
            ("@LastUpdate", updated)
        };
        _dbService.Execute("UPDATE products SET Deleted=@Deleted, LastUpdate=@LastUpdate WHERE Id=@Id", queryParams);
        return updated;
    }
}
