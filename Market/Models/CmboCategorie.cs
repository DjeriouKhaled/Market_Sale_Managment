using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Models
{
    public class CmboCategorie : INotifyPropertyChanged
    {
        private int id;
        private string nameCategorie;


        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged("Id"); }
        }

        public string NameCategorie
        {
            get { return nameCategorie; }
            set { nameCategorie = value; OnPropertyChanged("NameCategorie"); }
        }

        private void OnPropertyChanged(string s)
        {
            PropertyChangedEventHandler ph = PropertyChanged;
            ph?.Invoke(this, new PropertyChangedEventArgs(s));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
