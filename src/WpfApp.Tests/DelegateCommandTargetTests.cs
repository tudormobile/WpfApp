using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tudormobile.Wpf.Commands;

namespace WpfAppTests
{
    [TestClass]
    public class DelegateCommandTargetTests
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var className = "ClassName";
            var commandName = "CommandName";
            var targetObject = new object();
            var target = new DelegateCommandTarget(className, commandName, targetObject);

            Assert.AreEqual(className, target.ClassName);
            Assert.AreEqual(commandName, target.CommandName);
            Assert.AreEqual(targetObject, target.Target);
            Assert.IsTrue(target.Key.Contains(className));
            Assert.IsTrue(target.Key.Contains(commandName));

            Assert.IsNull(target.ExecuteMethod);
            Assert.IsNull(target.CanExecuteMethod);
        }

        [TestMethod]
        public void MethodPropertiesTest()
        {
            var expected = this.GetType().GetMethod(nameof(MethodPropertiesTest));
            var className = "ClassName";
            var commandName = "CommandName";
            var targetObject = new object();
            var target = new DelegateCommandTarget(className, commandName, targetObject);

            target.ExecuteMethod = expected;
            var actual = target.ExecuteMethod;
            Assert.AreEqual(expected, actual);
            Assert.AreNotEqual(expected, target.CanExecuteMethod);
            target.CanExecuteMethod = expected;
            Assert.AreEqual(expected, target.CanExecuteMethod);
        }

    }
}
