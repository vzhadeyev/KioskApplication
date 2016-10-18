using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KioskApplication.ViewModels
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        private string _pin = "";

        public string Pin
        {
            get
            {
                return _pin;
            }
            set
            {
                _pin = (value.Length > 4) ? value.Substring(0, 4) : value;
                OnPropertyChanged(new PropertyChangedEventArgs("Pin"));
            }
        }
        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
