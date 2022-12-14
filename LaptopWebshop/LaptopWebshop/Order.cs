namespace LaptopWebshop;

public class Order
{
    static int count = 0;

    public readonly int id;
    
    public Order()
    {
        id = ++count;

        this.username = string.Empty;
        cart = new List<Laptop>();
        date = DateTime.Now;
        address = string.Empty;
    }
    
    public Order(string line)
    {
        id = ++count;
        
        string[] t = line.Split('#');

        if (t.Length < 4)
        {
            this.username = string.Empty;
            cart = new List<Laptop>();
            date = DateTime.Now;
            address = string.Empty;
            return;
        }
            

        this.username = t[0];

        cart = new List<Laptop>();
        //string[] laptops = t[1].Split("|");
        //foreach (string l in laptops)
        //    cart.Add(new Laptop(l));

        cart = t[1].Split("|").Select(a => new Laptop(a)).ToList();
        
        DateTime.TryParse(t[2], out DateTime _date);
        this.date = _date;
        
        this.address = t[3];
    }
    
    //public int id { get; set; }
    public string username { get; set; }
    public List<Laptop> cart { get; private set; }
    public DateTime date { get; set; }
    public string address { get; set; }
    
    public int getSum()
    {
        return cart.Sum(l => l.price);
    }

    public string FormatToTxt() => string.Format("{0}#{1}#{2}#{3}", username, string.Join("|", cart.Select(a => a.FormatToTxt()).ToList()), date.ToString("yyyy.MM.dd HH:mm:ss"), address );

    public override string ToString() => string.Format("Username: {0}, Laptops: {1}, Date: {2}, Address: {3}", username, string.Join("|", cart.Select(a => a.ToString()).ToList()), date.ToString("yyyy.MM.dd HH:mm:ss"), address);
}