using System;
namespace LaptopWebshop
{
	public class HardDrive : Product
	{
		public int size { get; private set; }
		public string type { get; private set; }

        public HardDrive(string name, string brand, int price, int size, string type) : base(name, brand, price)
        {
            this.size = size;
            this.type = type;
        }
    }
}

