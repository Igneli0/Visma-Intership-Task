using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VismaProject
{
    internal class Meeting
    {
        public List<string> People = new List<string>();
        public string Name { get; set; }
        public string ResponsiblePerson { get; set; }
        public string Description { get; set; }
        public Enum.Category Category { get; set; }
        public Enum.Type Type {get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Meeting(string name, string responsiblePerson, string description, Enum.Category category, Enum.Type type, DateTime startDate, DateTime endDate)
        {
            Name = name;
            ResponsiblePerson = responsiblePerson;
            Description = description;
            Category = category;
            Type = type;  
            StartDate = startDate;
            EndDate = endDate;
        }

        public Meeting()
        {
        }

        public override string ToString()
        {
            var people = People;
            var name = Name;
            var responsiblePerson = ResponsiblePerson;
            var description = Description;
            var category = Category;
            var type = Type;
            var startdate = StartDate;
            var enddate = EndDate;

            return String.Format($"\nName: {name}" +
                                 $"\nResponsible person: {responsiblePerson}" +
                                 $"\nDescription: {description}" +
                                 $"\nCategory: {category}" +
                                 $"\nType: {type}" +
                                 $"\nStart date: {startdate}" +
                                 $"\nEnd date: {enddate}\n" +
                                 $"People in meeting: {String.Join(", ", people)}");

        }

        public void RemovePerson(string username)
        {
            People.Remove(username);
        }
    }
}
