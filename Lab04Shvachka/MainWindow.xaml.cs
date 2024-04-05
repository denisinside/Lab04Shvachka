using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Lab04Shvachka.Models;
using Lab04Shvachka.Services;
using Lab04Shvachka.Stores;
using Lab04Shvachka.ViewModels;

namespace Lab04Shvachka
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            NavigationStore navigationStore = new NavigationStore();
            navigationStore.CurrentViewModel = new PersonDataDisplayViewModel(navigationStore);
            DataContext = new MainViewModel(navigationStore);
            InitializeComponent();
        }
    }
}