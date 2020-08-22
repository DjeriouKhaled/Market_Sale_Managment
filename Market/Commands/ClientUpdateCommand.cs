namespace Market.Commands
{
    using ViewModels;
    using System;
    using System.Windows.Input;
    internal class ClientUpdateCommand : ICommand
    {
        private ClientViewModel _ViewModel;
        public ClientUpdateCommand (ClientViewModel op)
        {
            _ViewModel = op;
        }

        #region ICommand Members
        public event EventHandler CanExecuteChanged {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _ViewModel.CanUpdate;
        }

        public void Execute(object parameter)
        {
            _ViewModel.saveChange();
        }

        #endregion
    }
}
