using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace X07ListView
{
    public class Fruit : INotifyPropertyChanged
    {
        private string fName = "";
        private string fDesc = "";

        public string Name
        {
            get { return fName; }
            set
            {
                if (fName != value)
                {
                    this.fName = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Desc
        {
            get { return fDesc; }
            set 
            {
                if (fDesc !=  value) 
                { 
                    this.fDesc = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;   // btn.Clicked += () => {}

        private void OnPropertyChanged([CallerMemberName] string property = "")
        {
            // check if there are "clients" / "listeners"
            if (PropertyChanged != null)    // does something care about this property changing?
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public override string ToString()
        {
            return Name + " " + Desc;
        }
    }
}
