using System;
namespace LaptopWebshop
{
	public class Display : Product
	{
		public double size { get; private set; }
		public Tuple<int, int> resolution { get; private set; }

		public Display(string name, string brand, double size, int x, int y) : base(name, brand)
        {
			this.size = size;
			resolution = new Tuple<int, int>(x, y);
		}

        public Display(string line) : base(string.Empty, string.Empty)
        {
            string[] t = line.Split(';');

            this.name = t[0];
            this.brand = t[1];

            double.TryParse(t[2], out double _size);
            this.size = _size;

            int.TryParse(t[3], out int _resWidth);
            int.TryParse(t[4], out int _resHeight);

            this.resolution = new Tuple<int, int>(_resWidth, _resHeight);
        }

        public override string FormatToTxt() => string.Format("{0};{1};{2};{3};{4}", name, brand, size, resolution.Item1, resolution.Item2);
        
        public override string ToString() => string.Format("Size: {0}, Resolution: {1}x{2}", size, resolution.Item1, resolution.Item2);
    }
}

