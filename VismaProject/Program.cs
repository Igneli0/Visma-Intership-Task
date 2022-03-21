using System.Text.Json;
using System.Text.Json.Serialization;
using VismaProject;
using VismaProject.Models;

internal class Program
{
    static void Main(string[] args)
    {
        DB.LoadUserData();
        DB.LoadMeetingData();
        Console.WriteLine("Visma internal meeting manager\n");
        MainMenu.MainMenuInformation();
    }
}