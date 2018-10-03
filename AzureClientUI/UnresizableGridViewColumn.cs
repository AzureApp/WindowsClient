using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AzureClientUI
{
    public class UnresizableGridViewColumn : GridViewColumn
    {
        public double FixedWidth
        {
            get { return (double)GetValue(FixedWidthProperty); }
            set { SetValue(FixedWidthProperty, value); }
        }

        public static readonly DependencyProperty FixedWidthProperty =
            DependencyProperty.Register("FixedWidth", typeof(double), typeof(UnresizableGridViewColumn),
            new FrameworkPropertyMetadata(double.NaN, new PropertyChangedCallback(OnFixedWidthChanged)));

        static UnresizableGridViewColumn()
        {
            WidthProperty.OverrideMetadata(typeof(UnresizableGridViewColumn),
                new FrameworkPropertyMetadata(null, new CoerceValueCallback(OnCoerceWidth)));
        }

        private static object OnCoerceWidth(DependencyObject o, object baseValue)
        {
            UnresizableGridViewColumn fwc = o as UnresizableGridViewColumn;
            if (fwc != null)
                return fwc.FixedWidth;
            return 0.0;
        }

        private static void OnFixedWidthChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            UnresizableGridViewColumn fwc = o as UnresizableGridViewColumn;
            if (fwc != null)
                fwc.CoerceValue(WidthProperty);
        }
    }
}
