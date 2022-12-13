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
            
        }

        public abstract string Type();
        

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
            order.cart.Remove(order.cart[id - 1]);
        }
    }
}

