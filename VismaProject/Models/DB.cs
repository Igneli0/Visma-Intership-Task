using System;
using Newtonsoft.Json;
using VismaProject.Models;

namespace VismaProject
{
    internal class DB
    {   
        //Lists
        public static List<User> users = new List<User>();
        public static List<Meeting> meetings = new List<Meeting>();
        //Files
        const string usersDataFile = "Users.json";
        const string meetingDataFile = "Meetings.json";
        public static void SaveUserData()
        {
            var save = JsonConvert.SerializeObject(users);
            File.WriteAllText(usersDataFile, save);
        }
        public static void LoadUserData()
        {
            if (File.Exists(usersDataFile))
            {
                var loadData = File.ReadAllText(usersDataFile);
                users = JsonConvert.DeserializeObject<List<User>>(loadData);
            }
            else
            {
                Console.WriteLine("No users found. Let's create a user");
                Registration.RegisterUserInterface();
                SaveUserData();
            }
        }
        public static void SaveMeetingData()
        {
            var save = JsonConvert.SerializeObject(meetings);
            File.WriteAllText(meetingDataFile, save);
        }
        public static void LoadMeetingData()
        {
            if (File.Exists(meetingDataFile))
            {
                var loadMeeting = File.ReadAllText(meetingDataFile);
                meetings = JsonConvert.DeserializeObject<List<Meeting>>(loadMeeting);
            }
            else
            {
                File.Create(meetingDataFile).Close();
                LoadMeetingData();
            }
        }
    }
}
