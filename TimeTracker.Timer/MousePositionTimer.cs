using System;

namespace TimeTracker.Timer
{
    public sealed class MousePositionTimer
    {
        #region Fields

        private POINT _currentPosition;
        private int _minutes;
        private System.Timers.Timer _timer;
        private DateTime _startData;
        private bool _isChanges;
        private bool _isStopTimer;
        #endregion

        #region Events

        public event Action<DateTime> StopTimer;
        public event Action<DateTime> StartTimer;

        #endregion

        #region Constructor
        public MousePositionTimer(int minutesPeriod)
        {
            _minutes = minutesPeriod;
            InitTimer();
        }

        #endregion

        #region Methods

        private void InitTimer()
        {
            _timer = new System.Timers.Timer(5000); // 5 second
            _timer.AutoReset = true;
            _timer.Elapsed += _timer_Elapsed;
        }

        private void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            var tempCursorPos = MousePosition.GetCursorPosition();
            var tempDate = _startData.AddMinutes(_minutes);

            if (_isStopTimer && tempCursorPos != _currentPosition)
            {
                StartTimerAction(tempDate);
            }

            if (_currentPosition == tempCursorPos && e.SignalTime >= tempDate)
                _isChanges = false;
            else
            {
                _isChanges = true;
                _currentPosition = tempCursorPos;
            }

            if (!_isChanges && !_isStopTimer)
            {
                _startData = e.SignalTime;
                StopTimerAction(_startData);
            }
        }

        public void Start()
        {
            _startData = DateTime.Now;
            _currentPosition = MousePosition.GetCursorPosition();
            _timer.Start();
            StartTimerAction(_startData);
        }

        private void StartTimerAction(DateTime datetime)
        {
            _isStopTimer = false;
            if (StartTimer != null)
                StartTimer.Invoke(datetime);
        }

        private void StopTimerAction(DateTime datetime)
        {
            _isStopTimer = true;
            if (StopTimer != null)
                StopTimer.Invoke(datetime);
           
        }
        #endregion
    }
}
