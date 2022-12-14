using System;
using System.Security.Cryptography;
using System.Text;
using LaptopWebshoop;

namespace LaptopWebshop
{
	public abstract class User
	{
        protected Order order { get; set; }

        //Alap konstruktor a vendéghez
        public User()
        {
            order = new Order();
        }

        public abstract string Type();

        public void ListLaptops()
        {
            foreach (var laptop in Warehouse.ListProducts(a => a.GetType() == typeof(Laptop)))
                Console.WriteLine(laptop.ToString());

            //Console.WriteLine(string.Join("\r\n", Warehouse.ListProducts(a => a.GetType() == typeof(Laptop)).Select(a => a.ToString()).ToList()));
        }

        public void ListPrizes()
        {
            Console.WriteLine("LuckyWheel prizes: ");
            foreach (var p in LuckyWheel.getPrizes())
                Console.WriteLine(p);
        }
        
        public void AddToCart()
        {
            ListLaptops();
            
            int n = 0;
            Program.GetInput(ref n, "\r\nChoose a Laptop ID");

            if(!Warehouse.products.Where(a => a.GetType() == typeof(Laptop)).Any(a => a.id == n))
                Console.WriteLine("Invalid laptop ID!");
            else
                order.cart.Add((Laptop)Warehouse.products.Where(a => a.GetType() == typeof(Laptop)).First(a => a.id == n));
        }

        public void ShowCart()
        {
            if(order.cart.Count == 0)
                Console.WriteLine("Your cart is empty!");
            else
                foreach (var laptop in order.cart)
                    Console.WriteLine(laptop.ToString());
        }

        public void RemoveFromCart(int id)
        {
            if(id > 0 && id < order.cart.Count)
                order.cart.Remove(order.cart[id - 1]);
        }
    }
}

