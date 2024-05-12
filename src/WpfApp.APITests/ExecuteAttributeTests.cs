using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tudormobile.Wpf.Commands;

namespace WpfAppAPITests;

[TestClass]
public class ExecuteAttributeTests
{
    [TestMethod]
    public void ConstructorTest()
    {
        var commandName = "CommandName";
        var className = "ClassName";
        var target = new ExecuteAttribute(className, commandName);
        Assert.IsInstanceOfType<Attribute>(target, "Must derive from System.Attribute.");
        Assert.AreEqual(commandName, target.CommandName);
        Assert.AreEqual(className, target.ClassName);
    }

    [TestMethod]
    public void PropertiesTest()
    {
        var commandName = "CommandName";
        var target = new ExecuteAttribute(commandName);
        Assert.AreEqual(commandName, target.CommandName);
        Assert.IsNull(target.ClassName, "ClassName property must be (null) when none is provided.");
    }
}

