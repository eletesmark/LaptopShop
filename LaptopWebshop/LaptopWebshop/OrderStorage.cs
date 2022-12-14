namespace LaptopWebshop;

public class OrderStorage
{
    //lehet dict is tőlem de nem hiszem hogy szükséges
    private static List<Order> Orders = new();

    public static List<Order> GetOrders() => new(Orders);
    
    public static Order GetOrder(int id) => Orders.FirstOrDefault(x => x.id == id);

    public static void AddOrder(Order order) => Orders.Add(order);

    public static void DeleteOrder(int id) => Orders.Remove(GetOrder(id));

    public static void ReadOrdersTxt()
    {
        //TODO
    }

    public static void WriteOrdersTxt()
    {
        //TODO
    }
    
}