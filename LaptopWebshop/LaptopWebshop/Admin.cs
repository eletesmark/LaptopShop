using LaptopWebshoop;
using System;


namespace LaptopWebshop
{
    public class Admin : Manager
    {
        public static readonly string menu =
            "  1.Add manager rule to user\r\n  2.List orders\r\n  3.List users\r\n  4.Search user\r\n  5.Delete user\r\n  6.List laptops\r\n  7.Show cart\r\n  8.Add to cart\r\n  9.Remove from cart\r\n  10.Show prizes\r\n  11.Spin\r\n  12.Add new product\r\n  13.Modify product\r\n  14.Delete product\r\n  15.Stats\r\n  16.Change lucky-wheel's discounts\r\n  17.Logout\r\n  18.Exit";

        public Admin(string username, string name, string password, DateOnly birth) : base(username, name, password,
            birth)
        {
        }

        public Admin(string line) : base(line)
        {
        }

        public override string Type() => "Admin";


        //BUG rosszul menti el az új managert: más a jelszó mint az eredeti
        public static void AddManagerRole(string username)
        {
            RegisteredUser user = UserStorage.GetUser(username) ;
            if (user == null) return;
            if (user.GetType() == typeof(RegisteredUser))
            {
                user = new Manager(user.username, user.name, user.password, user.birth);
                UserStorage.AddUser(user);
            }
            else throw new Exception("You cannot give manager role to this user!");
            Program.WriteSucces($"Manager role successfully added to {username}!");
        }

        public static void ListUsers()
        {
            List<RegisteredUser> users = UserStorage.GetUsers();
            if (users is null)
            {
                throw new Exception("There are no users!");
                //return null;
            }

            Console.WriteLine("Users: ");
            foreach (var u in users)
            {
                Console.WriteLine($"{u.Type()} | username: {u.username} | name: {u.name} | birthdate: {u.birth} | last spin:{u.lastSpin}");
            }
            
        }
        
        public static RegisteredUser SearchUser(string username)
        {
            foreach (var user in UserStorage.GetUsers())
            {
                if (user.username == username)
                {
                    return user;
                }
            }
            return null;
        }
        
        public static RegisteredUser DeleteUser(string username)
        {
            foreach (var user in UserStorage.GetUsers())
            {
                if (user.username == username)
                {
                    return user;
                }
            }
            return null;
        }

        public void ListOrders()
        {
            List<Order> orders = OrderStorage.GetOrders();
            if (orders.Count == 0)
            {
                Program.WriteError("There are no completed orders!");
                return;
            }
            foreach (var order in orders)
            {
                Console.WriteLine(order.ToString());
            }
        }
        
    }
}