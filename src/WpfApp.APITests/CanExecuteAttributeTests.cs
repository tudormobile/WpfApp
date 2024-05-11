using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tudormobile.Wpf.Commands;

namespace WpfAppAPITests;

[TestClass]
public class CanExecuteAttributeTests
{
    [TestMethod]
    public void ConstructorTest()
    {
        var commandName = "CommandName";
        var className = "ClassName";
        var target = new CanExecuteAttribute(className, commandName);
        Assert.IsInstanceOfType<Attribute>(target, "Must derive from System.Attribute.");
        Assert.AreEqual(commandName, target.CommandName);
        Assert.AreEqual(className, target.ClassName);
    }

    [TestMethod]
    public void PropertiesTest()
    {
        var commandName = "CommandName";
        var target = new CanExecuteAttribute(commandName);
        Assert.AreEqual(commandName, target.CommandName);
        Assert.IsNull(target.ClassName, "ClassName property must be (null) when none is provided.");
    }
}
