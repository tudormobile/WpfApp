using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tudormobile.Wpf.Commands;

namespace WpfAppTests
{
    [TestClass]
    public class DelegateCommandTests
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var target = new DelegateCommand(nullMethod);
            Assert.IsInstanceOfType<ICommand>(target, "Must implement <ICommand> interface.");
            Assert.IsTrue(target.CanExecute(null), "Must default to (true).");
        }

        [TestMethod]
        public void GenericConstructorTest()
        {
            var target = new DelegateCommand<string>(doNothing);
            Assert.IsInstanceOfType<ICommand>(target, "Must implement <ICommand> interface.");
            Assert.IsTrue(target.CanExecute(null), "Must default to (true).");
            Assert.IsTrue(target.CanExecute(null), "Must default to (true).");
        }

        [TestMethod]
        public void CanExecuteTest()
        {
            var expected = false;
            bool? actual = null;
            var target = new DelegateCommand(nullMethod, new Func<bool>(() => expected));
            actual = target.CanExecute(null);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ExecuteTest()
        {
            object? actual = null;
            object expected = new object();
            var target = new DelegateCommand(new Action(() => { actual = expected; }));
            target.Execute(null);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod, ExcludeFromCodeCoverage, ExpectedException(typeof(TargetParameterCountException))]
        public void ExecuteWithWrongTypeTest()
        {
            object expected = new object();
            var target = new DelegateCommand(nullActionObject);
            target.Execute(null);   // throws
        }

        [TestMethod]
        public void GenericExecuteTest()
        {
            object? actual = null;
            object expected = new object();
            var target = new DelegateCommand<object>(new Action<object>(o => { actual = o; }));
            target.Execute(expected);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GenericCanExecuteTest()
        {
            var target = new DelegateCommand<string>(doNothing, new Func<string, bool>(o=> o is not string));
            var actual = target.CanExecute("this is a test");
            Assert.IsFalse(actual);
        }


        [TestMethod, ExcludeFromCodeCoverage, ExpectedException(typeof(InvalidCastException))]
        public void GenericExecuteWithWrongTypeTest()
        {
            object expected = new object();
            var target = new DelegateCommand<string>(doNothing);
            target.Execute(expected);   // throws
        }

        [TestMethod]
        public void CanExecuteChangedTest()
        {
            int counter = 0;
            var target = new TestDelegateCommand(doNothing);
            target.RaiseCanExecuteChanged(); // no errors w/no handler.
            target.CanExecuteChanged += (s, e) =>
            {
                counter++;
            };
            target.RaiseCanExecuteChanged();
            Assert.AreEqual(1, counter);
        }

        [ExcludeFromCodeCoverage]
        private void doNothing(string parameters) { }
        [ExcludeFromCodeCoverage]
        private void nullMethod() { }
        [ExcludeFromCodeCoverage]
        private void nullActionObject(object o) { }

        internal class TestDelegateCommand(Delegate d) : DelegateCommand(d)
        {
            public void RaiseCanExecuteChanged() => base.OnCanExecuteChanged();
        }
    }
}
