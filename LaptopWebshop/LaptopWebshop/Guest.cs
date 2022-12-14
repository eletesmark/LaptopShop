using System;
using LaptopWebshoop;

namespace LaptopWebshop
{
	public class Guest : User
	{
        public static readonly string menu = "  1.Login\r\n  2.Registration\r\n  3.List laptops\r\n  4.Show cart\r\n 5.Add to cart\r\n   6.Remove from cart\r\n  7.Exit";

        public Guest() : base() { }

        public override string Type() => "Guest";

        public override string ToString() => "GUEST";
        
        public static RegisteredUser Register()
        {
	        string username = string.Empty;
	        string name = string.Empty;
	        string password = string.Empty;
	        DateOnly birth = DateOnly.MinValue;

	        Console.WriteLine("\r\nRegistration form:");
	        while (true)
	        {
		        Console.Write("Username: ");
		        username = Console.ReadLine()!;
		        if (UserStorage.IsUsernameTaken(username)  || username.Length < 3)
		        {
			        Program.WriteError("This username is already in use or too short!");
					continue;
		        }

		        if (username is "")
		        {
			        Program.WriteError("Username cannot be empty");
			        continue;
		        }
		        
		        break;
	        }

	        do
	        {
		        Console.Write("Name: ");
		        name = Console.ReadLine() ?? string.Empty;
		        if (name.Trim().Equals(string.Empty) || name.Length < 3)
			        Program.WriteError("Name should be at least 3 characters long! Please try again");
	        } while (name.Trim().Equals(string.Empty) || name.Length < 3);
	        
	        do
	        {
		        Console.Write("Password: ");
		        password = Console.ReadLine() ?? string.Empty;
		        if (password == "0")
		        {
			        Program.WriteError("Password cannot be '0'");
			        continue;
		        }
		        if (password.Trim().Equals(string.Empty) || password.Length < 3)
			        Program.WriteError("Invalid input! Please try again");
	        } while (password.Trim().Equals(string.Empty) || password.Length < 3);

	        Console.Write("Birthday (YYYY-MM-DD): ");
	        while (!DateOnly.TryParse(Console.ReadLine(), out birth))
	        {
		        Program.WriteError("Invalid input! Please try again");
		        Console.Write("Birthday (YYYY-MM-DD): ");
	        }

	        RegisteredUser u = new(username, name, password, birth);
	        UserStorage.AddUser(u);
	        
	        return u;
        }
    }
}

