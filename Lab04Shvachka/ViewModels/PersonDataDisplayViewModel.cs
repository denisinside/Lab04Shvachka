using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Lab04Shvachka.Commands;
using Lab04Shvachka.Models;
using Lab04Shvachka.Services;
using Lab04Shvachka.Stores;
using Lab04Shvachka.Tools;

namespace Lab04Shvachka.ViewModels
{
    internal class PersonDataDisplayViewModel : ViewModelBase
    {
        #region Commands
        public ICommand NavigateAddPersonMenuCommand { get; }
        private RelayCommand<object> _addRandomUser;
        private RelayCommand<object> _deleteAllUsers;
        private RelayCommand<object> _deleteSelectedUsers;
        private RelayCommand<object> _changeEditMode;
        private RelayCommand<object> _close;

        public RelayCommand<object> AddRandomUser
        {
            get
            {
                return _addRandomUser ??= new RelayCommand<object>(_ => PersonsStore.AddUser(_userGenerator.GetPerson()));
            }
        }
        public RelayCommand<object> DeleteAllUsers
        {
            get
            {
                return _deleteAllUsers ??= new RelayCommand<object>(_ => PersonsStore.Clear());
            }
        }
        public RelayCommand<object> DeleteSelectedUsers
        {
            get
            {
                return _deleteSelectedUsers ??= new RelayCommand<object>(_ => RemoveSelectedUsers());
            }
        }
        public RelayCommand<object> ChangeEditMode
        {
            get
            {
                return _changeEditMode ??= new RelayCommand<object>(_ => EditMode = !EditMode);
            }
        }
        public RelayCommand<object> CloseCommand
        {
            get
            {
                return _close ??= new RelayCommand<object>(_ => Environment.Exit(0));
            }
        }
        #endregion

        #region Fields
        private MessageViewModel _messageViewModel;
        private bool _ascendingMode;
        private bool _descendingMode;
        private int _selectedSortMode;
        private UserGenerator _userGenerator;
        private int _selectedPersonIndex;
        private bool _editMode;
        #endregion

        #region Properties
        public PersonsStore PersonsStore { get; set; }
        public MessageViewModel MessageViewModel
        {
            get => _messageViewModel;
            set
            {
                _messageViewModel = value;
                OnPropertyChanged(nameof(MessageViewModel));
            }
        }
        public bool AscendingMode
        {
            get => (_ascendingMode);
            set
            {
                if (_ascendingMode != value)
                {
                    _ascendingMode = value;
                    SortPersons();
                    OnPropertyChanged(nameof(AscendingMode));
                }
            }
        }
        public bool DescendingMode
        {
            get => (_descendingMode);
            set
            {
                if (_descendingMode != value)
                {
                    _descendingMode = value;
                    SortPersons();
                    OnPropertyChanged(nameof(DescendingMode));
                }
            }
        }
        public int SelectedSortMode
        {
            get => _selectedSortMode;
            set
            {
                if (_selectedSortMode != value)
                {
                    _selectedSortMode = value;
                    SortPersons();
                    OnPropertyChanged(nameof(SelectedSortMode));
                }
            }
        }

        public int SelectedPersonIndex
        {
            get => _selectedPersonIndex;
            set
            {
                _selectedPersonIndex = value;
                OnPropertyChanged(nameof(SelectedPersonIndex));
            }
        }
        public bool EditMode
        {
            get => _editMode;
            set
            {
                _editMode = value;
                OnPropertyChanged(nameof(EditMode));
                OnPropertyChanged(nameof(EditModeStatus));
            }
        }
        public string EditModeStatus
        {
            get
            {
                return EditMode ? "Edit OFF" : "Edit ON";
            }
        }
        #endregion

        public PersonDataDisplayViewModel(NavigationStore navigationStore)
        {
            PersonsStore = new PersonsStore();
            AscendingMode = true;
            SelectedPersonIndex = 0;
            SelectedSortMode = 0;
            _userGenerator = new();
            NavigateAddPersonMenuCommand = new NavigateCommand<AddPersonMenuViewModel>(navigationStore, () => new AddPersonMenuViewModel(navigationStore));
        }

        private void RemoveSelectedUsers()
        {
            while(SelectedPersonIndex != -1)
                PersonsStore.RemoveUser(SelectedPersonIndex);
        }
        private void SortPersons()
        {
            PersonsStore.Sort((SortTypes)SelectedSortMode, (SortOrder)(DescendingMode ? 0 : 1));
        }
    }
}
