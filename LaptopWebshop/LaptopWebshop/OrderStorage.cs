using System.Text;

namespace LaptopWebshop;

public class OrderStorage
{
    //lehet dict is tőlem de nem hiszem hogy szükséges
    private static List<Order> Orders = new();

    public static List<Order> GetOrders() => new(Orders);
    
    public static Order GetOrder(int id) => Orders.FirstOrDefault(x => x.id == id);

    public static void AddOrder(Order order)
    {
        Orders.Add(order);
        WriteOrdersToTxt();
    }

    public static void DeleteOrder(int id) => Orders.Remove(GetOrder(id));

    public static void ReadOrdersTxt()
    {
        Orders.Clear();

        if (File.Exists("orders.txt"))
            Orders.AddRange(File.ReadAllLines("orders.txt").Where(a => a.Split(',').Length == 4).Select(a => new Order(a)).ToList());
    }

    public static void WriteOrdersToTxt()
    {
        TextWriter orders_Txt = StreamWriter.Null; //Close() miatt kell adni neki valami alap erteket

        try
        {
            orders_Txt = new StreamWriter("orders.txt", false, Encoding.UTF8);
            
            orders_Txt.WriteLine(string.Join("\r\n", Orders.Select(a => a.FormatToTxt()).ToList()));
        }
        catch (IOException ioex)
        {

        }
        catch (Exception e)
        {

        }
        finally
        {
            orders_Txt.Close();
        }
    }
    
}