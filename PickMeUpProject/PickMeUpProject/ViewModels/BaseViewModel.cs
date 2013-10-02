using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace PickMeUpProject.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        protected void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        internal void SetObservableValues<T>(
            ObservableCollection<T> observables, 
            IEnumerable<T> values)
        {
            if (observables != values)
            {
                observables.Clear();
                foreach (var value in values)
                {
                    observables.Add(value);
                }
            }
        }
    }
}