using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AzureClientUI.Models
{
    class RemoteDeviceConnectorModel : BaseModel
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

        public override IEnumerable GetErrors([CallerMemberName] string propertyName = null)
        {
            foreach (var obj in base.GetErrors(propertyName))
            {
                yield return obj;
            }

            if (string.IsNullOrEmpty(propertyName) || propertyName == nameof(Address))
            {
                if (string.IsNullOrWhiteSpace(address))
                {
                    yield return "No address provided";
                }
            }

            if (string.IsNullOrEmpty(propertyName) || propertyName == nameof(Port))
            {
                short port = 0;
                if (!Int16.TryParse(Port, out port))
                {
                    yield return "Port invalid";
                }
            }
        }
    }
}
