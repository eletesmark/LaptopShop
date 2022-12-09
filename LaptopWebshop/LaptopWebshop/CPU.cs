using System;
namespace LaptopWebshop
{
	public class CPU : Product
	{
		public double clockRate { get; private set; }
		public int cores { get; private set; }

        public CPU(string name, string brand, int price, double clockRate, int cores) : base(name, brand, price)
		{
			this.clockRate = clockRate;
			this.cores = cores;
		}
	}
}

