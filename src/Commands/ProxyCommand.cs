using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Tudormobile.Wpf.Commands
{
    public class ProxyCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
            => OnCanExecute(parameter);

        public void Execute(object? parameter)
            => OnExecute(parameter);

        protected virtual void OnCanExecuteChanged()
            => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        protected virtual void OnExecute(object? parameter)
            => (parameter as ICommand)?.Execute(null);

        protected virtual bool OnCanExecute(object? parameter)
            => (parameter as ICommand)?.CanExecute(null) ?? true;
    }
}
