using System;
namespace LaptopWebshop
{
	public class GPU : Product
	{
		public int memory { get; private set; }
        public double clockRate { get; private set; }

        public GPU(string name, string brand, int price, int memory, double clockRate) : base(name, brand)
        {
            this.memory = memory;
            this.clockRate = clockRate;
        }

        public GPU(string line) : base(string.Empty, string.Empty)
        {
            string[] t = line.Split(';');

            this.name = t[0];
            this.brand = t[1];

            int.TryParse(t[2], out int _memory);
            this.memory = _memory;

            double.TryParse(t[3], out double _clockRate);
            this.clockRate = _clockRate;
        }

        public override string FormatToTxt() => string.Format("{0};{1};{2};{3};{4}", id, name, brand, memory, clockRate);

        public override string ToString() => string.Format("Memory: {0}, clock rate: {1}", memory, clockRate);
    }
}

