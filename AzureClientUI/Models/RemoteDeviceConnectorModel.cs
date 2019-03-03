using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AzureClientUI.Models
{
    class RemoteDeviceConnectorModel : INotifyPropertyChanged
    {
        private string address;
        private string port;

        public string Address
        {
            get => address;
            set
            {
                if (address == value) return;

                address = value;
                OnPropertyChanged();
            }
        }

        public string Port
        {
            get => port;
            set
            {
                if (port == value) return;

                port = value;
                OnPropertyChanged();
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
