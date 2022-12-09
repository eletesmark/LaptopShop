using System;
namespace LaptopWebshop
{
    abstract public class Product
	{
        static int count = 0;

        public readonly int id;
		public string name { get; protected set; }
		public string brand { get; protected set; }
		public int price { get; protected set; }

        public Product(string name, string brand, int price)
        {
            id = ++count;

            this.name = name;
            this.brand = brand;
            this.price = price;
        }

        public override string ToString() => string.Format("ID: {0}\r\n -Name: {1}\r\n -Brand: {2}\r\n -Price: {3:C}", id, name, brand, price);
    }
}

