using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Lab04Shvachka.Stores;
using Lab04Shvachka.Tools;
using Lab04Shvachka.Commands;
using Lab04Shvachka.Exceptions;
using Lab04Shvachka.Models;
using Lab04Shvachka.Services;
using System.Windows.Media;

namespace Lab04Shvachka.ViewModels
{
    public class AddPersonMenuViewModel : ViewModelBase
    {

        #region Commands
        public ICommand NavigatePesonDataDisplayCommand { get; }
        public ICommand AddUserCommand {  get; }

        private bool CanExecute(object obj)
        {
            ToolTipText = UpdateToolTipText();
            return !String.IsNullOrWhiteSpace(Name) && !String.IsNullOrWhiteSpace(Surname) && !String.IsNullOrWhiteSpace(Email);
        }
        #endregion

        #region Fields
        private string _name;
        private string _surname;
        private string _email;
        private DateTime _selectedDate;
        private bool _isEnabled;
        private string _toolTipText;
        #endregion

        #region Properties
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        public string Surname
        {
            get { return _surname; }
            set
            {
                if (_surname != value)
                {
                    _surname = value;
                    OnPropertyChanged(nameof(Surname));
                }
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }

        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                if (_selectedDate != value)
                {
                    _selectedDate = value;
                    OnPropertyChanged(nameof(SelectedDate));
                }
            }
        }

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                if (_isEnabled != value)
                {
                    _isEnabled = value;
                    OnPropertyChanged(nameof(IsEnabled));
                }
            }
        }
        public string ToolTipText
        {
            get { return _toolTipText; }
            set
            {
                if (_toolTipText != value)
                {
                    _toolTipText = value;
                    OnPropertyChanged(nameof(ToolTipText));
                }
            }
        }

        public MessageViewModel MessageViewModel { get; }
        public string Message { set => MessageViewModel.Message = value; }
        public Color MessageColor { set => MessageViewModel.Color = value; }

        #endregion


        public AddPersonMenuViewModel(NavigationStore navigationStore)
        {
            SelectedDate = DateTime.Today;
            IsEnabled = true;
            MessageViewModel = new MessageViewModel();
            AddUserCommand = new AddUserCommand(this, CanExecute);
            NavigatePesonDataDisplayCommand = new NavigateCommand<PersonDataDisplayViewModel>(navigationStore, () => new PersonDataDisplayViewModel(navigationStore));
        }


        #region Additional Methods
        public void ClearData()
        {
            Name = String.Empty;
            Surname = String.Empty;
            Email = String.Empty;
            SelectedDate = DateTime.Today;
        }

        private string UpdateToolTipText()
        {
            if (String.IsNullOrWhiteSpace(Name))
            {
                return "Name field is empty.";
            }
            if (String.IsNullOrWhiteSpace(Surname))
            {
                return "Surname field is empty.";
            }
            if (String.IsNullOrWhiteSpace(Email))
            {
                return "Email field is empty.";
            }
            return "Click to add new person.";
        }
        #endregion
    }
}