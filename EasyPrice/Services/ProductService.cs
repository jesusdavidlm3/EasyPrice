using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using Models;

namespace EasyPrice.Services;

public static class ProductService
{
    public static ObservableCollection<Product> ProductList { get; set; }
    
    public static ObservableCollection<Product> GetAll()
    {
        var products = new ObservableCollection<Product>();
        string query = "SELECT * FROM products";
        var reader = DBService.Query(query);
        while (reader.Read())
        {
            var p = new Product
            (
                reader.GetGuid("Id"),  
                reader.GetString("Name"), 
                reader.GetDouble("PackPrice"),  
                reader.GetInt32("PackQuantity"), 
                reader.GetInt32("Gain"),
                reader.GetDateTime("LastUpdate")
            );
            products.Add(p);
        }
        return products;
    }
    
    public static Product AddProduct(string name, double packPrice, int packQuantity, int gain)
    {
        var product = new Product(Guid.NewGuid(), name, packPrice, packQuantity, gain, DateTime.Now);
        
        string query = "INSERT INTO products VALUES (@Id, @Name, @PackPrice, @PackQuantity, @Gain, @LastUpdate)";
        List<(string, object)> parameters =
        [
            ("@Id", product.Id!),
            ("@Name", product.Name),
            ("@PackPrice", product.PackPrice),
            ("@PackQuantity", product.PackQuantity),
            ("@Gain", product.Gain),
            ("@LastUpdate", product.LastUpdate)!
        ];
        DBService.Execute(query, parameters);
        
        return product;
    }
    
    public static void AddProduct(Product newProduct)
    {
        string query = "INSERT INTO products VALUES (@Id, @Name, @PackPrice, @PackQuantity, @Gain, @LastUpdate)";
        List<(string, object)> parameters =
        [
            ("@Id", newProduct.Id ?? Guid.NewGuid()),
            ("@Name", newProduct .Name),
            ("@PackPrice", newProduct.PackPrice),
            ("@PackQuantity", newProduct.PackQuantity),
            ("@Gain", newProduct.Gain),
            ("@LastUpdate", newProduct.LastUpdate)!
        ];
        DBService.Execute(query, parameters);
    }

    public static void UpdateProduct(Product product)
    {
        string query = "UPDATE products SET PackPrice = @PackPrice, PackQuantity = @PackQuantity, Gain = @Gain, LastUpdate = @LastUpdate WHERE Id = @Id";
        List< (string, object)> parameters =
        [
            ("@PackPrice", product.PackPrice),
            ("@PackQuantity", product.PackQuantity),
            ("@Gain", product.Gain),
            ("@LastUpdate", DateTime.Now),
            ("@Id", product.Id)!
        ];
        DBService.Execute(query, parameters);
    }

    public static void DeleteProduct(Product product)
    {
        string query = "DELETE FROM products WHERE Id=@Id";
        List<(string, object)> parameters = [("@Id", product.Id)!];
        DBService.Execute(query, parameters);
    }
}