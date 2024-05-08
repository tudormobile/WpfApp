using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;

namespace WpfAppAPITests
{
    [ExcludeFromCodeCoverage]
    public class TestCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        public object? ExecuteParameter { get; set; }
        public object? CanExecuteParameter { get; set; }
        public int ExecuteCounter { get; set; }
        public int CanExecuteCounter { get; set; }

        public bool CanExecute(object? parameter)
        {
            CanExecuteParameter = parameter;
            CanExecuteCounter++;
            return true;
        }

        public void Execute(object? parameter)
        {
            ExecuteParameter = parameter;
            ExecuteCounter++;
        }
        protected virtual void OnCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

}
