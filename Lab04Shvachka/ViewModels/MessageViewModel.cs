using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Lab04Shvachka.ViewModels
{
    public class MessageViewModel : ViewModelBase
    {
        private string _message;
        private Color _color = Color.FromRgb(255,255,255);
        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
                OnPropertyChanged(nameof(HasMessage));
            }
        }
        public Color Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
                OnPropertyChanged(nameof(Color));
            }
        }
        public bool HasMessage => !string.IsNullOrEmpty(Message);
    }
}
