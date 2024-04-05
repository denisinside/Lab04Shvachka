using Lab04Shvachka.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab04Shvachka.Services
{
    public class UserGenerator
    {
        readonly Random rnd = new Random();
        string[] names = { "Denys", "Volodymyr", "Pes", "Dmytro", "Andriy", "Anna", "Anastasia", "Meow", "Petro", "Viktor", "Oksana", "Marina", "Mariya", "Yaroslav", "Vasya" };
        string[] surnames = { "Shvachka", "Yablonsky", "Zaporozhets", "Prokofiev", "Poroshenko", "Patron", "Biden", "Bdzhilka", "Roblox", "Csharp", "Kvyt", "Meow", "Shevchenko", "Ukrainian" };
        string[] email_domains = { "gmail.com", "ukr.net", "me.ow", "outlook.com", "ukma.edu.ua", "hotmail.com", "mail.com", "pes.patron" };

        DateTime lowEndDate = new DateTime(1950, 1, 1);
        int daysFromLowDate;

        public UserGenerator()
        {
            daysFromLowDate = (DateTime.Today - lowEndDate).Days;
        }

        public List<Person> GetPersonList(int count = 50)
        {
            var persons = new List<Person>();
            for (int i = 0; i < count; i++)
            {
                persons.Add(GetPerson());
            }
            return persons;
        }

        public Person GetPerson()
        {
            string name = GetRandomValue(names);
            string surname = GetRandomValue(surnames);
            string email = GetRandomEmail(name, surname);
            DateTime dateTime = lowEndDate.AddDays(rnd.Next(daysFromLowDate - 18 * 365));

            return new Person(name, surname, email, dateTime);
        }

        private string GetRandomValue(string[] values)
        {
            return values[rnd.Next(values.Length)];
        }

        private string GetRandomEmail(string name, string surname)
        {
            int rndNumber = rnd.Next(100, 1000);
            string nickname = rnd.Next(2) == 1 ? (surname + name) : (name + surname);
            return $"{nickname}{rndNumber}@{GetRandomValue(email_domains)}";
        }
    }
}
