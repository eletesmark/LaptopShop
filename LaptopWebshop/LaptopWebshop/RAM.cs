using System;
namespace LaptopWebshop
{
	public class RAM : Product
	{
		public int size { get; private set; }
		public double speed { get; private set; }

        public RAM(string name, string brand, int price, int size, double speed) : base(name, brand, price)
        {
            this.size = size;
            this.speed = speed;
        }
    }
}

