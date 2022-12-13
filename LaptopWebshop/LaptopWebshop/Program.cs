using System;
using System.Text;
using System.IO;
using System.Xml.Linq;
using LaptopWebshop;

/*
 
 -User rész kész
 -Menü elvileg kész
 
 + Termékekkel lehetne tovább menni
    + warehouse osztály tovább fejlesztése
 + Szerencsekerék osztályt létrehozni
 
*/

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

            program.GuestMenu();

            UserStorage.WriteToTxt();
        }

        //Writers
        void WriteError(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\t{0}", msg);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        void WriteSucces(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t{0}", msg);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        void WriteCustom(string msg, ConsoleColor cc)
        {
            Console.ForegroundColor = cc;
            Console.WriteLine("\t{0}", msg);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        //GUEST Menu
        void GuestMenu()
        {
            Console.WriteLine("\r\nMenu:\r\n{0}", Guest.menu);

            //Console.WriteLine("  1.Login");
            //Console.WriteLine("  2.Registration");
            //Console.WriteLine("  3.List laptops");
            //Console.WriteLine("  4.Exit");

            Console.Write("Please select an option: ");
            int.TryParse(Console.ReadLine(),
                out int n); //Beolvas egy sort és megpróbálja számmá alakítaní, ha nem sikerül 0 lesz az 'n' változó értéke

            switch (n)
            {
                case 1:
                    Login();
                    break;
                case 2:
                    Registration();
                    break;
                //case 3: ListLaptops(); break;
                case 4: return;
                default:
                    WriteError("Invalid input! Try again:");
                    GuestMenu();
                    break;
            }
        }

        //REGISTERED Menu
        void RegisteredMenu()
        {
            Console.WriteLine("\r\nMenu:\r\n{0}", RegisteredUser.menu);

            //Console.WriteLine(" -1.List laptops");
            //Console.WriteLine(" -2.Add to cart");
            //Console.WriteLine(" -3.Show cart");
            //Console.WriteLine(" -4.Logout");
            //Console.WriteLine(" -5.Exit");

            Console.Write("Please select an option: ");
            int.TryParse(Console.ReadLine(),
                out int n); //Beolvas egy sort és megpróbálja számmá alakítaní, ha nem sikerül 0 lesz az 'n' változó értéke

            switch (n)
            {
                case 1: //ListLaptops(); break;
                case 2: //AddToCart(); break;
                case 3: //ShowCart(); break;
                case 4:
                    Logout();
                    break;
                case 5: return;
                default:
                    WriteError("Invalid input! Try again:");
                    RegisteredMenu();
                    break;
            }
        }

        void ManagerMenu()
        {
            Console.WriteLine("\r\nMenu:\r\n{0}", Manager.menu);

            //Console.WriteLine(" -1.List products");
            //Console.WriteLine(" -2.Add new product");
            //Console.WriteLine(" -3.Modify product");
            //Console.WriteLine(" -4.Delete product");
            //Console.WriteLine(" -5.Stats");
            //Console.WriteLine(" -6.Change lucky-wheel's discounts");
            //Console.WriteLine(" -7.Logout");
            //Console.WriteLine(" -8.Exit");

            Console.Write("Please select an option: ");
            int.TryParse(Console.ReadLine(),
                out int n); //Beolvas egy sort és megpróbálja számmá alakítaní, ha nem sikerül 0 lesz az 'n' változó értéke

            switch (n)
            {
                case 1: //ListProducts(); break;
                case 2: //AddNewProduct(); break;
                case 3: //ModifyProduct(); break;
                case 4: //DeleteProduct(); break;
                case 5: //ShowStats(); break;
                case 6: //ChangeDiscounts(); break;
                case 7:
                    Logout();
                    break;
                case 8: return;
            }
        }

        void AdminMenu()
        {
            Console.WriteLine("\r\nMenu:\r\n{0}", Admin.menu);

            //Console.WriteLine(" -1.Add manager rule to user");
            //Console.WriteLine(" -2.List orders");
            //Console.WriteLine(" -3.List users");
            //Console.WriteLine(" -4.Search user");
            //Console.WriteLine(" -5.Delete user");
            //Console.WriteLine(" -6.Logout");
            //Console.WriteLine(" -7.Exit");

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
                    RegisteredMenu();
                    break;
            }
        }

        //GetInput
        void GetInput(ref string param, string msg, bool isPassword = false)
        {
            do
            {
                Console.Write("\t{0}: ", msg);
                param = (isPassword ? ReadPassword() : Console.ReadLine()) ??
                        string.Empty; // Ha a Console.ReadLine() null értékkel tér vissza akkor helyette 'string.Empty' lesz a param értéke (különben warning lenne, mivel a string 'non-nullable reference' típus)
                if (param.Trim().Equals(string.Empty) || param.Length < 3)
                    WriteError("Invalid input! Please try again");
            } while (param.Trim().Equals(string.Empty) || param.Length < 3);
        }

        void GetInput(ref DateOnly param, string s)
        {
            Console.Write("\t{0}: ", s);
            while (!DateOnly.TryParse(Console.ReadLine(), out param))
            {
                WriteError("Invalid input! Please try again");
                Console.Write("\t{0}: ", s);
            }
        }

        //Registration
        void Registration()
        {
            string username = string.Empty;
            string name = string.Empty;
            string password = string.Empty;
            DateOnly birth = DateOnly.MinValue;

            Console.WriteLine("\r\nRegistration form:");

            GetInput(ref username, "Username");
            while (UserStorage.IsUsernameTaken(username))
            {
                WriteError("This username is already in use!");
                GetInput(ref username, "Username");
            }

            GetInput(ref name, "Name");
            GetInput(ref password, "Password", true);

            GetInput(ref birth, "Birthday (YYYY-MM-DD)");
            
            currentUser = new RegisteredUser(username, name, password, birth);
            Guest.Register(username, name, password, birth);
            WriteSucces("You successfully registered!");
            RegisteredMenu();
        }

        //Login
        void Login()
        {
            currentUser = RegisteredUser.login();
            WriteSucces("You successfully logged in!");

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
                    GuestMenu();
                    break;
            }
        }

        //Logout
            void Logout()
        {
            //Ha a kosar nem ures akkor rakerdezes kijelentkezes elott!!!
            currentUser = new Guest();
            GuestMenu();
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