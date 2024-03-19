using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace X08ListAndTableEX
{
    public class ToDo : INotifyPropertyChanged
    {
        private string tdDescription;
        private DateTime tdDate;
        private bool tdCompleted;
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string property = "")
        {
            if (PropertyChanged != null) 
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public string Description
        {
            get { return this.tdDescription; }
            set
            {
                if (tdDescription != value)
                {
                    this.tdDescription = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime Date
        {
            get 
            {
                return this.tdDate; 
            }
            set
            {
                if (tdDate != value)
                {
                    this.tdDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool Completed
        {
            get { return this.tdCompleted; }
            set
            {
                if (tdCompleted != value)
                {
                    this.tdCompleted = value;
                    OnPropertyChanged();
                }
            }
        }



    }
}
