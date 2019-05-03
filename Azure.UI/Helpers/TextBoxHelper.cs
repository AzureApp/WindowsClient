using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Azure.UI.Helpers
{
    public static class TextBoxHelper
    {
        public static readonly DependencyProperty DefaultTextProperty = DependencyProperty.RegisterAttached(
            "DefaultText",
            typeof(string),
            typeof(TextBoxHelper));

        [AttachedPropertyBrowsableForType(typeof(TextBox))]
        public static string GetDefaultText(FrameworkElement element)
        {
            return (string)element.GetValue(DefaultTextProperty);
        }

        public static void SetDefaultText(FrameworkElement element, string value)
        {
            element.SetValue(DefaultTextProperty, value);
        }
    }
}
