﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Lab04Shvachka.Services;

namespace Lab04Shvachka.Models
{
    public class Person
    {
        #region Fields
        private string _name;
        private string _surname;
        private string _email;
        private DateTime _birthday;

        private bool _initialized = false;
        private Lazy<Task> _task;
        #endregion

        #region Properties
        public string Name { get { return _name; } }
        public string Surname { get { return _surname; } }
        public string FullName { get; private set; }
        public string Email { get { return _email; } }
        public DateTime DateOfBirth { get { return _birthday; } }

        public int Age { get; set; }
        public bool IsAdult { get; set; }
        public WesternZodiacSign WesternZodiacSign { get; set; }
        public ChineseZodiacSign ChineseZodiacSign { get; set; }
        public bool IsBirthday { get; set; }
        #endregion

        #region Constructors
        public Person(string name, string surname, string email, DateTime dateOfBirthday)
        {
            _name = name;
            _surname = surname;
            _email = email;
            _birthday = dateOfBirthday;

            FullName = $"{Name} {Surname}";
            DateAnalyser analyser = new(DateOfBirth);
            Age =analyser.CalculateAge();
            IsAdult =  analyser.IsAdult();
            IsBirthday = true;

            WesternZodiacSign = ZodiacCalculator.CalculateWesternZodiac(DateOfBirth);
            ChineseZodiacSign =  ZodiacCalculator.CalculateChineseZodiac(DateOfBirth);
        }
        public Person(string name, string surname, string email) : this(name, surname, email, DateTime.MinValue)
        {
        }
        public Person(string name, string surname, DateTime dateOfBirthday) : this(name, surname, String.Empty, dateOfBirthday)
        {
        }
        #endregion

        public async Task InitializePersonAsync()
        {
            if (!_initialized)
            {
                FullName = $"{Name} {Surname}";

                DateAnalyser analyser = new DateAnalyser(DateOfBirth);
                Age = await Task.Run(() => analyser.CalculateAge());
                IsAdult = await Task.Run(() => analyser.IsAdult());
                IsBirthday = await Task.Run(() => analyser.IsBirthdayToday());

                WesternZodiacSign = await Task.Run(() => ZodiacCalculator.CalculateWesternZodiac(DateOfBirth));
                ChineseZodiacSign = await Task.Run(() => ZodiacCalculator.CalculateChineseZodiac(DateOfBirth));
            }
        }
    }
}
