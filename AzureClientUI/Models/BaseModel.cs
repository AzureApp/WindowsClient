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
    public class BaseModel : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public bool HasErrors
        {
            get
            {
                return GetErrors(null).OfType<object>().Any();
            }
        }

        public virtual void ForceValidation()
        {
            OnPropertyChanged(null);
        }

        public virtual IEnumerable GetErrors([CallerMemberName] string propertyName = null)
        {
            return Enumerable.Empty<object>();
        }

        protected void OnErrorsChanged([CallerMemberName] string propertyName = null)
        {
            OnErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }

        protected virtual void OnErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            ErrorsChanged?.Invoke(sender, e);
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            OnPropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(sender, e);
        }
    }
}