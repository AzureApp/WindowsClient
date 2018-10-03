using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using AzureClientUI.Models;
using MahApps.Metro.Controls.Dialogs;

namespace AzureClientUI.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public List<Process> Processes { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindowViewModel()
        {
            Processes = new List<Process>();

            for (int i = 0; i < 50; i++)
            {
                Processes.Add(new Process
                {
                    PID = 100 + i,
                    Name = String.Format("Test{0}.exe", i+1),
                    Path = String.Format("C:\\Windows\\Test{0}.exe", i+1)
                });
            }
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
