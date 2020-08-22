using Market.Commands;
using Market.Models;
using Market.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;

namespace Market.ViewModels
{
    public class ClientViewModel
    //  :  INotifyPropertyChanged
    {
        public List<ClientDTO> cn;
      
        ///<sammury>
        ///  Initializes a new instance of the  ClientViewModel class
        ///</sammury>
        public ClientViewModel()
        {
            _client = new ClientDTO("khaled love c#");
            UpdateCommand = new ClientUpdateCommand(this);
        }
        MainWindowViewModel main;
        public ClientViewModel(MainWindowViewModel d)
        {
            main = d;
            cn = new List<ClientDTO>();
            _client = new ClientDTO("khaled love c#");
            UpdateCommand = new ClientUpdateCommand(this);
           
        }

        ///<sammury>
        ///  get update Command for view model
        ///</sammury>
        public ICommand UpdateCommand {
            get;
            private set;
        }

        /// <summary>
        /// gets or sets a System.Boolean value indicating whether the Client can be updated.   
        /// </summary>
        public bool CanUpdate
        {
            get
            {
                if (Client == null)
                {
                    return false;
                }
                return !string.IsNullOrWhiteSpace(Client.NameClient);
            }

        }

        /// <summary>
        /// filds 
        /// </summary>
        string op = "khaled";
        //  public event PropertyChangedEventHandler PropertyChanged;

        #region Entity of Client

        private ClientDTO _client;
        public ClientDTO Client
        {
            get { return _client; }
            set { _client = value; }
        }

        #endregion

        public void func()
        {
           
            Console.WriteLine("func called");
        }
        public int  testDelegate(int i)
        {
            //  Console.WriteLine(i);
            return i;
        }
       
        public void saveChange()
        {
            //  Debug.Assert(false, string.Format("{0} was updated",Client.NameClient));
            op = op + "1";
            Client.NameClient = op;
        }




        // lambda expressions / delegate
        public delegate int Delg(int i);
        public delegate int Delg2(int i, int j);
        public void lambda_expressions()
        {
            //  public delegate void Delg();
            Delg d = new Delg(testDelegate);
            d(8);
            // called func

            // create simple list
            List<int> gh = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            // FindALL(delegate condition )
            List<int> evenNumber = gh.FindAll(delegate (int i) { return (i % 2 == 0); });
            foreach (int kh in evenNumber)
            {
                // Console.Write(kh + " ");
            }


            // use lambda experssion
            List<int> oddNumber = gh.FindAll(i => i % 2 == 1 && i != 5);
            //  oddNumber.ForEach(i => Console.Write(" " + i));
            Delg2 d1 = (j, i) => i * 2 + 1;
            Delg2 add = new Delg2((j, i) => i + j);
            Delg2 min = new Delg2((j, i) => i < j ? i : j);
            Console.Write(d1(2, 8));

        }

    }
}
