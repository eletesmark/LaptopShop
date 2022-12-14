using System;
using System.Security.Cryptography;
using System.Text;

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

        void getPrizes()
        {
            //TODO list all prizes to stdout
            
        }
        
        public void AddToCart(Laptop laptop)
        {
            order.cart.Add(laptop);
        }

        public void RemoveFromCart(int id)
        {
            if(id > 0 && id < order.cart.Count)
                order.cart.Remove(order.cart[id - 1]);
        }
    }
}

