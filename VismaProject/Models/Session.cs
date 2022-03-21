using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VismaProject.Models
{
    internal class Session
    {
        public static string user;
        public static bool userLoginStatus = false;

        public static void SetUserSession(string name)
        {
            user = name;
            userLoginStatus = true;
        }

        public static bool IsUserLoggedIn()
        {
            return userLoginStatus;
        }

        public static bool SetUserLoggedOut()
        {
            return userLoginStatus = false;
        }
    }
}
