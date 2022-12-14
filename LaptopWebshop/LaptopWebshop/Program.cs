using System;
using System.Text;
using System.IO;
using System.Net.Mime;
using System.Xml.Linq;
using LaptopWebshop;

namespace LaptopWebshoop
{
    class Program
    {
        User currentUser;

        Program()
        {
            currentUser = new Guest();

            UserStorage.ReadUsersTxt();
        }

        public static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            //Console.BackgroundColor = ConsoleColor.Black;
            //Console.Clear();

            Program program = new Program();

            program.Menu();
        }

        void Menu()
        {
            while (true)
            {
                switch (currentUser.Type())
                {
                    case "Guest":
                        GuestMenu(); 
                        break;
                    case "Registered user": 
                        RegisteredMenu();
                        break;
                    case "Manager": 
                        ManagerMenu();
                        break;
                    case "Admin": 
                        AdminMenu();
                        break;
                    default: 
                        WriteError("Invalid input! Try again:"); 
                        break;
                }
            }
        }

        void WriteTxts()
        {
            UserStorage.WriteToTxt();
            Warehouse.WriteProductsToTxt();
            OrderStorage.WriteOrdersToTxt();
        }

        //Writers
        public static void WriteError(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\t{0}", msg);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void WriteSucces(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t{0}", msg);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void WriteCustom(string msg, ConsoleColor cc)
        {
            Console.ForegroundColor = cc;
            Console.WriteLine("\t{0}", msg);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        
        //GetInput
        public static void GetInput(ref string param, string msg)
        {
            do
            {
                Console.Write("\t{0}: ", msg);
                param = Console.ReadLine() ?? string.Empty; // Ha a Console.ReadLine() null értékkel tér vissza akkor helyette 'string.Empty' lesz a param értéke (különben warning lenne, mivel a string 'non-nullable reference' típus)
                if (param.Trim().Equals(string.Empty) || param.Length < 3)
                    WriteError("Invalid input! Please try again");
            } while (param.Trim().Equals(string.Empty) || param.Length < 3);
        }
        
        public static void GetInput(ref int param, string msg)
        {
            Console.Write("\t{0}: ", msg);
            while (!int.TryParse(Console.ReadLine(), out param))
            {
                WriteError("Invalid input! Please try again");
                Console.Write("\t{0}: ", msg);
            }
        }
        
        public static void GetInput(ref double param, string msg)
        {
            Console.Write("\t{0}: ", msg);
            while (!double.TryParse(Console.ReadLine(), out param))
            {
                WriteError("Invalid input! Please try again");
                Console.Write("\t{0}: ", msg);
            }
        }

        //GUEST Menu
        void GuestMenu()
        {
            Console.WriteLine("\r\nMenu:\r\n{0}", Guest.menu);
            
            Console.Write("Please select an option: ");
            int.TryParse(Console.ReadLine(), out int n); //Beolvas egy sort és megpróbálja számmá alakítaní, ha nem sikerül 0 lesz az 'n' változó értéke

            switch (n)
            {
                case 1:
                    Login();
                    break;
                case 2:
                    Registration();
                    break;
                //case 3: ListLaptops(); break;
                case 4:
                    WriteTxts();
                    Environment.Exit(0); 
                    break;
                default:
                    WriteError("Invalid input! Try again:");
                    break;
            }
        }

        //REGISTERED Menu
        void RegisteredMenu()
        {
            Console.WriteLine("\r\nMenu:\r\n{0}", RegisteredUser.menu);

            Console.Write("Please select an option: ");
            int.TryParse(Console.ReadLine(),
                out int n); //Beolvas egy sort és megpróbálja számmá alakítaní, ha nem sikerül 0 lesz az 'n' változó értéke

            switch (n)
            {
                case 1: currentUser.ListLaptops(); break;
                case 2: //AddToCart(); break;
                case 3: //ShowCart(); break;
                case 4:
                    Logout();
                    break;
                case 5:
                    WriteTxts();
                    Environment.Exit(0); 
                    break;
                default:
                    WriteError("Invalid input! Try again:");
                    break;
            }
        }

        void ManagerMenu()
        {
            Console.WriteLine("\r\nMenu:\r\n{0}", Manager.menu);

            Console.Write("Please select an option: ");
            int.TryParse(Console.ReadLine(),
                out int n); //Beolvas egy sort és megpróbálja számmá alakítaní, ha nem sikerül 0 lesz az 'n' változó értéke

            switch (n)
            {
                case 1: //ListProducts(); break;
                case 2: ((Manager)currentUser).addNewProduct(); break;
                case 3: //ModifyProduct(); break;
                case 4: //DeleteProduct(); break;
                case 5: //ShowStats(); break;
                case 6: //ChangeDiscounts(); break;
                case 7:
                    Logout();
                    break;
                case 8: 
                    WriteTxts();
                    Environment.Exit(0); 
                    break;
                default:
                    WriteError("Invalid input! Try again:");
                    break;
            }
        }

        void AdminMenu()
        {
            Console.WriteLine("\r\nMenu:\r\n{0}", Admin.menu);
            
            Console.Write("Please select an option: ");
            int.TryParse(Console.ReadLine(),
                out int n); //Beolvas egy sort és megpróbálja számmá alakítaní, ha nem sikerül 0 lesz az 'n' változó értéke

            switch (n)
            {
                case 1:
                    AddManagerRole();
                    AdminMenu();
                    break;
                case 2: //ListOrders(); AdminMenu(); break;
                case 3: ListUsers(); AdminMenu(); break;
                case 4: SearchUser(); AdminMenu();  break;
                case 5: DeleteUser(); AdminMenu(); break;
                case 6:
                    Logout();
                    break;
                case 7: return;
                default:
                    WriteError("Invalid input! Try again:");
                    AdminMenu();
                    break;
            }
        }
        
        //Registration
        void Registration()
        {
            currentUser = Guest.Register();
            WriteSucces("You successfully registered!");
            //RegisteredMenu();
        }

        //Login
        void Login()
        {
            currentUser = RegisteredUser.login();
            WriteSucces("You successfully logged in!");

            /*
            switch (currentUser.Type())
            {
                case "Registered user":
                    RegisteredMenu();
                    break;
                case "Manager":
                    ManagerMenu();
                    break;
                case "Admin":
                    AdminMenu();
                    break;
                default:
                    WriteError("Unexpected error! Please try again!");
                    //GuestMenu();
                    return;
                    break;
            }
            */
        }

        //Logout
        void Logout()
        {
            //Ha a kosar nem ures akkor rakerdezes kijelentkezes elott!!!
            currentUser = new Guest();
            GuestMenu();
        }

        void AddNewProduct()
        {
            Console.WriteLine();
            Console.WriteLine("  1. CPU");
        }
        
        //TODO szépen kiírni
        public void AddManagerRole()
        {
            if (currentUser.Type() != "Admin")
            {
                return;
            }
            Console.WriteLine("username: ");
            string username = Console.ReadLine();

            Admin.AddManagerRole(username);
        }
        //TODO szépen kiírni
        public void ListUsers()
        {
            if (currentUser.Type() != "Admin")
            {
                Console.WriteLine("How ?");
                return;
            }
            // ne kérdezd én sem vágom, de az adminnak kell ezt csinálnia sooo...
            var users = Admin.ListUsers();
            
            foreach (var u in users)
            {
                Console.WriteLine(u.username + " "+ u.name+ " "+u.Type());
            }
        }
        
        //TODO szépen megírni a kiírást
        public void SearchUser()
        {
            Console.WriteLine("Username you want to search for: ");
            string username = Console.ReadLine();
            User u = Admin.SearchUser(username);
            if (u!= null)
            {
                return;
            }
            Console.WriteLine("There is no user with this username!");
        }
        //TODO szépen megírni a kiírást, lehetne szebben
        public void DeleteUser()
        {
            if (currentUser.Type() != "Admin" || currentUser.Type() != "Manager")
            {
                return;
            }

            Console.WriteLine("Username of the user you want to delete: ");
            string username = Console.ReadLine();
            try
            {
                Admin.DeleteUser(username);
            }
            catch (Exception e)
            {
                Console.WriteLine("There's no user with this userName!");
            }
            Console.WriteLine("User Successfully deleted!");
            AdminMenu();
        }

        //Password reader - https://www.c-sharpcorner.com/forums/password-in-c-sharp-console-application
        public static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            while (keyInfo.Key != ConsoleKey.Enter)
            {
                if (keyInfo.Key != ConsoleKey.Backspace &&
                    (char.IsLetter(keyInfo.KeyChar) || char.IsNumber(keyInfo.KeyChar)))
                {
                    Console.Write("*");
                    password += keyInfo.KeyChar;
                }
                else if (keyInfo.Key == ConsoleKey.Backspace && !string.IsNullOrEmpty(password))
                {
                    password = password.Substring(0, password.Length - 1);
                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                    Console.Write(" ");
                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                }

                keyInfo = Console.ReadKey(true);
            }

            Console.WriteLine();
            return password;
        }
    }
}