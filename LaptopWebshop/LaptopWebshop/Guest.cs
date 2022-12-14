using System;
namespace LaptopWebshop
{
	public class Guest : User
	{
        public static readonly string menu = "  1.Login\r\n  2.Registration\r\n  3.List laptops\r\n  4.Exit";

        public Guest() : base() { }

        public override string Type() => "Guest";

        public override string ToString() => "GUEST";
        
        //public static void Register(string username, string name, string password, DateOnly birth)
        public static RegisteredUser Register()
        {
	        string username = string.Empty;
	        string name = string.Empty;
	        string password = string.Empty;
	        DateOnly birth = DateOnly.MinValue;

	        Console.WriteLine("\r\nRegistration form:");

	        Console.Write("Username: ");
	        username = Console.ReadLine()!;
	        while (UserStorage.IsUsernameTaken(username))
	        {
		        Console.WriteLine("This username is already in use!");
		        Console.Write("Username: ");
		        username = Console.ReadLine()!;
	        }

	        do
	        {
		        Console.Write("Name: ");
		        name = Console.ReadLine() ?? string.Empty;
		        if (name.Trim().Equals(string.Empty) || name.Length < 3)
			        Console.WriteLine("Invalid input! Please try again");
	        } while (name.Trim().Equals(string.Empty) || name.Length < 3);
	        
	        do
	        {
		        Console.Write("Password: ");
		        password = Console.ReadLine() ?? string.Empty;
		        if (username.Trim().Equals(string.Empty) || username.Length < 3)
			        Console.WriteLine("Invalid input! Please try again");
	        } while (username.Trim().Equals(string.Empty) || username.Length < 3);

	        Console.Write("Birthday (YYYY-MM-DD): ");
	        while (!DateOnly.TryParse(Console.ReadLine(), out birth))
	        {
		        Console.WriteLine("Invalid input! Please try again");
		        Console.Write("Birthday (YYYY-MM-DD): ");
	        }

	        RegisteredUser u = new(username, name, password, birth);
	        UserStorage.AddUser(u);
	        
	        return u;
        }
    }
}

