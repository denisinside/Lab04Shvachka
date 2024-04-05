using Lab04Shvachka.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab04Shvachka.ViewModels;
using Lab04Shvachka.Stores;
using Lab04Shvachka.Models;
using System.Xml.Linq;
using Lab04Shvachka.Exceptions;
using Lab04Shvachka.Services;
using System.Windows.Media;
namespace Lab04Shvachka.Commands
{
    public class AddUserCommand : RelayCommand<object>
    {
        private static AddPersonMenuViewModel _viewmodel;
        private static PersonsStore _personsStore;
        public AddUserCommand(AddPersonMenuViewModel viewmodel, Predicate<object> canExecute)
            : base(parameter => AddUser(parameter), canExecute)
        {
            _viewmodel = viewmodel;
            _personsStore = new PersonsStore();
        }

        private static async void AddUser(object parameter)
        {
            Person person;
            _viewmodel.IsEnabled = false;
            try
            {
                await ValidatePerson();

                person = new(_viewmodel.Name, _viewmodel.Surname, _viewmodel.Email, _viewmodel.SelectedDate);
            }
            catch
            {
                return;
            }
            finally
            {
                _viewmodel.IsEnabled = true;
            }
            _personsStore.AddUser(person);
            _viewmodel.MessageColor = Color.FromRgb(0,128,0);
            _viewmodel.Message = "User was added!";
            _viewmodel.ClearData();
        }


        private static async Task ValidatePerson()
        {
            PersonValidation pv = new(_viewmodel.Name, _viewmodel.Surname, _viewmodel.Email, _viewmodel.SelectedDate);
            try
            {
                await pv.ValidatePersonValuesAsync();
            }
            catch (BannedUserError e)
            {
                _viewmodel.MessageColor = Color.FromRgb(128,0,0);
                _viewmodel.Message = e.Message;
                await Task.Delay(3000);
                Environment.Exit(1);
                throw;
            }
            catch (Exception e)
            {
                _viewmodel.MessageColor = Color.FromRgb(128, 0, 0);
                _viewmodel.Message = e.Message;
                throw;
            }
        }
    }
}
