using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VismaProject.Models
{
    internal class MainMenu
    {
        public static void MainMenuInformation()
        {
            while(!Session.IsUserLoggedIn())
            {
                Console.Write("Login - 1" +
                              "\nRegister - 2" +
                              "\nExit - 3" +
                              "\nChoose a number: ");

                string selection = Console.ReadLine();

                switch (selection)
                {
                    case "1":
                        Console.Clear();
                        Login.LoginUserInterface();
                        break;

                    case "2":
                        Console.Clear();
                        Registration.RegisterUserInterface();
                        break;
                    case "3":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid selection please select 1 or 2\n");
                        break;
                }
            }
            Console.Clear();
            Console.WriteLine("Succesfully logged in" +
                              $"\nWelcome {Session.user}!");

            bool exit = false;

            while (!exit || !Session.IsUserLoggedIn())
            {
                Console.WriteLine("\nCreate a new meeting - 1" +
                                  "\nDelete a meeting - 2" +
                                  "\nAdd person to a meeting - 3" +
                                  "\nRemove person from a meeting - 4" +
                                  "\nFilter meetings - 5" +
                                  "\nShow all meetings - 6" +
                                  "\nLogout - 7" +
                                  "\nExit - 8");

                string selection = Console.ReadLine();

                switch (selection)
                {
                    case "1":
                        Console.Clear();
                        MeetingControl.CreatingMeeting();
                        break;
                    case "2":
                        Console.Clear();
                        MeetingControl.DeletingMeeting();
                        break;
                    case "3":
                        Console.Clear();
                        MeetingControl.AddPersonToMeeting();
                        break;
                    case "4":
                        Console.Clear();
                        MeetingControl.RemovingPersonFromMeeting();
                        break;
                    case "5":
                        MeetingControl.MeetingFilteringOptions();
                        break;
                    case "6":
                        Console.Clear();
                        MeetingControl.ShowAllMeetings();
                        break;
                    case "7":
                        Console.Clear();
                        Session.SetUserLoggedOut();
                        Console.WriteLine("Succesfully logged out\n");
                        MainMenuInformation();
                        break;
                    case "8":
                        exit = true;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid selection! Please select from 1 to 7\n");
                        break;
                }
            }
        }

    }
}
