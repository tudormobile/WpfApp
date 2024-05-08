using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tudormobile.Wpf.Commands;

namespace WpfAppAPITests
{
    [TestClass]
    public class FilePickerParametersTests
    {
        [TestMethod]
        public void TitleTest()
        {
            var target = new FilePickerParameters();

            PropertyTester.TestDependencyProperty(target,
                FilePickerParameters.TitleProperty,
                nameof(target.Title),
                "expected", String.Empty,
                v => target.Title = v,
                () => target.Title);
        }

        [TestMethod]
        public void FilterTest()
        {
            var target = new FilePickerParameters();

            PropertyTester.TestDependencyProperty(target,
                FilePickerParameters.FilterProperty,
                nameof(target.Filter),
                "*.*", String.Empty,
                v => target.Filter = v,
                () => target.Filter);
        }

        [TestMethod]
        public void CommandTest()
        {
            var target = new FilePickerParameters();

            PropertyTester.TestDependencyProperty(target,
                FilePickerParameters.CommandProperty,
                nameof(target.Command),
                new FilePickerCommand(), null,
                v => target.Command = v,
                () => target.Command);
        }
        [TestMethod]
        public void FileNameTest()
        {
            var target = new FilePickerParameters();

            PropertyTester.TestDependencyProperty(target,
                FilePickerParameters.FileNameProperty,
                nameof(target.FileName),
                "expected", String.Empty,
                v => target.FileName = v,
                () => target.FileName);
        }

    }
}
