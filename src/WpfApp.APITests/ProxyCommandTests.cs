using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tudormobile.Wpf.Commands;

namespace WpfAppAPITests
{
    [TestClass]
    public class ProxyCommandTests
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var target = new ProxyCommand();
            Assert.IsInstanceOfType<ICommand>(target, "Must implement ICommand.");
            Assert.IsTrue(target.CanExecute(null), "Must assume can execute by default.");
            Assert.IsTrue(target.CanExecute(new object()), "Must assume can execute by default.");

            target.Execute(new object());   // no exceptions
            target.Execute(new object());   // no exceptions
        }

        [TestMethod]
        public void ProxyTest()
        {
            var target = new ProxyCommand();
            var command = new TestCommand();

            target.Execute(command);
            target.CanExecute(command);

            Assert.IsNull(command.CanExecutParameter);
            Assert.IsNull(command.ExecuteParameter);
        }

        [TestMethod]
        public void CanExecuteChangedTest()
        {
            var target = new TestProxyCommand();
            target.CallOnCanExecuteChanged();   // no exception w/no handler

            int counter = 0;
            target.CanExecuteChanged += (s, e) =>
            {
                counter++;
            };

            target.CallOnCanExecuteChanged();
            Assert.AreEqual(1, counter);
        }


        [ExcludeFromCodeCoverage]
        internal class TestCommand : ICommand
        {
            public object? CanExecutParameter { get; set; } = new object();
            public object? ExecuteParameter { get; set; } = new object();

            public event EventHandler? CanExecuteChanged;

            public bool CanExecute(object? parameter)
            {
                CanExecutParameter = parameter;
                return false;
            }

            public void Execute(object? parameter)
            {
                ExecuteParameter = parameter;
            }

            public virtual void OnCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        [ExcludeFromCodeCoverage]
        internal class TestProxyCommand : ProxyCommand
        {
            public void CallOnCanExecuteChanged() => base.OnCanExecuteChanged();
        }

    }
}
