using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VismaProject.Models
{
    internal class Login
    {
        public static void LoginUserInterface()
        {
            Console.Write("Enter your username: ");
            string username = Console.ReadLine();
            Console.Write("Enter your password: ");
            string password = Console.ReadLine();
            LoginUser(username, password);
        }

        public static void LoginUser(string username, string password)
        {
            List<User> users = DB.users;

            var DBinfo = users.FirstOrDefault(user => user.Name == username && user.Password == password);
            //If user logged in correctly
            if (DBinfo != null)
            {
                Console.Clear();
                Console.WriteLine($"User {username} logged in\n");
                Session.SetUserSession(username);
            }
            //If user logged in incorrectly
            else
            {
                Console.Clear();
                Console.WriteLine("Incorrect username or password");
                LoginUserInterface();
            }
        }
    }
}
