using System;
namespace LaptopWebshop
{
	public class RAM : Product
	{
		public int size { get; private set; }
		public double speed { get; private set; }

        public RAM(string name, string brand, int size, double speed) : base(name, brand)
        {
            this.size = size;
            this.speed = speed;
        }

        public RAM(string line) : base(string.Empty, string.Empty)
        {
            string[] t = line.Split(';');

            this.name = t[0];
            this.brand = t[1];

            int.TryParse(t[2], out int _size);
            this.size = _size;

            double.TryParse(t[3], out double _speed);
            this.speed = _speed;
        }

        public override string FormatToTxt() => string.Format("{0};{1};{2};{3}", name, brand, size, speed);
        
        public override string ToString() => string.Format("Size: {0}, speed: {1}", size, speed);
    }
}

