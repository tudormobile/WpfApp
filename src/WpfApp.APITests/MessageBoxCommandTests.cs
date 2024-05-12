using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tudormobile.Wpf.Commands;

namespace WpfAppAPITests
{
    [TestClass]
    public class MessageBoxCommandTests
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var target = new MessageBoxCommand();
            Assert.IsInstanceOfType<ICommand>(target);
            target.Execute(null);   // no exceptions
        }

        [TestMethod]
        public void ConstructorWithActionTest()
        {
            MessageBoxResult expected = MessageBoxResult.Yes;
            MessageBoxResult actual = MessageBoxResult.None;
            var target = new MessageBoxCommand(result =>
            {
                actual = result;
            }, new TestDialogService() { Result = expected });
            target.Execute(new MessageBoxParameters());
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CanExecuteTest()
        {
            var expected = true;
            var target = new MessageBoxCommand();

            var actual = target.CanExecute(null);
            Assert.AreEqual(expected, actual, "Must always be able to execute.");
        }

        [TestMethod]
        public void ExecuteTest()
        {
            var expected = MessageBoxResult.Cancel;
            var cmd = new TestCommand();
            var target = new MessageBoxCommand(dialogService: new TestDialogService() { Result = expected });
            target.Execute(cmd);

            Assert.AreEqual(expected, cmd.ExecuteParameter);
            Assert.AreEqual(1, cmd.ExecuteCounter);
        }

        [TestMethod]
        public void ExecuteWithParametersTest()
        {
            var expected = MessageBoxResult.Cancel;
            var cmd = new TestCommand();
            var target = new MessageBoxCommand(dialogService: new TestDialogService() { Result = expected });
            var p = new MessageBoxParameters()
            {
                Command = cmd,
                Result = MessageBoxResult.Yes,
                Icon = MessageBoxImage.Question,
            };
            target.Execute(p);

            Assert.AreEqual(expected, cmd.ExecuteParameter);
            Assert.AreEqual(1, cmd.ExecuteCounter);
        }

        [TestMethod]
        public void ExecuteWithStringTest()
        {
            var expected = MessageBoxResult.Cancel;
            var cmd = new TestCommand();
            var target = new MessageBoxCommand(dialogService: new TestDialogService() { Result = expected });
            var s = "Text|Caption|OK|None";
            target.Execute(s);
            // how to validate? TODO: Through the test dialog service
        }
    }

}
