using System;
using System.Security.Cryptography;
using System.Text;

namespace LaptopWebshop
{
    public class RegisteredUser : User
    {
        public string username { get; protected set; }
        public string name { get; protected set; }
        public string password { get; protected set; }
        public DateOnly birth { get; protected set; }
        public DateTime lastSpin { get; protected set; }
        public int discount { get; protected set; }

        public static readonly string menu =
            "  1.List laptops\r\n  2.Add to cart\r\n  3.Show cart\r\n  4.Logout\r\n  5.Exit";
        
        //TODO létrehozásnál beállítani az order adattagot filebeolvasáskor
        public RegisteredUser(string username, string name, string password, DateOnly birth)
        {
            this.username = username;
            this.name = name;
            this.password = SHA1(password);
            this.birth = birth;

            //Default értékek beállítása
            this.lastSpin = DateTime.MinValue; //Alapból a legkorábbi dátumot tároljuk, így biztosítva, hogy a még sohasem pörgetett felhasználónál is müködjön majd az ellenőrzés
            this.discount = 0;
        }
        
        public RegisteredUser(string line)
        {
            string[] t = line.Split(';');

            this.username = t[0];
            this.name = t[1];
            this.password = t[2];

            DateOnly.TryParse(t[3], out DateOnly tempDO);
            this.birth = tempDO;

            DateTime.TryParse(t[4], out DateTime tempDT);
            this.lastSpin = tempDT;

            int.TryParse(t[5], out int tempDisc);
            this.discount = tempDisc;
        }
        
        public bool IsIt(string username, string password) => this.username.Equals(username) && this.password.Equals(SHA1(password));

        public override string ToString() => string.Format("{0}\r\n -Name: {1}\r\n -Password: {2}\r\n -Birthday: {3}\r\n -Type: {4}\r\n -Last spin: {5}\r\n -Discount: {6}", username, name, password, birth.ToString("yyyy.MM.dd"), Type(), lastSpin.ToString("yyyy.MM.dd HH:mm:ss"), discount);

        //Formázott kiírás a txt-be mentéshez
        public string FormatToTxt() => string.Format("{0};{1};{2};{3};{4};{5}", username, name, password, birth.ToString("yyyy.MM.dd"), lastSpin.ToString("yyyy.MM.dd HH:mm:ss"), discount.ToString());

        //Hash password - https://www.codeproject.com/Questions/523323/Encryptingpluspasswordplusinplusc-23
        public static string SHA1(string password) => Convert.ToBase64String(HashAlgorithm.Create("SHA1").ComputeHash(Encoding.Unicode.GetBytes(password)));
        
        //ha false dobhatsz szép errort ha már megírtad a functiont
        public bool spin()
        {
            List<int> prizes = LuckyWheel.getPrizes();
            if (prizes.Count > 0)
            {
                Random rnd = new Random();
                int prizesindex = rnd.Next(0, prizes.Count);
                discount = prizes[prizesindex];
                lastSpin = DateTime.Now;
                return true;
            }

            return false;
        }

        public override string Type() => "Registered user";

        public static RegisteredUser login()
        {
            string username = string.Empty;
            string password = string.Empty;
            DateOnly birth;

            Console.WriteLine("\r\nLogin form:");

            Console.WriteLine("Username: ");
            username = Console.ReadLine()!;
            while (!UserStorage.IsValidUsername(username))
            {
                Console.WriteLine("Invalid username! Try again:");
                username = Console.ReadLine()!;
            }

            Console.WriteLine("Password: ");
            password = Console.ReadLine()!;
            while (!UserStorage.IsValid(username, password))
            {
                Console.WriteLine("Invalid password! Try again:");
                password = Console.ReadLine()!;
            }

            string name = UserStorage.GetUser(username).name;
            birth = UserStorage.GetUser(username).birth;
            string type = UserStorage.GetUser(username).Type();

            switch (type)
            {
                case "RegisteredUser": return new RegisteredUser(username, name, password, birth);
                case "Manager": return new Manager(username, name, password, birth);
                case "Admin": return new Admin(username, name, password, birth);
                default: return null;
            }
        }

        public void Purchase()
        {
            
        }
    }
}