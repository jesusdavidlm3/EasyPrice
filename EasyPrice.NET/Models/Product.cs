using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using EasyPrice.NET.Services;
using Microsoft.Data.Sqlite;

namespace EasyPrice.NET.Models;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public double PackPrice { get; set; }
    public int PackQuantity { get; set; }
    public int Gain { get; set; }

    public Product(Guid id, string name, double packPrice, int packQuantity, int gain)
    {
        Id = id;
        Name = name;
        PackPrice = packPrice;
        PackQuantity = packQuantity;
        Gain = gain;
    }

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
                int.Parse(reader["Gain"].ToString())
            );
            products.Add(p);
        }
        return products;
    }
    
    public static Product AddProduct(string name, double packPrice, int packQuantity, int gain)
    {
        var product = new Product(Guid.NewGuid(), name, packPrice, packQuantity, gain);
        
        string query = "INSERT INTO products VALUES (@Id, @Name, @PackPrice, @PackQuantity, @Gain)";
        List<(string, object)> parameters =
        [
            ("@Id", product.Id.ToString()),
            ("@Name", product.Name),
            ("@PackPrice", product.PackPrice),
            ("@PackQuantity", product.PackQuantity),
            ("@Gain", product.Gain)
        ];
        DBService.Execute(query, parameters);
        
        return product;
    }

    public static void UpdateProduct(Product product)
    {
        string query = "UPDATE poducts SET PackPrice = @PackPrice, PackQuantity = @PackQuantity, Gain = @Gain WHERE Id = @Id";
        List< (string, object)> parameters =
        [
            ("@PackPrice", product.PackPrice.ToString()),
            ("@PackQuantity", product.PackQuantity.ToString()),
            ("@Gain", product.Gain.ToString()),
            ("@Id", product.Id.ToString())
        ];
    }

    public static void DeleteProduct(Product product)
    {
        string query = "DELETE FROM products WHERE Id = @Id";
        List<(string, object)> parameters = [("@Id", product.Id.ToString())];
        DBService.Execute(query, parameters);
    }


    public double GetValueDolar
    {
        get
        {
            double unitPrice = PackPrice / PackQuantity;
            double operableGain = Gain / 100d;
            double unitValue = unitPrice + (operableGain * unitPrice);
            return unitValue;
        }
    }
    
    public double GetValueBs
    {
        get
        {
            double unitValueDolar = GetValueDolar;
            double unitValueBs = unitValueDolar * DBService.settings.DolarPrice;
            return unitValueBs;
        }
    }
}