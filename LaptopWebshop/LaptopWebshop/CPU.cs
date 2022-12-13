using System;
namespace LaptopWebshop
{
	public class CPU : Product
	{
		public double clockRate { get; private set; }
		public int cores { get; private set; }

        public CPU(string name, string brand, int price, double clockRate, int cores) : base(name, brand)
		{
			this.clockRate = clockRate;
			this.cores = cores;
		}

        public CPU(string line) : base(string.Empty, string.Empty)
        {
            string[] t = line.Split(';');

            this.name = t[0];
            this.brand = t[1];

            double.TryParse(t[2], out double _clockRate);
            this.clockRate = _clockRate;

            int.TryParse(t[3], out int _cores);
            this.cores = _cores;
        }

        public override string FormatToTxt() => string.Format("{0};{1};{2};{3}", name, brand, clockRate, cores);
    }
}

