using System;
namespace LaptopWebshop
{
	public class Admin : User
	{
        public static readonly string menu = "  1.Add manager rule to user\r\n  2.List orders\r\n  3.List users\r\n  4.Search user\r\n  5.Delete user\r\n  6.Logout\r\n  7.Exit";

        public Admin(string username, string name, string password, DateOnly birth) : base(username, name, password, birth) { }

        public Admin(string line) : base(line) { }

        public override string Type() => "Admin";
    }
}

