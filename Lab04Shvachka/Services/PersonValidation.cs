using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using Lab04Shvachka.Exceptions;

namespace Lab04Shvachka.Services
{
    class PersonValidation
    {
        private static List<String> _blackList = new() { "Oleksiy Arestovych", "Illya Kyva" };

        private string _name;
        private string _surname;
        private string _email;
        private DateTime _dateTime;
        public PersonValidation(string name, string surname, string email, DateTime dt) 
        {
            _dateTime = dt;
            _name = name;
            _surname = surname;
            _email = email;
        }
        public async Task ValidatePersonValuesAsync()
        {
            if(!await Task.Run(() => EmailValidation(_email)))
                throw new EmailFormatError("Invalid email format.");
            if (!await Task.Run(() => AgeValidation(_dateTime)))
                throw new AgeError("Unacceptable age.");
            if (!await Task.Run(() => BanCheck(_name, _surname)))
                throw new BannedUserError("Get out of here, robber!");
            if (!await Task.Run(() => NameValidation(_name, _surname)))
                throw new BannedUserError("Empty name or surname value.");
        }

        public static bool EmailValidation(string email)
        {
            string emailRegexPattern = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
            return Regex.IsMatch(email, emailRegexPattern, RegexOptions.IgnoreCase);
        }

        public static bool AgeValidation(DateTime dateTime)
        {
            int age = new DateAnalyser(dateTime).CalculateAge();
            return age >= 0 && age <= 135;
        }

        public static bool BanCheck(string name, string surname)
        {
            return !_blackList.Contains($"{name} {surname}");
        }

        public static bool NameValidation(string name, string surname)
        {
            return !String.IsNullOrWhiteSpace(name) && !String.IsNullOrWhiteSpace(surname);
        }
    }
}
