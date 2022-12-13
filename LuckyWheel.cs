using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopWebshop
{
    public static class LuckyWheel
    {
        private static List<int> prizes;

        public static List<int> getPrizes()
        {
            return prizes;
        }

        public static void addNewPrize(int newPrize)
        {
            prizes.Add(newPrize);
        }

        public static void deletePrize(int prize)
        {
            prizes = prizes.Where((x) => x != prize).ToList();
        }
    }
}
