namespace LaptopWebshop
{
    public class Admin : Manager
    {
        public static readonly string menu =
            "  1.Add manager rule to user\r\n  2.List orders\r\n  3.List users\r\n  4.Search user\r\n  5.Delete user\r\n  6.Logout\r\n  7.Exit";

        public Admin(string username, string name, string password, DateOnly birth) : base(username, name, password,
            birth)
        {
        }

        public Admin(string line) : base(line)
        {
        }

        public override string Type() => "Admin";


        //BUG rosszul menti el az új managert: más a jelszó mint az eredeti
        public static void AddManagerRole(string username)
        {
            RegisteredUser user = UserStorage.GetUser(username) ;
            if (user == null) return;
            if (user.GetType() == typeof(RegisteredUser))
            {
                user = new Manager(user.username, user.name, user.password, user.birth);
                UserStorage.AddUser(user);
            }
            else throw new Exception("You cannot give manager role to this user!");
        }
    //TODO ha kész lesz az order
        // public static void ListOrders()
        // {
        //     foreach (var order in OrderStorage.Orders)
        //     {
        //         Console.WriteLine(order);
        //     }
        // }
        
        public static List<RegisteredUser> ListUsers()
        {
            if (UserStorage.GetUsers().Count < 1)
            {
                throw new Exception("There are no users!");
                //return null;
            }
            return UserStorage.GetUsers();
            
        }
        
        public static RegisteredUser SearchUser(string username)
        {
            foreach (var user in UserStorage.GetUsers())
            {
                if (user.username == username)
                {
                    return user;
                }
            }
            return null;
        }
        
        public static void DeleteUser(string username)
        {
            UserStorage.GetUsers().Remove(UserStorage.GetUser(username));
        }
        
        
    }
}