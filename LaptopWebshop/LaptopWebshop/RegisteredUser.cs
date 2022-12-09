using System;
namespace LaptopWebshop
{
	public class RegisteredUser : User
	{
		public static readonly string menu = "  1.List laptops\r\n  2.Add to cart\r\n  3.Show cart\r\n  4.Logout\r\n  5.Exit";

		public RegisteredUser(string username, string name, string password, DateOnly birth) : base(username, name, password, birth) { }

		public RegisteredUser(string line) : base(line) { }

		public override string Type() => "Registered user";
	}
}

