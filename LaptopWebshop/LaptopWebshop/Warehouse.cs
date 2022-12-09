using System;
namespace LaptopWebshop
{
	public static class Warehouse
	{
		public static List<Product> products = new List<Product>();

		public static void AddNewProduct(Product newProduct)
		{
			products.Add(newProduct);
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

    }
}

