using System;
using LaptopWebshoop;

namespace LaptopWebshop
{
    public class Manager : RegisteredUser
    {
        public static readonly string menu =
            "  1.List products\r\n  2.Add new product\r\n  3.Modify product\r\n  4.Delete product\r\n  5.Stats\r\n  6.Change lucky-wheel's discounts\r\n  7.Logout\r\n  8.Exit";

        public Manager(string username, string name, string password, DateOnly birth) : base(username, name, password,
            birth)
        {
        }

        public Manager(string line) : base(line)
        {
        }

        // public Manager(User u) : base(u.username, u.name, u.password, u.birth) { }

        public override string Type() => "Manager";

        public void AddNewProduct()
        {
            Console.WriteLine("\r\nProduct types: ");
            Console.WriteLine("  1.CPU");
            Console.WriteLine("  2.GPU");
            Console.WriteLine("  3.RAM");
            Console.WriteLine("  4.HardDrive");
            Console.WriteLine("  5.Display");
            Console.WriteLine("  6.Laptop");

            int n = 0;
            Program.GetInput(ref n, "Choose a product type");

            string tmpName = string.Empty; //All
            string tmpBrand = string.Empty; //All
            double tmpClockRate = 0; //CPU, GPU
            int tmpCores = 0; //CPU
            int tmpMemory = 0; //GPU, RAM, HardDrive
            double tmpSpeed = 0; //RAM
            string tmpType = string.Empty; //HardDrive
            double tmpSize = 0; //Display
            int tmpWidth = 0; //Display
            int tmpHeight = 0; //Display

            int tmpCPUid = 0;
            int tmpGPUid = 0;
            int tmpRAMid = 0;
            int tmpHardDRiveid = 0;
            int tmpDisplayid = 0;

            double tmpWeight = 0;
            int tmpPrice = 0;

            switch (n)
            {
                case 1:
                    Program.GetInput(ref tmpName, "CPU name");
                    Program.GetInput(ref tmpBrand, "CPU brand");
                    Program.GetInput(ref tmpClockRate, "CPU clock rate");
                    Program.GetInput(ref tmpCores, "CPU cores");
                    Warehouse.AddNewProduct(new CPU(tmpName, tmpBrand, tmpClockRate, tmpCores));
                    Program.WriteSucces("CPU added successfully!");
                    break;
                case 2:
                    Program.GetInput(ref tmpName, "GPU name");
                    Program.GetInput(ref tmpBrand, "GPU brand");
                    Program.GetInput(ref tmpMemory, "GPU memory");
                    Program.GetInput(ref tmpClockRate, "GPU clock rate");
                    Warehouse.AddNewProduct(new GPU(tmpName, tmpBrand, tmpMemory, tmpClockRate));
                    Program.WriteSucces("GPU added successfully!");
                    break;
                case 3:
                    Program.GetInput(ref tmpName, "RAM name");
                    Program.GetInput(ref tmpBrand, "RAM brand");
                    Program.GetInput(ref tmpMemory, "RAM size");
                    Program.GetInput(ref tmpSpeed, "RAM speed");
                    Warehouse.AddNewProduct(new RAM(tmpName, tmpBrand, tmpMemory, tmpSpeed));
                    Program.WriteSucces("RAM added successfully!");
                    break;
                case 4:
                    Program.GetInput(ref tmpName, "HardDrive name");
                    Program.GetInput(ref tmpBrand, "HardDrive brand");
                    Program.GetInput(ref tmpMemory, "HardDrive size");
                    Program.GetInput(ref tmpType, "HardDrive type");
                    Warehouse.AddNewProduct(new HardDrive(tmpName, tmpBrand, tmpMemory, tmpType));
                    Program.WriteSucces("HardDrive added successfully!");
                    break;
                case 5:
                    Program.GetInput(ref tmpName, "Display name");
                    Program.GetInput(ref tmpBrand, "Display brand");
                    Program.GetInput(ref tmpSize, "Display size");
                    Program.GetInput(ref tmpWidth, "Display width (pixels)");
                    Program.GetInput(ref tmpHeight, "Display height (pixels)");
                    Warehouse.AddNewProduct(new Display(tmpName, tmpBrand, tmpSize, tmpWidth, tmpHeight));
                    Program.WriteSucces("Display added successfully!");
                    break;
                case 6:
                    Console.WriteLine("Components:\r\n{0}",
                        string.Join("\r\n",
                            Warehouse.ListProducts(a => a.GetType() != typeof(Laptop)).OrderBy(a => a.GetType().Name)
                                .Select(a => string.Format("ID: {0}, {1}, {2}", a.id, a.GetType().Name, a.ToString()))
                                .ToList()));
                    Program.GetInput(ref tmpName, "Laptop name");
                    Program.GetInput(ref tmpBrand, "Laptop brand");
                    Program.GetInput(ref tmpCPUid, "Laptop CPU ID");
                    Program.GetInput(ref tmpGPUid, "Laptop GPU ID");
                    Program.GetInput(ref tmpRAMid, "Laptop RAM ID");
                    Program.GetInput(ref tmpHardDRiveid, "Laptop HardDrive ID");
                    Program.GetInput(ref tmpDisplayid, "Laptop Display ID");
                    Program.GetInput(ref tmpWeight, "Laptop weight");
                    Program.GetInput(ref tmpPrice, "Laptop price");
                    if (Warehouse.products.Where(a => a.GetType() == typeof(CPU)).Any(a => a.id == tmpCPUid)
                        && Warehouse.products.Where(a => a.GetType() == typeof(GPU)).Any(a => a.id == tmpGPUid)
                        && Warehouse.products.Where(a => a.GetType() == typeof(RAM)).Any(a => a.id == tmpRAMid)
                        && Warehouse.products.Where(a => a.GetType() == typeof(HardDrive))
                            .Any(a => a.id == tmpHardDRiveid)
                        && Warehouse.products.Where(a => a.GetType() == typeof(Display)).Any(a => a.id == tmpDisplayid))
                        Warehouse.AddNewProduct(new Laptop(tmpName, tmpBrand, tmpCPUid, tmpGPUid, tmpRAMid,
                            tmpHardDRiveid, tmpDisplayid, tmpWeight, tmpPrice));
                    else
                    {
                        Console.WriteLine("Invalid component ID(s)!");
                        break;
                    }

                    Program.WriteSucces("Laptop added successfully!");
                    break;
                default:
                    Console.WriteLine("Invalid input!");
                    break;
            }
        }
        
        

        public static void AddNewPrize(int prize)
        {
            LuckyWheel.addNewPrize(prize);
        }

        //deletes all prize with the same value
        public static void DeletePrize(int prize)
        {
            LuckyWheel.deletePrize(prize);
        }

        public void GetTotalRevenue()
        {
            //write out the sum of all orders in orderStorage
            int sum = 0;
            var orders = OrderStorage.GetOrders();
            foreach (var order in orders)
            {
                sum += order.getSum();
            }
            Console.WriteLine("Total revenue: {0}", sum);
        }
        
    }
}