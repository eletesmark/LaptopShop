using System;
using System.Text;

namespace LaptopWebshop
{
	public static class Warehouse
	{
		public static List<Product> products = new List<Product>();

		public static void AddNewProduct(Product newProduct)
		{
			products.Add(newProduct);
            WriteProductsToTxt();
		}

		public static void ModifyProduct(int id, Product product)
		{
			if (products.Any(a => a.id == id))
				products[products.Where(a => a.id == id).Select(a => a.id).ToList()[0]] = product;
		}

		public static void DeleteProduct(int id)
		{
            if (products.Any(a => a.id == id))
                products.Remove(products.Where(a => a.id == id).ToList()[0]);
		}

		public static List<Product> ListProducts() => new List<Product>(products);

        public static List<Product> ListProducts(Func<Product, bool> filter) => products.Where(a => filter(a)).ToList();

        public static void ReadProductsTxt()
        {
            products.Clear();

            Console.WriteLine(File.ReadAllLines("CPUs.txt").Length);
            
            if (File.Exists("CPUs.txt"))
                products.AddRange(File.ReadAllLines("CPUs.txt").Where(a => a.Split(';').Length == 4).Select(a => new CPU(a)).ToList());

            if (File.Exists("GPUs.txt"))
                products.AddRange(File.ReadAllLines("GPUs.txt").Where(a => a.Split(';').Length == 4).Select(a => new GPU(a)).ToList());

            if (File.Exists("RAMs.txt"))
                products.AddRange(File.ReadAllLines("RAMs.txt").Where(a => a.Split(';').Length == 4).Select(a => new RAM(a)).ToList());

            if (File.Exists("HardDrives.txt"))
                products.AddRange(File.ReadAllLines("HardDrives.txt").Where(a => a.Split(';').Length == 4).Select(a => new HardDrive(a)).ToList());

            if (File.Exists("Displays.txt"))
                products.AddRange(File.ReadAllLines("Displays.txt").Where(a => a.Split(';').Length == 5).Select(a => new Display(a)).ToList());

            if (File.Exists("Laptops.txt"))
                products.AddRange(File.ReadAllLines("Laptops.txt").Where(a => a.Split(';').Length == 26).Select(a => new Laptop(a)).ToList());
        }

        public static void WriteProductsToTxt()
        {
            TextWriter CPUs_Txt = StreamWriter.Null; //Close() miatt kell adni neki valami alap erteket
            TextWriter GPUs_Txt = StreamWriter.Null;
            TextWriter RAMs_Txt = StreamWriter.Null;
            TextWriter HardDrives_Txt = StreamWriter.Null;
            TextWriter Displays = StreamWriter.Null;
            TextWriter Laptops = StreamWriter.Null;

            try
            {
                CPUs_Txt = new StreamWriter("CPUs.txt", false, Encoding.UTF8);
                GPUs_Txt = new StreamWriter("GPUs.txt", false, Encoding.UTF8);
                RAMs_Txt = new StreamWriter("RAMs.txt", false, Encoding.UTF8);
                HardDrives_Txt = new StreamWriter("HardDrives.txt", false, Encoding.UTF8);
                Displays = new StreamWriter("Displays.txt", false, Encoding.UTF8);
                Laptops = new StreamWriter("Laptops.txt", false, Encoding.UTF8);

                CPUs_Txt.WriteLine(string.Join("\r\n", products.Where(a => a.GetType() == typeof(CPU)).Select(a => ((CPU)a).FormatToTxt()).ToList()));
                GPUs_Txt.WriteLine(string.Join("\r\n", products.Where(a => a.GetType() == typeof(GPU)).Select(a => ((GPU)a).FormatToTxt()).ToList()));
                RAMs_Txt.WriteLine(string.Join("\r\n", products.Where(a => a.GetType() == typeof(RAM)).Select(a => ((RAM)a).FormatToTxt()).ToList()));
                HardDrives_Txt.WriteLine(string.Join("\r\n", products.Where(a => a.GetType() == typeof(HardDrive)).Select(a => ((HardDrive)a).FormatToTxt()).ToList()));
                Displays.WriteLine(string.Join("\r\n", products.Where(a => a.GetType() == typeof(Display)).Select(a => ((Display)a).FormatToTxt()).ToList()));
                Laptops.WriteLine(string.Join("\r\n", products.Where(a => a.GetType() == typeof(Laptop)).Select(a => ((Laptop)a).FormatToTxt()).ToList()));
            }
            catch (IOException ioex)
            {

            }
            catch (Exception e)
            {

            }
            finally
            {
                CPUs_Txt.Close();
                GPUs_Txt.Close();
                RAMs_Txt.Close();
                HardDrives_Txt.Close();
                Displays.Close();
                Laptops.Close();
            }
        }
    }
}

