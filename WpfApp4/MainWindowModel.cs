using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WpfApp4
{
    public class MainWindowModel : INotifyPropertyChanged
    {
        private FrameworkElement? _selectedUI;
        public string Message { get; set; } = "Hello from 'MainWindowModel'";
        public string Title { get; set; } = "WpfApp4: Main Window";
        public ObservableCollection<Type> Controls { get; } = [];
        public ICommand? SelectControlCommand { get; set; }
        public FrameworkElement? SelectedUI 
        {
            get => _selectedUI;
            set { _selectedUI = value; OnPropertyChanged(nameof(SelectedUI)); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
