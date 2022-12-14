namespace LaptopWebshop;

public class Order
{
    public Order()
    {
        
    }
    
    public int id { get; set; }
    public string username { get; set; }
    public List<Laptop> cart { get; private set; }
    public DateTime date { get; set; }
    public string address { get; set; }
    
    int getSum()
    {
        return cart.Sum(l => l.price);
    }
    
}