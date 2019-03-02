using System.ComponentModel;
using System.Runtime.CompilerServices;

using OperatingSystem = AzureClientUI.DataObjects.OperatingSystem;

namespace AzureClientUI.Models
{
    class Device : INotifyPropertyChanged
    {
        private string name;
        private string address;
        private string acpVersion;
        private OperatingSystem operatingSystem;

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

        public OperatingSystem OperatingSystem
        {
            get => operatingSystem;
            set
            {
                if (operatingSystem == value) return;

                operatingSystem = value;
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
