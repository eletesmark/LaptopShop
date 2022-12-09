using System;
namespace LaptopWebshop
{
	public class GPU : Product
	{
		public int memory { get; private set; }
        public double clockRate { get; private set; }

        public GPU(string name, string brand, int price, int memory, double clockRate) : base(name, brand, price)
        {
            this.memory = memory;
            this.clockRate = clockRate;
        }
    }
}

