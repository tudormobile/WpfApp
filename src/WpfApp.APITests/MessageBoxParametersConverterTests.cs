using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
            Assert.IsTrue(target.CanConvertTo(typeof(MessageBoxParameters)), "Must be able to convert to MessageBoxParameters.");
        }

        [TestMethod]
        public void ConvertFromTest()
        {
            var target = new MessageBoxParametersConverter();
            var testData = "Message|Caption|YesNoCancel|Stop|Yes";

            var actual = (MessageBoxParameters)target.ConvertFrom(testData)!;

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

        [TestMethod]
        public void ConvertToEmptyTest()
        {
            var target = new MessageBoxParametersConverter();
            var testData = new MessageBoxParameters()
            {
                Text = null,
                Caption = null,
                Button = MessageBoxButton.OK,
                Icon = MessageBoxImage.None,
                Result = MessageBoxResult.None,
            };
            var expected = "||OK|None|None";

            var actual = (String)target.ConvertTo(testData, expected.GetType())!;

            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void ConvertFromEmptyTest()
        {
            var target = new MessageBoxParametersConverter();
            var testData = "";
            var actual = (MessageBoxParameters)target.ConvertFrom(testData)!;

            Assert.AreEqual(string.Empty, actual.Text);
            Assert.IsNull(actual.Caption);
            Assert.AreEqual(MessageBoxImage.None, actual.Icon);
            Assert.AreEqual(MessageBoxButton.OK, actual.Button);
            Assert.AreEqual(MessageBoxResult.None, actual.Result);

        }

        [TestMethod]
        public void ConvertTest()
        {
            var target = new MessageBoxParametersConverter();
            var testData = "Message|Caption|YesNoCancel|Stop|Yes";

            var actual = (MessageBoxParameters)target.Convert(testData, typeof(MessageBoxParameters), null, null)!;

            Assert.AreEqual("Message", actual.Text);
            Assert.AreEqual("Caption", actual.Caption);
            Assert.AreEqual(MessageBoxButton.YesNoCancel, actual.Button);
            Assert.AreEqual(MessageBoxImage.Stop, actual.Icon);
            Assert.AreEqual(MessageBoxResult.Yes, actual.Result);

        }

        [TestMethod]
        public void ConvertBackTest()
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

            var actual = (String)target.ConvertBack(testData, expected.GetType(), null, null)!;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod, ExcludeFromCodeCoverage, ExpectedException(typeof(NotSupportedException))]
        public void ConvertToInvalidType()
        {
            var target = new MessageBoxParametersConverter();
            _ = target.ConvertTo(null, typeof(object));
        }
    }
}
