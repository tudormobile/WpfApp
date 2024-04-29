using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace WpfApp3
{
    internal class ClockData : INotifyPropertyChanged
    {
        DispatcherTimer _timer;
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        public TimeSpan Time { get; private set; }

        public ClockData(Dispatcher dispatcher)
        {
            _timer = new DispatcherTimer(TimeSpan.FromSeconds(1), DispatcherPriority.ApplicationIdle, updateTime, dispatcher);
            _timer.Start();
        }

        private void updateTime(object? sender, EventArgs e)
        {
            Time = Time.Add(TimeSpan.FromSeconds(1));
            OnPropertyChanged(nameof(Time));
        }
    }
}
