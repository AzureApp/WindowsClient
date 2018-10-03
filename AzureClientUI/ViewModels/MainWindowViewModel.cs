using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MahApps.Metro.Controls.Dialogs;
using System.Diagnostics;
namespace AzureClientUI.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public List<Models.Process> Processes { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindowViewModel()
        {
            Processes = new List<Models.Process>();

            var processes = Process.GetProcesses();

            foreach (var process in processes)
            {
                if (!String.IsNullOrEmpty(process.MainWindowTitle))
                {
                    try
                    {
                        Processes.Add(new Models.Process
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

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
