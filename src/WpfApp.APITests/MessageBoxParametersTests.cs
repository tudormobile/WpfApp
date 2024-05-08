using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tudormobile.Wpf.Commands;

namespace WpfAppAPITests
{
    [TestClass]
    public class MessageBoxParametersTests
    {
        [TestMethod]
        public void TextTest()
        {
            var target = new MessageBoxParameters();

            PropertyTester.TestDependencyProperty(target,
                MessageBoxParameters.TextProperty,
                nameof(target.Text),
                "expected", null,
                v => target.Text = v,
                () => target.Text);
        }

        [TestMethod]
        public void CaptionTest()
        {
            var target = new MessageBoxParameters();

            PropertyTester.TestDependencyProperty(target,
                MessageBoxParameters.CaptionProperty,
                nameof(target.Caption),
                "expected", null,
                v => target.Caption = v,
                () => target.Caption);
        }

        [TestMethod]
        public void IconTest()
        {
            var target = new MessageBoxParameters();

            PropertyTester.TestDependencyProperty(target,
                MessageBoxParameters.IconProperty,
                nameof(target.Icon),
                MessageBoxImage.Error, MessageBoxImage.None,
                v => target.Icon = v,
                () => target.Icon);
        }

        [TestMethod]
        public void ButtonTest()
        {
            var target = new MessageBoxParameters();

            PropertyTester.TestDependencyProperty(target,
                MessageBoxParameters.ButtonProperty,
                nameof(target.Button),
                MessageBoxButton.YesNoCancel, MessageBoxButton.OK,
                v => target.Button = v,
                () => target.Button);
        }

        [TestMethod]
        public void CommandTest()
        {
            var target = new MessageBoxParameters();

            PropertyTester.TestDependencyProperty(target,
                MessageBoxParameters.CommandProperty,
                nameof(target.Command),
                new FilePickerCommand(), null,
                v => target.Command = v,
                () => target.Command);
        }

        [TestMethod]
        public void ResultTest()
        {
            var target = new MessageBoxParameters();

            PropertyTester.TestDependencyProperty(target,
                MessageBoxParameters.ResultProperty,
                nameof(target.Result),
                MessageBoxResult.Cancel, MessageBoxResult.None,
                v => target.Result = v,
                () => target.Result);
        }
    }
}
