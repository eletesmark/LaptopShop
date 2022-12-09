using System;
namespace LaptopWebshop
{
	public class Laptop : Product
	{
		public CPU processor { get; private set; }
		public GPU graphicsCard { get; private set; }
		public RAM memory { get; private set; }
		public HardDrive storage { get; private set; }
		public Display screen { get; private set; }

        public double weight { get; private set; }
        public uint count { get; private set; }

        public Laptop(string name, string brand, int price, CPU processor, GPU graphicsCard, RAM memory, HardDrive storage, Display screen, double weight) : base(name, brand, price)
        {
            this.processor = processor;
            this.graphicsCard = graphicsCard;
            this.memory = memory;
            this.storage = storage;
            this.screen = screen;
            this.weight = weight;
            this.count = 0;
        }
	}
}

