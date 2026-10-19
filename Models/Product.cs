using System.Text.Json.Serialization;

namespace Models;

public class Product
{
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public double PackPrice { get; set; }
    public int PackQuantity { get; set; }
    public int Gain { get; set; }
    public DateTime? LastUpdate { get; set; }
    public bool Deleted { get; set; }
    
    public Product(){}
    
    public Product(Guid id, string name, double packPrice, int packQuantity, int gain, DateTime lastUpdate, bool? deleted = false)
    {
        Id = id;
        Name = name;
        PackPrice = packPrice;
        PackQuantity = packQuantity;
        Gain = gain;
        LastUpdate = lastUpdate;
        Deleted = deleted ?? false;
    }
    
    public Product(string name, double packPrice, int packQuantity, int gain, DateTime lastUpdate, bool? deleted = false)
    {
        Id = Guid.NewGuid();
        Name = name;
        PackPrice = packPrice;
        PackQuantity = packQuantity;
        Gain = gain;
        LastUpdate =  lastUpdate;
        Deleted = deleted ??  false;
    }

    public Product(Product newProduct)
    {
        Id = newProduct.Id ?? Guid.NewGuid();
        Name = newProduct.Name;
        PackPrice = newProduct.PackPrice;
        PackQuantity = newProduct.PackQuantity;
        Gain = newProduct.Gain;
        LastUpdate = newProduct.LastUpdate ?? DateTime.Now;
        Deleted = newProduct.Deleted;
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
        set{}
    }
}