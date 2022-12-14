using System;
namespace LaptopWebshop
{
    public class Manager : RegisteredUser
    {
        public static readonly string menu = "\r\n  1.List products\r\n  2.Add new product\r\n  3.Modify product\r\n  4.Delete product\r\n  5.Stats\r\n  6.Change lucky-wheel's discounts\r\n  7.Logout\r\n  8.Exit";

        public Manager(string username, string name, string password, DateOnly birth) : base(username, name, password, birth) { }

        public Manager(string line) : base(line) { }

        // public Manager(User u) : base(u.username, u.name, u.password, u.birth) { }

        public override string Type() => "Manager";
        
        public void addNewPrize(int prize)
        {
            LuckyWheel.addNewPrize(prize);
        }

        //deletes all prize with the same value
        public void deletePrize(int prize)
        {
            LuckyWheel.deletePrize(prize);
        }
    }
}

