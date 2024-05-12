using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tudormobile.Wpf.Commands;

namespace WpfAppAPITests
{
    [TestClass]
    public class FilePickerCommandTests
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var target = new SaveFilePickerCommand();
            Assert.IsInstanceOfType<ICommand>(target);
        }

        [TestMethod]
        public void CanExecuteTest()
        {
            var expected = true;
            var target = new OpenFilePickerCommand();
            var actual = target.CanExecute(null);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ExecuteTest()
        {
            var expected = "some filename";
            var cmd = new TestCommand();
            var service = new TestDialogService() { FileDialogResult = true, FileDialogFileName = expected };
            var target = new OpenFilePickerCommand(service);

            target.Execute(cmd);

            Assert.AreEqual(expected, cmd.ExecuteParameter);
            Assert.AreEqual(1, cmd.ExecuteCounter);

            // Should not delegate to command when file dialog is (false) or (null)
            service.FileDialogResult = false;
            target.Execute(cmd);
            service.FileDialogResult = null;
            target.Execute(cmd);
            Assert.AreEqual(1, cmd.ExecuteCounter, "Should not have passed through to command when FileDialog returns false or null");
        }

        [TestMethod]
        public void ExecuteWithNullTest()
        {
            var expected = "some filename";
            var cmd = new TestCommand();
            var service = new TestDialogService() { FileDialogResult = true, FileDialogFileName = expected };
            var target = new OpenFilePickerCommand(service);

            target.Execute(null);

            Assert.AreEqual(0, cmd.ExecuteCounter);
        }

        [TestMethod]
        public void ExecuteWithParametersTest()
        {
            var p = new FilePickerParameters();
            var expected = "some filename";
            var cmd = new TestCommand();
            var service = new TestDialogService() { FileDialogResult = true, FileDialogFileName = expected };
            var target = new OpenFilePickerCommand(service);

            target.Execute(p);

            Assert.AreEqual(0, cmd.ExecuteCounter);

            p.Command = cmd;
            target.Execute(p);
            Assert.AreEqual(expected, cmd.ExecuteParameter);
            Assert.AreEqual(1, cmd.ExecuteCounter);

        }



    }
}
