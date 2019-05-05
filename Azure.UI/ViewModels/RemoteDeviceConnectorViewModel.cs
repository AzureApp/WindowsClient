using Azure.UI.Helpers;
using Azure.UI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Grpc.Core;
using Azure.Proto;
using Grpc.Core;

namespace Azure.UI.ViewModels
{
    class RemoteDeviceConnectorViewModel : INotifyPropertyChanged
    {
        public RemoteDeviceConnectorModel RemoteDevice { get; set; }
        public ICommand ConnectCommand { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public RemoteDeviceConnectorViewModel()
        {
            RemoteDevice = new RemoteDeviceConnectorModel
            {
                Port = "1248"
            };
            ConnectCommand = new RelayCommand(Connect, CanConnect);
        }

        private void Connect(object param)
        {
            Console.WriteLine("connecting to {0}:{1}", RemoteDevice.Address, RemoteDevice.Port);

            int port;

            if (!Int32.TryParse(RemoteDevice.Port, out port))
            {
                return;
            }

            // This code is only temporary
            App app = (App)Application.Current;

            Channel channel = new Channel(RemoteDevice.Address, port, ChannelCredentials.Insecure);

            var connectionClient = new ConnectionService.ConnectionServiceClient(channel);

            try
            {
                var reply = connectionClient.Connect(new Handshake
                {
                    AzureVersion = "1.0.0",
                    DeviceInfo = new DeviceInfo
                    {
                        DeviceName = "Windows PC",
                        OperatingSystem = DeviceInfo.Types.OperatingSystem.OsWindows,
                        OsVersion = "10",
                        Udid = ""
                    }
                });
            }
            catch (RpcException e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private bool CanConnect(object param)
        {
            return !RemoteDevice.HasErrors;
        }

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
