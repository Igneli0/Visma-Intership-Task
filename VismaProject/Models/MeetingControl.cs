using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VismaProject.Models
{
    internal class MeetingControl
    {
        public static void CreatingMeeting()
        {
            Meeting meet = new Meeting();

            Console.Write("Please enter meeting name: ");
            meet.Name = Console.ReadLine();
            Console.Write("Please enter responsible person: ");
            var responsiblePerson = Console.ReadLine();
            if (Registration.CheckingIfPersonIsAlreadyMember(responsiblePerson))
            {
                Console.Clear();
                Console.WriteLine("Cannot make a responsible person, user who is not registered on the system");
                return;
            }
            //Checking if user is not creating meeting by other person name
            else if (Session.user != responsiblePerson)
            {
                Console.Clear();
                Console.WriteLine("Cannot create meeting and enter other people name as responsible person");
                return;
            }
            meet.ResponsiblePerson = responsiblePerson;
            meet.People.Add(responsiblePerson);
            Console.Write("Please enter description: ");
            meet.Description = Console.ReadLine();
            Console.Write("Please select category: " +
                          "\nCodeMonkey - 1" +
                          "\nHub - 2" +
                          "\nShort - 3" +
                          "\nTeamBuilding - 4" +
                          "\nYour choice: ");
            string selection = Console.ReadLine();
            switch (selection)
            {
                case "1" :
                    meet.Category = Enum.Category.CodeMonkey;
                    break;
                case "2" :
                    meet.Category = Enum.Category.Hub;
                    break;
                case "3":
                    meet.Category = Enum.Category.Short;
                    break;
                case "4":
                    meet.Category = Enum.Category.TeamBuilding;
                    break;
            }
            Console.Write("Please select type: " +
                          "\nLive - 1" +
                          "\nInPerson - 2" +
                          "\nYour choice: ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1" :
                    meet.Type = Enum.Type.Live;
                    break;
                case "2" :
                    meet.Type = Enum.Type.InPerson;
                    break;
                default :
                    Console.WriteLine("Incorrect input");
                    break;
            }
            Console.Write("Please enter start date(yyyy-mm-dd hh:mm): ");
            var startInput = Console.ReadLine();
            meet.StartDate = DateTime.Parse(startInput);
            Console.Write("Please enter end date(yyyy-mm-dd hh:mm): ");
            var endInput = Console.ReadLine();
            meet.EndDate = DateTime.Parse(endInput);

            DB.meetings.Add(meet);
            DB.SaveMeetingData();
            Console.Clear();
            Console.WriteLine("Meeting was created");
        }

        public static void DeletingMeeting()
        {
            ShowAllMeetings();

            Console.Write("\nPlease enter meeting name which you want to delete: ");
            var input = Console.ReadLine();

            if (DB.meetings.Exists(x => x.Name == input))
            {
                var select = DB.meetings.SingleOrDefault(x => x.Name == input);

                if (Session.user == select.ResponsiblePerson)
                {
                    Console.Clear();
                    Console.Write("Are you sure you want to delete(Yes - 1, No - 2): ");
                    string inputDelete = Console.ReadLine();

                    switch (inputDelete)
                    {
                        case "1":
                            DB.meetings.Remove(select);
                            DB.SaveMeetingData();
                            Console.Clear();
                            Console.WriteLine("Succesfully deleted");
                            break;
                        case "2":
                            Console.Clear();
                            return;
                            break;
                        default:
                            Console.WriteLine("Invalid input");
                            break;
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Error, you cannot delete this meeting");
                }
            }
            else
            {
                Console.WriteLine("This meeting doesn't exists");
            }
        }

        public static void AddPersonToMeeting()
        {
            //Showing all meetings to console
            ShowAllMeetings();

            Console.Write("\nPlease enter meeting name in which you want to add a person: ");
            var meetName = Console.ReadLine();
            Console.Clear();

            //Checking if this meeting exists
            if (!DB.meetings.Exists(x => x.Name == meetName))
            {
                Console.WriteLine("This meeting doesn't exist");
                return;
            }
            Console.WriteLine($"{DB.meetings.FirstOrDefault(x => x.Name == meetName)}\n");

            Meeting selectedMeeting = DB.meetings.FirstOrDefault(x => x.Name == meetName);

            //Show persons from what to choose
            foreach (var item in DB.users.Select(x => x.Name).ToArray())
            {
                Console.WriteLine(item);
            }
            Console.Write("\nPlease choose person name which you want to add to meeting: ");
            var addPerson = Console.ReadLine();

            //Checking if this person isn't in the meeting already
            if (selectedMeeting.People.Any(x => x == addPerson))
            {
                Console.Clear();
                Console.WriteLine("This person already attends this meeting");
                return;
            }
            //Checking if this person exists as registered user
            if (Registration.CheckingIfPersonIsAlreadyMember(addPerson))
            {
                Console.Clear();
                Console.WriteLine($"Person {addPerson} doesn't exist as registered user");
                return;
            }

            var timeCheckingOverMeetings = DB.meetings.Where(x => x.StartDate >= selectedMeeting.StartDate ||
                                                            x.EndDate >= selectedMeeting.EndDate).ToList();
            //Checking if the person isn't going to another meeting at the same time
            if (timeCheckingOverMeetings.Any(x => x.People.Contains(addPerson)))
            {
                Console.Clear();
                Console.WriteLine("This person already attends another meeting at the same time");
                return;
            }

            Console.Clear();
            Console.Write($"Are you sure you want to add this person {addPerson} (Yes - 1, No - 2): ");
            int inputDelete = int.Parse(Console.ReadLine());

            if (inputDelete == 1)
            {
                if (addPerson != selectedMeeting.ResponsiblePerson ||  addPerson != null)
                {
                   DB.meetings[DB.meetings.IndexOf(selectedMeeting)].People.Add(addPerson);
                   DB.SaveMeetingData();
                   Console.Clear();
                   Console.WriteLine($"Succesfully added {addPerson} to meeting and this person will be busy from {selectedMeeting.StartDate} to {selectedMeeting.EndDate}");
                }
            }
            else if(inputDelete == 2)
            {
                Console.Clear();
            }
            else Console.WriteLine("Invalid input");

        }
        //Printing all meetings to console
        public static void ShowAllMeetings()
        {
            Console.Clear();
            var listOfMeetings = DB.meetings;
            listOfMeetings.ForEach(Console.WriteLine);
        }

        public static void FilterMeetingByDescription(string selectedDescription)
        {
            var sortedMeetings = DB.meetings.Where(x => x.Description.Contains(selectedDescription)).ToList();
            Console.Clear();
            Console.WriteLine("List of meetings filtered by description: ");
            sortedMeetings.ForEach(Console.WriteLine);
        }

        public static void FilterMeetingByCategory()
        {
            List<Meeting> sortedMeetings = DB.meetings.OrderBy(x => x.Category).ToList();
            Console.Clear();
            Console.WriteLine("List of meetings filtered by category: \n");
            sortedMeetings.ForEach(Console.WriteLine);
        }

        public static void FilterMeetingByType()
        {
            List<Meeting> sortedMeetings = DB.meetings.OrderBy(x => x.Type).ToList();
            Console.Clear();
            Console.WriteLine("List of meetings filtered by type: \n");
            sortedMeetings.ForEach(Console.WriteLine);
        }
        public static void FilterMeetingByResponsiblePerson()
        {
            List<Meeting> sortedMeetings = DB.meetings.OrderBy(x => x.ResponsiblePerson).ToList();
            Console.Clear();
            Console.WriteLine("List of meetings filtered by responsible person: \n");
            sortedMeetings.ForEach(Console.WriteLine);
        }

        public static void FilterMeetingByDates()
        {
            //Start time
            Console.Write("Please enter start date: "); 
            var start = Console.ReadLine();
            DateTime startTime = new DateTime();
            DateTime.TryParse(start, out startTime);

            //End time
            Console.Write("Please enter end date: ");
            var end = Console.ReadLine();
            DateTime endTime = new DateTime();
            DateTime.TryParse(end, out endTime);

            var temp = DB.meetings.Where(x => x.StartDate > startTime && x.EndDate < endTime);
            var result = temp.Select(x => x.Name + " meeting starts: " + x.StartDate + " , meeting ends: " + x.EndDate).ToList();
            result.ForEach(Console.WriteLine);
        }

        public static void FilterMeetingByAttendees()
        {
            Console.WriteLine("Please write a number of least attendees you want to see: ");
            int input = int.Parse(Console.ReadLine());
            var temp = DB.meetings.Where(x => x.People.Count >= input);
            var result = temp.Select(x => x.Name + " number of attendees is: " + x.People.Count).ToList();
            result.ForEach(Console.WriteLine);
        }

        public static void MeetingFilteringOptions()
        {
            Console.Clear();
            Console.WriteLine("Please select how to filter meetings by:" +
                              "\n\nBy description - 1" +
                              "\nBy responsible person - 2" +
                              "\nBy category - 3" +
                              "\nBy type - 4" +
                              "\nBy dates - 5" +
                              "\nBy number of attendees - 6" +
                              "\nExit - 7" +
                              "\n\nPlease select from 1-7:");

            int select = int.Parse(Console.ReadLine());

            switch (select)
            {
                case 1:
                    Console.Clear();
                    Console.Write("Please enter description: ");
                    var description = Console.ReadLine();
                    FilterMeetingByDescription(description);
                    break;
                case 2:
                    Console.Clear();
                    FilterMeetingByResponsiblePerson();
                    break;
                case 3:
                    Console.Clear();
                    FilterMeetingByCategory();
                    break;
                case 4:
                    Console.Clear();
                    FilterMeetingByType();
                    break;
                case 5:
                    Console.Clear();
                    FilterMeetingByDates();
                    break;
                case 6:
                    Console.Clear();
                    FilterMeetingByAttendees();
                    break;
                case 7:
                    Console.Clear();
                    return;
                    break;
               default:
                    Console.WriteLine("Invalid selection");
                    break;

            }
        }

        public static void RemovingPersonFromMeeting()
        {
            ShowAllMeetings();

            Console.Write("\nPlease enter meeting name in which you want to delete a person: ");
            var meetName = Console.ReadLine();
            var meeting = DB.meetings.SingleOrDefault(x => x.Name == meetName);

            Console.Clear();
            Console.WriteLine(meeting);

            Console.WriteLine($"List of members from {meetName} meeting:");
            foreach (var person in meeting.People)
            {
                Console.WriteLine(person);
            }

            Console.WriteLine("\nWrite a member which you want to remove: ");
            var removeMember = Console.ReadLine();

            if(meeting.People.Any(x => x == removeMember) && meeting.ResponsiblePerson != removeMember)
            {
                Console.Clear();
                meeting.RemovePerson(removeMember);
                DB.SaveMeetingData();
                Console.WriteLine("Succesfully removed a person from meeting");
            }
            else if (meeting.ResponsiblePerson == removeMember)
            {
                Console.Clear();
                Console.WriteLine("Invalid input! Cannot remove responsible person from meeting");
            }
            else
            {
                Console.WriteLine("Invalid input");
            }

        }

    }
}
