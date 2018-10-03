using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro;
using MahApps.Metro.Controls;
using AzureClientUI.ViewModels;
namespace AzureClientUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ListView_Loaded(object sender, RoutedEventArgs e)
        {
            var listView = sender as ListView;
            if (listView != null)
            {
                var gvhrp = FindFirstVisual<GridViewHeaderRowPresenter>(listView);
                if (gvhrp != null)
                {
                    var separator = FindFirstVisual<Separator>(gvhrp);
                    if (separator != null)
                    {
                        separator.Background = Brushes.Red;
                    }
                }
            }
        }

        T FindFirstVisual<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        return (T)child;
                    }

                    var result = FindFirstVisual<T>(child);
                    if (result != null)
                    {
                        return result;
                    }
                }
            }

            return null;
        }
    }
}
