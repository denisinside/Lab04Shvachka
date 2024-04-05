using Lab04Shvachka.Models;
using Lab04Shvachka.Repositories;
using Lab04Shvachka.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab04Shvachka.Stores
{
    public enum SortTypes
    {
        Name,
        Surname,
        Email,
        Age,
        Birthday
    }
    public enum SortOrder
    {
        Descending,
        Ascending
    }
    public class PersonsStore
    {
        

        private static readonly object Locker = new object();
        private static BindingList<Person> _users;
        private static FileRepository _repository;
        private static bool _isSavingData = false;


        public static BindingList<Person> Users
        {
            get
            {
                if (_users != null)
                    return _users;
                lock (Locker)
                {
                    return _users;
                }
            }

        }
        public PersonsStore()
        {
        }

        public void AddUser(Person person)
        {
            Users.Add(person);
        }
        public void RemoveUser(Person person)
        {
            Users.Remove(person);
        }
        public void RemoveUser(int index)
        {
            Users.RemoveAt(index);
        }
        public void Clear()
        {
            Users.Clear();
        }
        public async void Sort(SortTypes type, SortOrder order)
        {
            if (_users == null)
                await Init();
            Users.ListChanged -= SaveDataAsync;
            BindingList<Person> _sortedUsers;
            switch (type)
            {
                case SortTypes.Name:
                    _sortedUsers = order == SortOrder.Ascending ? new BindingList<Person>(Users.OrderBy(p => p.Name).ToList()) :
                                                                  new BindingList<Person>(Users.OrderByDescending(p => p.Name).ToList());
                    break;
                case SortTypes.Surname:
                    _sortedUsers = order == SortOrder.Ascending ? new BindingList<Person>(Users.OrderBy(p => p.Surname).ToList()) :
                                                                  new BindingList<Person>(Users.OrderByDescending(p => p.Surname).ToList());
                    break;
                case SortTypes.Email:
                    _sortedUsers = order == SortOrder.Ascending ? new BindingList<Person>(Users.OrderBy(p => p.Email).ToList()) :
                                                                  new BindingList<Person>(Users.OrderByDescending(p => p.Email).ToList());
                    break;
                case SortTypes.Age:
                    _sortedUsers = order == SortOrder.Ascending ? new BindingList<Person>(Users.OrderBy(p => p.Age).ToList()) :
                                                                  new BindingList<Person>(Users.OrderByDescending(p => p.Age).ToList());
                    break;
                case SortTypes.Birthday:
                    _sortedUsers = order == SortOrder.Ascending ? new BindingList<Person>(Users.OrderBy(p => p.IsBirthday).ToList()) :
                                                                  new BindingList<Person>(Users.OrderByDescending(p => p.IsBirthday).ToList());
                    break;
                default:
                    throw new ArgumentException("Invalid Sort Type.");
            }

            Users.Clear();

            foreach (var user in _sortedUsers)
            {
                Users.Add(user);
            }
            Users.ListChanged += SaveDataAsync;
        }

        private static async void SaveDataAsync(object? sender, ListChangedEventArgs e)
        {
            if (!_isSavingData && (e.ListChangedType == ListChangedType.ItemChanged ||
        e.ListChangedType == ListChangedType.ItemAdded ||
        e.ListChangedType == ListChangedType.ItemDeleted))
            {
                _isSavingData = true;
                await _repository.AddOrUpdateAsync(Users);
                _isSavingData = false;
            }
        }

        private static async Task Init()
        {
            _repository = new FileRepository();
            _users = await _repository.GetAsync();
            if (_users == null)
            {
                List<Person> people = new UserGenerator().GetPersonList();
                _users = new BindingList<Person>(people);
            }
            await _repository.AddOrUpdateAsync(_users);
            _users.ListChanged += SaveDataAsync;
        }
    }
}
