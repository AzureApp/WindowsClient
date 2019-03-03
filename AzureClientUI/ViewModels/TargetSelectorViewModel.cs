using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MahApps.Metro.Controls.Dialogs;
using System.Diagnostics;
using System.Windows.Input;
using AzureClientUI.Helpers;
using AzureClientUI.Controls;
using System.Windows;

namespace AzureClientUI.ViewModels
{
    public class TargetSelectorViewModel : INotifyPropertyChanged
    {
        public List<Models.ProcessModel> Processes { get; set; }
        public ICommand RemoteDeviceButtonCommand { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public TargetSelectorViewModel()
        {
            RemoteDeviceButtonCommand = new RelayCommand(ShowRemoteDeviceWindow, CanShowRemoteDeviceWindow);

            Processes = new List<Models.ProcessModel>();

            var processes = Process.GetProcesses();

            foreach (var process in processes)
            {
                if (!String.IsNullOrEmpty(process.MainWindowTitle))
                {
                    try
                    {
                        Processes.Add(new Models.ProcessModel
                        {
                            PID = process.Id,
                            Name = process.ProcessName,
                            Path = process.MainModule.FileName
                        });
                    }
                    catch (Win32Exception e)
                    {
                        Console.WriteLine("Could not load details about process {0} [{1}]", process, e);
                    }
                }
            }
        }

        private bool CanShowRemoteDeviceWindow(object parameter)
        {
            // return true for now, maybe check that azure state is fine first
            return true;
        }

        private void ShowRemoteDeviceWindow(object parameter)
        {
            var window = new RemoteDeviceWindow();
            window.Owner = Window.GetWindow(Application.Current.MainWindow);
            window.Show();
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
