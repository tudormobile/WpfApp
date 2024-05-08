using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Tudormobile.Wpf.Commands
{
    /// <summary>
    /// Defines a command the serves as a proxy to a subsequent command.
    /// </summary>
    public class ProxyCommand : ICommand
    {
        /// <inheritdoc/>
        public event EventHandler? CanExecuteChanged;

        /// <inheritdoc/>
        public bool CanExecute(object? parameter)
            => OnCanExecute(parameter);

        /// <inheritdoc/>
        public void Execute(object? parameter)
            => OnExecute(parameter);

        /// <summary>
        /// Raises the CanExecuteChanged event.
        /// </summary>
        /// <remarks>
        /// Derived objects may override this method to provide custom behavior.
        /// </remarks>
        protected virtual void OnCanExecuteChanged()
            => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        /// <summary>
        /// Raises the Execute event.
        /// </summary>
        /// <remarks>
        /// If the parameter is an ICommand instance, the execute action is delegated to that instance.
        /// Derived objects may override this method to provide custom behavior.
        /// </remarks>
        protected virtual void OnExecute(object? parameter)
            => (parameter as ICommand)?.Execute(null);

        /// <summary>
        /// Raises the CanExecute event.
        /// </summary>
        /// <remarks>
        /// If the parameter is an ICommand instance, the execute action is delegated to that instance.
        /// Derived objects may override this method to provide custom behavior.
        /// </remarks>
        protected virtual bool OnCanExecute(object? parameter)
            => (parameter as ICommand)?.CanExecute(null) ?? true;
    }
}
