using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopWebshop
{
    public static class LuckyWheel
    {
        private static List<int> prizes = new();

        public static List<int> getPrizes()
        {
            return prizes;
        }

        public static void addNewPrize(int newPrize)
        {
            prizes.Add(newPrize);
            WritePrizesToTxt();
        }

        public static void deletePrize(int id)
        {
            if (id > 0 && id < prizes.Count+1)
            {
                prizes.RemoveAt(id-1);
            }
            WritePrizesToTxt();
        }

        public static void ReadPrizesTxt()
        {
            prizes.Clear();

            try
            {
                if (File.Exists("Prizes.txt"))
                    prizes = File.ReadAllLines("Prizes.txt").Select(a => int.Parse(a)).ToList();
            }
            catch (Exception e)
            {
                prizes = new List<int>();
            }
            // finally
            // {
            // }
        }

        public static void WritePrizesToTxt()
        {
            TextWriter Prizes_Txt = StreamWriter.Null;

            try
            {
                Prizes_Txt = new StreamWriter("Prizes.txt", false, Encoding.UTF8);

                Prizes_Txt.WriteLine(string.Join("\r\n", prizes));
            }
            catch (IOException ioex)
            {

            }
            catch (Exception e)
            {

            }
            finally
            {
                Prizes_Txt.Close();
            }
        }
    }
}
