using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Lab04Shvachka.Commands;
using Lab04Shvachka.Models;
using Lab04Shvachka.Stores;

namespace Lab04Shvachka.ViewModels
{
    internal class PersonDataDisplayViewModel : ViewModelBase
    {
        public ObservableCollection<Person> Users { get; set; }

        public ICommand NavigateAddPersonMenuCommand { get; }
        public PersonDataDisplayViewModel(NavigationStore navigationStore)
        {
            NavigateAddPersonMenuCommand = new NavigateCommand<AddPersonMenuViewModel>(navigationStore, () => new AddPersonMenuViewModel(navigationStore));
            Users = [new Person("ADenys", "Patron", "denis29@shw.com", DateTime.Today), new Person("Denys", "Patron", "denis29@shw.com", DateTime.Today), new Person("Denys", "Patron", "denis29@shw.com", DateTime.Today), new Person("Denys", "Patron", "denis29@shw.com", DateTime.Today), new Person("Denys", "Patron", "denis29@shw.com", DateTime.Today)];

        }
    }
}
