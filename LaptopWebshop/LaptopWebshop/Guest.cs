using System;
namespace LaptopWebshop
{
	public class Guest : User
	{
        public static readonly string menu = "  1.Login\r\n  2.Registration\r\n  3.List laptops\r\n  4.Exit";

        public Guest() : base() { }

        public override string Type() => "Guest";

        public override string ToString() => "GUEST";

        //BUG nem lehet getterrel hozzáadni a userStoragehoz mert az csak kimásolja a felhasználókat egy listába
        public static void Register(string username, string name, string password, DateOnly birth)
        {
	        RegisteredUser u = new(username, name, password, birth);
	        UserStorage.GetUsers().Add( u);
	        // UserStorage.

	        List<User> users = UserStorage.GetUsers();

	        Console.WriteLine("Usernames in users:");
	        foreach (var a in users)
	        {
		        Console.WriteLine(a.username);
	        }
	        UserStorage.WriteToTxt();
        }
    }
}

