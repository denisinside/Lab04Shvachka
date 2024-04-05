using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using Lab04Shvachka.Services;

namespace Lab04Shvachka.Models
{
    public class Person : INotifyPropertyChanged
    {
        #region PropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
        #endregion

        #region Fields
        private string _name;
        private string _surname;
        private string _email;
        private DateTime _birthday;
        #endregion

        #region Properties
        public string Name { get { return _name; } set { _name = value; OnPropertyChanged(); } }
        public string Surname { get { return _surname; } set { _surname = value; OnPropertyChanged(); } }
        public string Email { get { return _email; } set { _email = value; OnPropertyChanged(); } }
        public DateTime DateOfBirth
        {
            get => _birthday;
            set
            {
                _birthday = value;
                InitializePerson();
                OnPropertyChanged();
                OnPropertyChanged(nameof(Age));
                OnPropertyChanged(nameof(IsBirthday));
                OnPropertyChanged(nameof(WesternZodiacSign));
                OnPropertyChanged(nameof(ChineseZodiacSign));
            }
        }

        public int Age { get; set; }
        public bool IsAdult { get; set; }
        public WesternZodiacSign WesternZodiacSign { get; set; }
        public ChineseZodiacSign ChineseZodiacSign { get; set; }
        public bool IsBirthday { get; set; }
        #endregion

        #region Constructors
        public Person() { }
        public Person(string name, string surname, string email, DateTime dateOfBirthday)
        {
            Name = name;
            Surname = surname;
            Email = email;
            DateOfBirth = dateOfBirthday;
        }
        public Person(string name, string surname, string email) : this(name, surname, email, DateTime.MinValue)
        {
        }
        public Person(string name, string surname, DateTime dateOfBirthday) : this(name, surname, String.Empty, dateOfBirthday)
        {
        }
        #endregion

        public void InitializePerson()
        {
                DateAnalyser analyser = new DateAnalyser(DateOfBirth);
                Age = analyser.CalculateAge();
                IsAdult = analyser.IsAdult();
                IsBirthday = analyser.IsBirthdayToday();

                WesternZodiacSign = ZodiacCalculator.CalculateWesternZodiac(DateOfBirth);
                ChineseZodiacSign = ZodiacCalculator.CalculateChineseZodiac(DateOfBirth);
        }
    }
}
