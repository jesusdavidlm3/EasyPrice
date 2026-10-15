namespace Models;

public class Product
{
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public double PackPrice { get; set; }
    public int PackQuantity { get; set; }
    public int Gain { get; set; }
    
    public Product(){}
    
    public Product(Guid id, string name, double packPrice, int packQuantity, int gain)
    {
        Id = id;
        Name = name;
        PackPrice = packPrice;
        PackQuantity = packQuantity;
        Gain = gain;
    }
    
    public Product(string name, double packPrice, int packQuantity, int gain)
    {
        Id = Guid.NewGuid();
        Name = name;
        PackPrice = packPrice;
        PackQuantity = packQuantity;
        Gain = gain;
    }

    public Product(Product newProduct)
    {
        Id = newProduct.Id ?? Guid.NewGuid();
        Name = newProduct.Name;
        PackPrice = newProduct.PackPrice;
        PackQuantity = newProduct.PackQuantity;
        Gain = newProduct.Gain;
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