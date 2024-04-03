using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab04Shvachka.Stores;
using Lab04Shvachka.Tools;
using Lab04Shvachka.ViewModels;

namespace Lab04Shvachka.Commands
{
    public class NavigateCommand<TViewModel> : RelayCommand<object>
        where TViewModel : ViewModelBase
    {
        public NavigateCommand(NavigationStore navigationStore, Func<TViewModel> createViewModel)
            : base(parameter => Navigate(parameter, navigationStore, createViewModel))
        {
        }

        private static void Navigate(object parameter, NavigationStore _navigationStore, Func<TViewModel> _createViewModel)
        {
            _navigationStore.CurrentViewModel = _createViewModel();
        }

    }
}
