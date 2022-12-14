using System;
using System.Security.Cryptography;
using System.Text;
using LaptopWebshoop;

namespace LaptopWebshop
{
    public class RegisteredUser : User
    {
        public string username { get; protected set; }
        public string name { get; protected set; }
        public string password { get; protected set; }
        public DateOnly birth { get; protected set; }
        public DateTime lastSpin { get; protected set; }
        public int discount { get; protected set; }

        public static readonly string menu =
            "  1.List laptops\r\n  2.Show cart\r\n  3.Add to cart\r\n  4.Remove from cart\r\n  5.Purchase\r\n  6.Show my prize\r\n  7.Spin\r\n  8.Logout\r\n  9.Exit";
        
        //TODO létrehozásnál beállítani az order adattagot filebeolvasáskor
        public RegisteredUser(string username, string name, string password, DateOnly birth) : base()
        {
            this.username = username;
            this.name = name;
            this.password = password;
            this.birth = birth;

            //Default értékek beállítása
            this.lastSpin = DateTime.MinValue; //Alapból a legkorábbi dátumot tároljuk, így biztosítva, hogy a még sohasem pörgetett felhasználónál is müködjön majd az ellenőrzés
            this.discount = 0;
        }
        
        public RegisteredUser(string line) : base()
        {
            string[] t = line.Split('#');

            this.username = t[0];
            this.name = t[1];
            this.password = t[2];

            DateOnly.TryParse(t[3], out DateOnly tempDO);
            this.birth = tempDO;

            DateTime.TryParse(t[4], out DateTime tempDT);
            this.lastSpin = tempDT;

            int.TryParse(t[5], out int tempDisc);
            this.discount = tempDisc;

            order = new Order(t[6]);
        }
        
        public bool IsIt(string username, string password) => this.username.Equals(username) && this.password.Equals(password);

        public override string ToString() => string.Format("{0}\r\n -Name: {1}\r\n -Password: {2}\r\n -Birthday: {3}\r\n -Type: {4}\r\n -Last spin: {5}\r\n -Discount: {6}", username, name, password, birth.ToString("yyyy.MM.dd"), Type(), lastSpin.ToString("yyyy.MM.dd HH:mm:ss"), discount);

        //Formázott kiírás a txt-be mentéshez
        public string FormatToTxt() => string.Format("{0}#{1}#{2}#{3}#{4}#{5}#{6}", username, name, password, birth.ToString("yyyy.MM.dd"), lastSpin.ToString("yyyy.MM.dd HH:mm:ss"), discount.ToString(), order.cart.Count == 0 ? string.Empty : order.FormatToTxt());
        
        public void spin()
        {
            
            if (!(DateTime.Now.Subtract(lastSpin).TotalHours >= 24))
            {
                Program.WriteError("You can only spin once a day!");
                return;
            }
            List<int> prizes = LuckyWheel.getPrizes();
            if (prizes.Count > 0)
            {
                Random rnd = new Random();
                int prizesindex = rnd.Next(0, prizes.Count);
                discount = prizes[prizesindex];
                lastSpin = DateTime.Now;
                Program.WriteSucces($"Congrats! You won a {discount}% discount!");
                return;
            }
            Program.WriteError("Oops! There was an error. Better luck next time!");
        }

        public override string Type() => "Registered user";

        public static User login()
        {
            string username = string.Empty;
            string password = string.Empty;

            Console.WriteLine("\r\nLogin form:");

            Console.Write("Username: ");
            username = Console.ReadLine()!;
            while (!UserStorage.IsValidUsername(username))
            {
                if (username == "0")
                {
                    Console.WriteLine("Back to the menu");
                    return new Guest();
                }
                Console.WriteLine("Invalid username! Try again or write '0' to exit:");
                Console.Write("Username: ");
                username = Console.ReadLine()!;
            }

            Console.Write("Password: ");
            password = Console.ReadLine()!;
            while (!UserStorage.IsValid(username, password))
            {
                if (password == "0")
                {
                    Console.WriteLine("Back to the menu");
                    return new Guest();
                }
                Console.WriteLine("Invalid password! Try again: or write '0' to exit:");
                Console.Write("Password: ");
                password = Console.ReadLine()!;
            }

            Console.WriteLine(UserStorage.GetUser(username).GetType().ToString());
            return UserStorage.GetUser(username);
        }

        public void Purchase()
        {
            if (order.cart.Count == 0)
            {
                Program.WriteError("Your cart is empty!");
                return;
            }

            order.username = username;
            // foreach (var item in order.cart)
            // {
            //     item.price = item.price * (1 - discount);
            // }
            int price = order.getSum() * (1 - discount);
            string address = string.Empty;
            Program.GetInput(ref address,"Destination address");
            order.address = address;

            order.date = DateTime.Now;
            
            OrderStorage.AddOrder(order);

            order = new Order();
            Program.WriteSucces("\nSuccessfully ordered!");
            Console.WriteLine($"\r\nYou have to pay: ${price}");

        }
    }
}