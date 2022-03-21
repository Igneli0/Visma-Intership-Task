using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VismaProject.Models
{
    internal class Registration
    {
        public static void RegisterUserInterface()
        {
            Console.Write("Enter your new username: ");
            string username = Console.ReadLine();
            if (!CheckingIfPersonIsAlreadyMember(username))
            {
                Console.Clear();
                Console.WriteLine("This user already exists\n");
                return;
            }
            Console.Write("Enter your new password: ");
            string password = Console.ReadLine();
            Console.Write("Re-enter your new password: ");
            string repassword = Console.ReadLine();

            //Checking does password match
            bool passwordCheck = CheckingPassword(password, repassword);

            if (passwordCheck)
            {
                User newUser = new User();
                newUser.Name = username;
                newUser.Password = password;

                //Adding new user to database and saving
                Console.Clear();
                Console.WriteLine("Succesfully registered");
                DB.users.Add(newUser);
                DB.SaveUserData();
            }
        }

        static bool CheckingPassword(string password, string repassword)
        {
            if (password == repassword)
            {
                return true;
            }
            else
            {
                Console.WriteLine("Wrong password input");
                return false;
            }    
        }

        public static bool CheckingIfPersonIsAlreadyMember(string name)
        {
            var userList = DB.users;

            foreach (var user in userList.Select(x => x.Name))
            {
                if (user != name)
                {
                    continue;
                }
                return false;
            }
            return true;
        }

    }
}
