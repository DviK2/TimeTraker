using GalaSoft.MvvmLight.Command;
using System.Windows;
using System.Windows.Input;
using TimerTracker.DataAcess.Models;
using System.Runtime;

namespace TimeTracker.ViewModel
{
    class AddProjectViewModel
    {
        private ICommand _okCommand;

        public Project Project { get; set; }

        public ICommand OkCommand
        {
            get { return _okCommand ?? (_okCommand = new RelayCommand<Window>(OkAction)); }
        }

        public void OkAction(Window window)
        {
            window.DialogResult = true;
            window.Close();
        }
    }
}
