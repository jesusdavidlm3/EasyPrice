using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Models;

namespace EasyPrice.Services;

public static class ProductService
{
    public static ObservableCollection<Product> productList { get; set; }
    
    public static ObservableCollection<Product> GetAll()
    {
        var products = new ObservableCollection<Product>();
        string query = "SELECT * FROM products";
        var reader = DBService.Query(query);
        while (reader.Read())
        {
            var p = new Product
            (
                Guid.Parse(reader["Id"].ToString()),  
                reader["Name"].ToString(), 
                float.Parse(reader["PackPrice"].ToString()),  
                int.Parse(reader["PackQuantity"].ToString()), 
                int.Parse(reader["Gain"].ToString()),
                DateTime.Parse(reader["LastUpdate"].ToString())
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
            ("@Id", product.Id.ToString()),
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
            ("@Id", newProduct.Id.ToString() ?? Guid.NewGuid().ToString()),
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
            ("@PackPrice", product.PackPrice.ToString()),
            ("@PackQuantity", product.PackQuantity.ToString()),
            ("@Gain", product.Gain.ToString()),
            ("@LastUpdate", DateTime.Now),
            ("@Id", product.Id.ToString())
        ];
        DBService.Execute(query, parameters);
    }

    public static void DeleteProduct(Product product)
    {
        string query = "DELETE FROM products WHERE Id = @Id";
        List<(string, object)> parameters = [("@Id", product.Id.ToString())];
        DBService.Execute(query, parameters);
    }
}