using System.ComponentModel;

namespace Market.Models
{
    public class ClientDTO : INotifyPropertyChanged
    {

        /// <summary>
        /// intialize constructeur  
        /// </summary>
        public ClientDTO() {}
        public ClientDTO(string name_client)
        {
            NameClient = name_client;
        }

        ///<sammury>
        ///  Entity of NameClient
        ///</sammury>
        private string name_client;
        public string NameClient
        {
            get { return name_client; }
            set { name_client = value; OnPropertyChanged("NameClient"); }
        }

        #region notify Property Changed

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string s)
        {
            PropertyChangedEventHandler ph = PropertyChanged;
            if (ph != null)
            {
                ph(this, new PropertyChangedEventArgs(s));
            }
        }
        #endregion


    }
}
