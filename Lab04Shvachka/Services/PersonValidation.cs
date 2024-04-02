﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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
            try
            {
                await Task.Run(() => EmailValidation());
                await Task.Run(() => AgeValidation());
                await Task.Run(() => BanCheck());
            }
            catch
            {
                throw;
            }
        }

        private void EmailValidation()
        {
            string emailRegexPattern = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
            if (!Regex.IsMatch(_email, emailRegexPattern, RegexOptions.IgnoreCase))
                throw new EmailFormatError();
        }

        private void AgeValidation()
        {
            DateAnalyser da = new(_dateTime);
             int age = da.CalculateAge();
            if(age < 0)
                throw new TooYoungAgeError();
            if (age > 135)
                throw new TooOldAgeError();
        }

        private void BanCheck()
        {
            var fullName = $"{_name} {_surname}";
            if(_blackList.Contains(fullName))
                throw new BannedUserError();
        }
    }
}
