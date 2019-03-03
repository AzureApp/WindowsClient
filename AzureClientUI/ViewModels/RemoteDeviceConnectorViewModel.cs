﻿using AzureClientUI.Helpers;
using AzureClientUI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AzureClientUI.ViewModels
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
        }

        private bool CanConnect(object param)
        {
            return (RemoteDevice.Address != null && RemoteDevice.Port != null);
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
