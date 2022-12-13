using System;
using System.Text;

namespace LaptopWebshop
{
	public static class UserStorage
	{
		//static List<User> users = new List<User>();
        static Dictionary<string, User> users = new();

		public static List<User> GetUsers() =>  new(users.Values);

        public static User GetUser(string username)
        {
            return users[username];
        }
        
        //TODO set user -> enélkül nem fogsz tudni hozzáadni a dictionaryhez

        public static bool IsUsernameTaken(string username) => users.ContainsKey(username);//users.Any(a => a.username.Equals(username));

        //
        public static bool IsValid(string username, string password) => users.Any(a => a.Value.IsIt(username, password));

        public static bool IsValidUsername(string username) => users.Any(a => a.Value.username.Equals(username));

        // public static User LogIn(string username) => users[username];

        // public static void Register(string username, string name, string password, DateOnly birth) => users.Add(username, new RegisteredUser(username, name, password, birth));

        // public static void AddManagerRule(User u) => users[u.username] = new Manager(u);

        public static void ReadUsersTxt()
        {
            users.Clear();

            if (File.Exists("registeredUsers.txt"))
                foreach (User u in File.ReadAllLines("registeredUsers.txt").Where(a => a.Split(';').Length == 6).Select(a => new RegisteredUser(a)).DistinctBy(a => a.username).Where(a => !users.ContainsKey(a.username)).ToList())
                    users.Add(u.username, u);

            if (File.Exists("managers.txt"))
                foreach (User u in File.ReadAllLines("managers.txt").Where(a => a.Split(';').Length == 6).Select(a => new Manager(a)).DistinctBy(a => a.username).Where(a => !users.ContainsKey(a.username)).ToList())
                    users.Add(u.username, u);

            if (File.Exists("admins.txt"))
                foreach (User u in File.ReadAllLines("admins.txt").Where(a => a.Split(';').Length == 6).Select(a => new Admin(a)).DistinctBy(a => a.username).Where(a => !users.ContainsKey(a.username)).ToList())
                    users.Add(u.username, u);
        }

        public static void WriteToTxt()
        {
            TextWriter ruw = new StreamWriter("registeredUsers.txt", false, Encoding.UTF8);
            TextWriter mw = new StreamWriter("managers.txt", false, Encoding.UTF8);
            TextWriter aw = new StreamWriter("admins.txt", false, Encoding.UTF8);

            foreach (User u in users.Select(a => a.Value))
                if(u.Type().Equals("Registered user"))
                    ruw.WriteLine(u.FormatToTxt());
                else if (u.Type().Equals("Manager"))
                    mw.WriteLine(u.FormatToTxt());
                else if (u.Type().Equals("Admin"))
                    aw.WriteLine(u.FormatToTxt());

            ruw.Close();
            mw.Close();
            aw.Close();
        }

        // public static string getType(string username, string password)
        // {
        //     if (users.Any(a => a.Value.IsIt(username, password)))
        //         return users[username].Type();
        //
        //     return "Not found";
        // }
    }
}

