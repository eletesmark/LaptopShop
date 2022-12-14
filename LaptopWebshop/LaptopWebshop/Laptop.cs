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
        public int price { get; private set; }

        public Laptop(string name, string brand, int price, CPU processor, GPU graphicsCard, RAM memory,
            HardDrive storage, Display screen, double weight) : base(name, brand)
        {
            this.processor = processor;
            this.graphicsCard = graphicsCard;
            this.memory = memory;
            this.storage = storage;
            this.screen = screen;
            this.weight = weight;
            this.count = 0;
            this.price = price;
        }

        public Laptop(string line) : base(string.Empty, string.Empty)
        {
            string[] t = line.Split(';');

            if (t.Length < 26)
            {
                return;
            }
                
            
            this.name = t[0];
            this.brand = t[1];

            /*
             Próbáltam, hogy csak az alkatrészek id-ét kiírni txt-be és beolvasáskor azt kikeresni a products listából
             de mivel minden termék a létrehozásakor kap egy id-t a konstruktorától,
             ezért nem lehetne a beolvasáskor beállítani a beolvasott id-t, mindig újat kap,
             szóval ez alapján nem lehetne visszakeresni az alkatrészeket, inkább kiírattam minden adatukat
            */
            this.processor = new CPU(string.Format("{0};{1}{2};{3}", t[2], t[3], t[4], t[5]));
            this.graphicsCard = new GPU(string.Format("{0};{1}{2};{3}", t[6], t[7], t[8], t[9]));
            this.memory = new RAM(string.Format("{0};{1}{2};{3}", t[10], t[11], t[12], t[13]));
            this.storage = new HardDrive(string.Format("{0};{1}{2};{3}", t[14], t[15], t[16], t[17]));
            this.screen = new Display(string.Format("{0};{1}{2};{3};{4}", t[18], t[19], t[20], t[21], t[22]));

            double.TryParse(t[23], out double _weight);
            this.weight = _weight;

            uint.TryParse(t[24], out uint _count);
            this.count = _count;

            int.TryParse(t[25], out int _price);
            this.price = _price;
        }

        public override string FormatToTxt() => string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9}", name, brand, processor.FormatToTxt(), graphicsCard.FormatToTxt(), memory.FormatToTxt(), storage.FormatToTxt(), screen.FormatToTxt(), weight, count, price);

        public override string ToString() => string.Format("ID: {0}: Name: {1}, Brand: {2}, Weight: {3} Kg, Count: {4}, Price: {5:C}, CPU: {6}, GPU: {7}, RAM: {8}, HardDrive: {9}, Screen: {10}", id, name, brand, weight, count, price, processor.ToString(), graphicsCard.ToString(), memory.ToString(), storage.ToString(), screen.ToString());

        //use this to create a laptop from the console
         public Laptop(string name, string brand, int cpuID, int gpuID, int ramID, int storageID, int screenID, double weight, int price) : base(name, brand)
         {
              processor = (CPU)Warehouse.products.FirstOrDefault(a => a.id == cpuID);        
              graphicsCard = (GPU)Warehouse.products.FirstOrDefault(a => a.id == gpuID);
              memory = (RAM)Warehouse.products.FirstOrDefault(a => a.id == ramID); 
              storage = (HardDrive)Warehouse.products.FirstOrDefault(a => a.id == storageID); 
              screen = (Display)Warehouse.products.FirstOrDefault(a => a.id == screenID); 
              this.weight = weight;
              count = 0;
              this.price = price;
         }
    }
}