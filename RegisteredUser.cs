using System;

namespace LaptopWebshop
{
    public class RegisteredUser : User
    {
        public static readonly string menu =
            "  1.List laptops\r\n  2.Add to cart\r\n  3.Show cart\r\n  4.Logout\r\n  5.Exit";

        public RegisteredUser(string username, string name, string password, DateOnly birth) : base(username, name,
            password, birth)
        {
        }

        public RegisteredUser(string line) : base(line)
        {
        }

        public override string Type() => "Registered user";

        public static User login()
        {
            string username = string.Empty;
            string password = string.Empty;
            DateOnly birth;

            Console.WriteLine("\r\nLogin form:");

            Console.WriteLine("Username: ");
            username = Console.ReadLine()!;
            while (!UserStorage.IsValidUsername(username))
            {
                Console.WriteLine("Invalid username! Try again:");
                username = Console.ReadLine()!;
            }

            Console.WriteLine("Password: ");
            password = Console.ReadLine()!;
            while (!UserStorage.IsValid(username, password))
            {
                Console.WriteLine("Invalid password! Try again:");
                password = Console.ReadLine()!;
            }

            return UserStorage.GetUser(username);

            //string name = UserStorage.GetUser(username).name;
            //birth = UserStorage.GetUser(username).birth;
            //string type = UserStorage.GetUser(username).Type();
            //Console.WriteLine(type+ " asdasdasd ");

            //switch (type)
            //{
            //    case "RegisteredUser" : return new RegisteredUser(username, name, password, birth);
            //    case "Manager" : return new Manager(username, name, password, birth);
            //    case "Admin" : return new Admin(username, name, password, birth);
            //    default: return null;
            //}
        }
    }
}