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
            Warehouse.ReadProductsTxt();
            OrderStorage.ReadOrdersTxt();
            LuckyWheel.ReadPrizesTxt();
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
            LuckyWheel.WritePrizesToTxt();
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
                case 3: currentUser.ListLaptops(); break;
                case 4: currentUser.AddToCart(); break;
                case 5: currentUser.ShowCart(); break;
                case 6:
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
                case 2: currentUser.AddToCart(); break;
                case 3: currentUser.ShowCart(); break;
                case 4: ((RegisteredUser)currentUser).Purchase(); break;
                case 5:
                    Console.WriteLine($"Your discount: {((RegisteredUser)currentUser).discount}%"); break;
                case 6: ((RegisteredUser)currentUser).spin(); break;
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

        void ManagerMenu()
        {
            Console.WriteLine("\r\nMenu:\r\n{0}", Manager.menu);

            Console.Write("Please select an option: ");
            int.TryParse(Console.ReadLine(),
                out int n); //Beolvas egy sort és megpróbálja számmá alakítaní, ha nem sikerül 0 lesz az 'n' változó értéke

            switch (n)
            {
                case 1: currentUser.ListLaptops(); break;
                case 2: ((Manager)currentUser).AddNewProduct(); break;
                case 3: ((Manager)currentUser).ModifyProduct(); break;
                case 4: ((Manager)currentUser).DeleteProduct(); break;
                case 5: ((Manager)currentUser).GetTotalRevenue(); break;
                case 6:
                    ChangeDiscounts();
                    break;
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
                    break;
                case 2:
                    ((Admin)currentUser).ListOrders();
                    break;
                case 3:
                    ListUsers();
                    break;
                case 4:
                    SearchUser();
                    break;
                case 5:
                    DeleteUser();
                    break;
                case 6: currentUser.ListLaptops(); break;
                case 7: currentUser.AddToCart(); break;
                case 8: currentUser.ShowCart(); break;
                case 9: User.ListPrizes(); break;
                case 10: ((RegisteredUser)currentUser).spin(); break;
                case 11: ((Manager)currentUser).AddNewProduct(); break;
                case 12: ((Manager)currentUser).ModifyProduct(); break;
                case 13: ((Manager)currentUser).DeleteProduct(); break;
                case 14: ((Manager)currentUser).GetTotalRevenue(); break;
                case 15:
                    ChangeDiscounts();
                    break;
                case 16:
                    Logout();
                    break;
                case 17:
                    WriteTxts();
                    Environment.Exit(0); 
                    break;
                default:
                    WriteError("Invalid input! Try again:");
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
            if (currentUser.GetType() != typeof(Guest))
                WriteSucces("You successfully logged in!");
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
            Admin.ListUsers();
            if (currentUser.Type() != "Admin")
            {
                return;
            }

            Console.WriteLine("\nusername: ");
            string username = Console.ReadLine();

            Admin.AddManagerRole(username);
        }

        //TODO szépen kiírni
        public void ListUsers()
        {
            if (currentUser.Type() != "Admin")
            {
                Console.WriteLine("How did you get here? You're no admin!");
                return;
            }

            Admin.ListUsers();
        }
        
        //TODO szépen megírni a kiírást
        public void SearchUser()
        {
            while (true)
            {
                Console.WriteLine("Username you want to search for: ");
                string username = Console.ReadLine()!;
                if (username == "0") break;
                    
                var user = Admin.SearchUser(username);
                if (user is null)
                {
                   WriteError("There is no user with this username! Try again or type '0' to exit");
                   continue;
                }

                Console.WriteLine($"{user.Type()} | username: {user.username} | name: {user.name} | birthdate: {user.birth} | last spin:{user.lastSpin}");
                break;
            }
        }

        //TODO szépen megírni a kiírást, lehetne szebben
        public void DeleteUser()
        {
            ListUsers();
            if (currentUser.GetType() != typeof(Admin) && currentUser.GetType() != typeof(Manager))
            {
                return;
            }

            while (true)
            {
                Console.WriteLine("Username of the user you want to delete or type '0' the exit: ");
                string username = Console.ReadLine();
                if (username == "0") break;
                var user = Admin.DeleteUser(username);

                if (user is null)
                {
                    WriteError("There is no user with this username! Try again or type '0' to exit"); 
                    continue;
                }
                
                UserStorage.RemoveUser(username);
                WriteSucces($"User {username}Successfully deleted!");
                break;
            }

            Console.WriteLine("User Successfully deleted!");
            AdminMenu();
        }

        public void ChangeDiscounts()
        {
            User.ListPrizes();
            Console.Write("1.add new discount \n2.delete a discount");
            Console.Write("Please select an option: ");
            string res = Console.ReadLine();
            switch (res)
            {
                case "1":
                {
                    int discount = -1;
                    while (true)
                    {
                        try
                        {
                            Console.Write("Enter the discount: ");
                            discount = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("This is not a number!");
                        }
                        Console.WriteLine(discount);

                        if (discount is <= 0 or >= 100)
                        {
                            WriteError("Prize should be between 0 and 100!");
                            continue;
                        }
                        break;
                    }

                    Console.WriteLine(discount);
                    Manager.AddNewPrize(discount);
                   Program.WriteSucces("Discount successfully added!");
                    break;
                }
                case "2":
                {
                    int prizeID = -1;
                    while (prizeID < 0)
                    {
                        try
                        {
                            Console.Write("Enter the id of the discount: ");
                            prizeID = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (Exception e)
                        {
                            WriteError("This is not a number!");
                        }
                    }

                    if (prizeID > LuckyWheel.getPrizes().Count)
                    {
                        WriteError("There is no prize with this id!\n Returning to Menu");
                        return;
                    }

                    Manager.DeletePrize(prizeID);

                    WriteSucces("Prize successfully deleted!");
                    break;
                }
                default:
                {
                    WriteError("Invalid input!");
                    break;
                }
            }
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