using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace X08ListAndTableEX
{
    public class ToDo : INotifyPropertyChanged
    {
        private string tdTitle;
        private DateTime tdDate;
        private bool tdCompleted;

        public string Title
        {
            get { return this.tdTitle; }
            set
            {
                if (tdTitle != value)
                {
                    this.tdTitle = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime Date
        {
            get { return this.tdDate; }
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

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string property = "")
        {
            if (PropertyChanged != null) 
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

    }
}
