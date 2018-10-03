using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Drawing;

namespace AzureClientUI.Models
{
    public class Process : INotifyPropertyChanged
    {
        private int _pid;
        private string _name;
        private string _path;
       
        public int PID
        {
            get => _pid;
            set
            {
                if (_pid == value) return;

                _pid = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                if (_name == value) return;

                _name = value;
                OnPropertyChanged();
            }
        }

        public string Path
        {
            get => _path;
            set
            {
                if (_path == value) return;

                _path = value;
                OnPropertyChanged();
            }
        }

        public Icon Icon
        {
            get
            {
                if (_path != null)
                {
                    return Icon.ExtractAssociatedIcon(_path);
                }
                return null;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
