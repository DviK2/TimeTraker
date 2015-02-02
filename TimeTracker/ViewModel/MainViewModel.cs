using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Windows.Input;
using TimerTracker.DataAcess;
using TimerTracker.DataAcess.Models;
using TimeTracker.Timer;
using TimeTracker.View;
using System.Runtime;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using System;

namespace TimeTracker.ViewModel
{
    public sealed class MainViewModel
    {
        #region Fileds

        private MousePositionTimer _mousePositionTimer;
        private DbRepositoriy _dbRepositoriy;
        private ICommand _addProjectCommand;
        private DateTime _startDateTime;

        #endregion

        #region Construcotr

        public MainViewModel()
        {
            InitTimer();
            _dbRepositoriy = new DbRepositoriy();
            Projects = new ObservableCollection<Project>(_dbRepositoriy.GetProjects());
            SystemEvents.SessionEnding += SystemEvents_SessionEnding;
        }
        #endregion


        #region Properties
        public ICollection<Project> Projects { get; set; }

        public ICommand AddProjectCommand
        {
            get { return _addProjectCommand ?? (_addProjectCommand = new RelayCommand(AddProjectAction)); }
        }
        
        #endregion

        #region Methods
        public void InitTimer()
        {
            _mousePositionTimer = new MousePositionTimer(5);
            _mousePositionTimer.StartTimer += MousePositionTimer_StartTimer;
            _mousePositionTimer.StopTimer += MousePositionTimer_StopTimer;
            _mousePositionTimer.Start();
        }
        private void SystemEvents_SessionEnding(object sender, SessionEndingEventArgs e)
        {
            if (e.Reason == SessionEndReasons.Logoff)
            { }
            else if (e.Reason == SessionEndReasons.SystemShutdown)
            {
                InsertWorkHouers(DateTime.Now);
            }
            //if (Environment.HasShutdownStarted)
            //{
            //    //Tackle Shutdown
            //}
            //else
            //{
            //    //Tackle log off
            //}
        }
        private void MousePositionTimer_StopTimer(DateTime dateTime)
        {
            InsertWorkHouers(dateTime);
        }

        private void InsertWorkHouers(DateTime dateTime)
        {
            _dbRepositoriy.InsertWorkHouers(new WorkHouers()
            {
                ProjectID = 1,
                Date = DateTime.Now,
                Housers = dateTime.Hour - _startDateTime.Hour,
                Minutes = dateTime.Minute - _startDateTime.Minute
            });
        }

        private void MousePositionTimer_StartTimer(DateTime dateTime)
        {
            _startDateTime = dateTime;
        }

        public void AddProjectAction()
        {
            var window = new AddProjectView();
            var viewModel = new AddProjectViewModel { Project = new Project() };
            window.DataContext = viewModel;
            if (window.ShowDialog() != true)
                return;

            Projects.Add(viewModel.Project);
            _dbRepositoriy.InserPoject(viewModel.Project);
        }
        #endregion

    }
}
