using System;
using System.ComponentModel;

namespace IKriv.Windows.Mvvm
{
    public class ViewModelBase : MarshalByRefObject, INotifyPropertyChanged
    {
        protected void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override object InitializeLifetimeService()
        {
            return null;
        }
    }
}
