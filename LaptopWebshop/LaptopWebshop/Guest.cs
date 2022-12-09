using System;
namespace LaptopWebshop
{
	public class Guest : User
	{
        public static readonly string menu = "  1.Login\r\n  2.Registration\r\n  3.List laptops\r\n  4.Exit";

        public Guest() : base() { }

        public override string Type() => "Guest";

        public override string ToString() => "GUEST";
    }
}

