using System;
namespace LaptopWebshop
{
	public class HardDrive : Product
	{
		public int size { get; private set; }
		public string type { get; private set; }

        public HardDrive(string name, string brand, int size, string type) : base(name, brand)
        {
            this.size = size;
            this.type = type;
        }

        public HardDrive(string line) : base(string.Empty, string.Empty)
        {
            string[] t = line.Split(';');

            this.name = t[0];
            this.brand = t[1];

            int.TryParse(t[2], out int _size);
            this.size = _size;

            this.type = t[4];
        }

        public override string FormatToTxt() => string.Format("{0};{1};{2};{3}", name, brand, size, type);
        
        public override string ToString() => string.Format("Size: {0}, Type: {1}", size, type);
    }
}

