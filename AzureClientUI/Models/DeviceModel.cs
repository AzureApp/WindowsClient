using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AzureClientUI.Models
{
    class DeviceModel : INotifyPropertyChanged
    {
        private string name;
        private string address;
        private string port;
        private string acpVersion;

        public string Name
        {
            get => name;
            set
            {
                if (name == value) return;

                name = value;
                OnPropertyChanged();
            }
        }

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

        public string ACPVersion
        {
            get => ACPVersion;
            set
            {
                if (acpVersion == value) return;

                acpVersion = value;
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
