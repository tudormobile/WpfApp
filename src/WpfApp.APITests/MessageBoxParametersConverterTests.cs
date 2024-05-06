using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tudormobile.Wpf.Commands;
using Tudormobile.Wpf.Converters;

namespace WpfAppAPITests
{
    [TestClass]
    public class MessageBoxParametersConverterTests
    {
        [TestMethod]
        public void CanConvertFromTest()
        {
            var target = new MessageBoxParametersConverter();
            Assert.IsTrue(target.CanConvertFrom(typeof(String)), "Must be able to convert from a string value.");
        }

        [TestMethod]
        public void CanConvertToTest()
        {
            var target = new MessageBoxParametersConverter();
            Assert.IsTrue(target.CanConvertTo(typeof(MessageBoxParameters)), "Must be able to convert to MessageBoxParmaters.");
        }

        [TestMethod]
        public void ConvertFromTest()
        {
            var target = new MessageBoxParametersConverter();
            var testData = "Message|Caption|YesNoCancel|Stop|Yes";

            var actual = (MessageBoxParameters) target.ConvertFrom(testData)!;

            Assert.AreEqual("Message", actual.Text);
            Assert.AreEqual("Caption", actual.Caption);
            Assert.AreEqual(MessageBoxButton.YesNoCancel, actual.Button);
            Assert.AreEqual(MessageBoxImage.Stop, actual.Icon);
            Assert.AreEqual(MessageBoxResult.Yes, actual.Result);
        }

        [TestMethod]
        public void ConvertToTest()
        {
            var target = new MessageBoxParametersConverter();
            var testData = new MessageBoxParameters()
            {
                Text = "Message",
                Caption = "Caption",
                Button = MessageBoxButton.YesNoCancel,
                Icon = MessageBoxImage.Hand,
                Result = MessageBoxResult.Yes,
            };
            var expected = "Message|Caption|YesNoCancel|Hand|Yes";

            var actual = (String)target.ConvertTo(testData, expected.GetType())!;

            Assert.AreEqual(expected, actual);
        }
    }
}
