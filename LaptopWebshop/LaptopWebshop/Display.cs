using System;
namespace LaptopWebshop
{
	public class Display : Product
	{
		public int size { get; private set; }
		public Tuple<int, int> resolution { get; private set; }

		public Display(string name, string brand, int price, int size, int x, int y) : base(name, brand, price)
        {
			this.size = size;
			resolution = new Tuple<int, int>(x, y);
		}
	}
}

