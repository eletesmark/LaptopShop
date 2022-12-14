using System;
using System.Text;

namespace LaptopWebshop
{
	public static class UserStorage
	{
		//static List<User> users = new List<User>();
        private static Dictionary<string, RegisteredUser> users = new();

		public static List<RegisteredUser> GetUsers() =>  new(users.Values);

        public static RegisteredUser GetUser(string username)
        {
            return users[username];
        }

        public static void AddUser(RegisteredUser u)
        {
            users[u.username] = u;
            WriteToTxt();
        }

        public static void RemoveUser(string username)
        {
            users.Remove(username);
        }

        public static bool IsUsernameTaken(string username) => users.ContainsKey(username);//users.Any(a => a.username.Equals(username));

        //
        public static bool IsValid(string username, string password) => users.Any(a => a.Value.IsIt(username, password));

        public static bool IsValidUsername(string username) => users.Any(a => a.Value.username.Equals(username));

        public static void ReadUsersTxt()
        {
            users.Clear();

            if (File.Exists("registeredUsers.txt"))
                foreach (RegisteredUser u in File.ReadAllLines("registeredUsers.txt").Where(a => a.Split('#').Length == 7).Select(a => new RegisteredUser(a)).DistinctBy(a => a.username).Where(a => !users.ContainsKey(a.username)).ToList())
                    users.Add(u.username, u);

            if (File.Exists("managers.txt"))
                foreach (RegisteredUser u in File.ReadAllLines("managers.txt").Where(a => a.Split('#').Length == 7).Select(a => new Manager(a)).DistinctBy(a => a.username).Where(a => !users.ContainsKey(a.username)).ToList())
                    users.Add(u.username, u);

            if (File.Exists("admins.txt"))
                foreach (RegisteredUser u in File.ReadAllLines("admins.txt").Where(a => a.Split('#').Length == 7).Select(a => new Admin(a)).DistinctBy(a => a.username).Where(a => !users.ContainsKey(a.username)).ToList())
                    users.Add(u.username, u);
        }

        public static void WriteToTxt()
        {
            TextWriter registeredUsers_Txt = null;// = StreamWriter.Null; //Close() miatt kell adni neki valami alap erteket
            TextWriter managers_Txt = null;// = StreamWriter.Null;
            TextWriter admins_Txt = null;// = StreamWriter.Null;
            
            try
            {
                registeredUsers_Txt = new StreamWriter("registeredUsers.txt", false, Encoding.UTF8);
                managers_Txt = new StreamWriter("managers.txt", false, Encoding.UTF8);
                admins_Txt = new StreamWriter("admins.txt", false, Encoding.UTF8);

                /*
                registeredUsers_Txt.WriteLine(string.Join("\r\n",
                    users.Where(a => a.Value.GetType() == typeof(RegisteredUser))
                        .Select(a => ((RegisteredUser)a.Value).FormatToTxt()).ToList()));
                managers_Txt.WriteLine(string.Join("\r\n", users.Where(a => a.Value.GetType() == typeof(Manager))
                    .Select(a => ((Manager)a.Value).FormatToTxt()).ToList()));
                admins_Txt.WriteLine(string.Join("\r\n", users.Where(a => a.Value.GetType() == typeof(Admin))
                    .Select(a => ((Admin)a.Value).FormatToTxt()).ToList()));
                */
                foreach (RegisteredUser u in users.Select(a => a.Value))
                    if (u.Type().Equals("Registered user"))
                        registeredUsers_Txt.WriteLine(u.FormatToTxt());
                    else if (u.Type().Equals("Manager"))
                        managers_Txt.WriteLine(u.FormatToTxt());
                    else if (u.Type().Equals("Admin"))
                        admins_Txt.WriteLine(u.FormatToTxt());
            }
            catch (IOException ioex)
            {
                Console.WriteLine("IO hiba");
            }
            catch (Exception e)
            {
                Console.WriteLine("Hiba");
                Console.WriteLine(e);
            }
            finally
            {
                registeredUsers_Txt.Close();
                managers_Txt.Close();
                admins_Txt.Close();
            }
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

