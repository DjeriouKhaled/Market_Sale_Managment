using System.ComponentModel;

namespace Market.Models
{
    class Categorie_Model : INotifyPropertyChanged
    {
        private string nameCategorie;
        private int id;
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
