using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace AzureClientUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ConnectionManager connectionManager = new ConnectionManager();

        public ConnectionManager GetConnectionManager() => connectionManager;

        [STAThread]
        public static void Main()
        {
            var application = new App();
            application.InitializeComponent();
            application.Run();
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                ConnectionManager manager = new ConnectionManager();
                manager.Connect("127.0.0.1", 1248);
            }).Start();
        }
    }
}
