using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Lab04Shvachka.Stores;
using Lab04Shvachka.Tools;
using Lab04Shvachka.Commands;

namespace Lab04Shvachka.ViewModels
{
    public class AddPersonMenuViewModel : ViewModelBase
    {

        public ICommand NavigatePesonDataDisplayCommand { get; }

        public AddPersonMenuViewModel(NavigationStore navigationStore)
        {
            NavigatePesonDataDisplayCommand = new NavigateCommand<PersonDataDisplayViewModel>(navigationStore, () => new PersonDataDisplayViewModel(navigationStore));
        }

    }
}
