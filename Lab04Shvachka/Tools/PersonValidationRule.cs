using Lab04Shvachka.Models;
using Lab04Shvachka.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Lab04Shvachka.Tools
{
    internal class PersonValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value,
            System.Globalization.CultureInfo cultureInfo)
        {
            Person person = (value as BindingGroup).Items[0] as Person;
            if (!PersonValidation.NameValidation(person.Name, person.Surname))
            {
                return new ValidationResult(false,
                    "Name cells are empty.");
            }
            if (!PersonValidation.EmailValidation(person.Email))
            {
                return new ValidationResult(false,
                    "Incorrect email format.");
            }
            if (!PersonValidation.AgeValidation(person.DateOfBirth))
            {
                return new ValidationResult(false,
                    "UnacceptableAge");
            }
            else
            {
                return ValidationResult.ValidResult;
            }
        }
    }
}
