using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Tudormobile.Wpf.Commands;

namespace WpfAppTests;

[TestClass]
public class DelegateCommandHelpersTests
{
    [TestMethod]
    public void CreateDelegateTest()
    {
        var actual = DelegateCommandHelpers.CreateDelegate(this, this.GetType().GetMethod(nameof(VoidMethod)));
        Assert.IsNotNull(actual);

        // and.. make sure can call with no errors
        actual.DynamicInvoke();

    }
    [TestMethod, ExcludeFromCodeCoverage]
    public void CreateAsyncDelegateTest()
    {
        // not supported (for now)
        Assert.ThrowsExactly<NotSupportedException>(() => 
            DelegateCommandHelpers.CreateDelegate(this, this.GetType().GetMethod(nameof(AsyncVoidMethod))));
    }

    [TestMethod]
    public void CreateDelegateWithParameterTest()
    {
        var actual = DelegateCommandHelpers.CreateDelegate(this, this.GetType().GetMethod(nameof(MethodWithParameter)));
        Assert.IsNotNull(actual);

        // and.. make sure can call with no errors
        actual.DynamicInvoke("must be string");
    }

    [TestMethod]
    public void CreateDelegateWithNotSupportedSignatureTest()
    {
        var actual = DelegateCommandHelpers.CreateDelegate(this, this.GetType().GetMethod(nameof(NotSupportedMethod)));
        Assert.IsNull(actual, "Must return (null) for unsupported method signatures.");
    }

    [TestMethod]
    public void CreateDelegateWithNullMethodTest()
    {
        var actual = DelegateCommandHelpers.CreateDelegate(this, null);
        Assert.IsNull(actual, "Must return (null) for null objects.");
    }

    [TestMethod]
    public void CreateDelegateCommandTest()
    {
        // Without parameter
        var actual = DelegateCommandHelpers.CreateDelegateCommand(this,
            this.GetType().GetMethod(nameof(VoidMethod)),
            this.GetType().GetMethod(nameof(BoolMethod)));

        Assert.IsNotNull(actual);
        // and.. make sure can call them with no errors
        actual.Execute(new object());
        actual.CanExecute(new object());
    }

    [TestMethod]
    public void CreateDelegateCommandWithParameterTest()
    {
        // Without parameter
        var actual = DelegateCommandHelpers.CreateDelegateCommand(this,
            this.GetType().GetMethod(nameof(VoidMethodWithParameter)),
            this.GetType().GetMethod(nameof(BoolMethodWithParameter)));

        Assert.IsNotNull(actual);

        // and.. make sure can call them with no errors
        actual.Execute("must be string");
        actual.CanExecute("must also be a string");
    }

    [TestMethod]
    public void CreateDelegateCommandWithoutExecuteTest()
    {
        // Without parameter
        var actual = DelegateCommandHelpers.CreateDelegateCommand(this,
            null,
            this.GetType().GetMethod(nameof(BoolMethodWithParameter)));

        Assert.IsNotNull(actual);

        // and...make sure can still invoke
        actual.Execute("must be string because CanExecute takes a string");
    }

    [TestMethod, ExcludeFromCodeCoverage]
    public void CreateDelegateCommandWithNotSupportedSignatureTest()
    {
        Assert.ThrowsExactly<NotSupportedException>(() => 
            DelegateCommandHelpers.CreateDelegateCommand(this,
                this.GetType().GetMethod(nameof(NotSupportedMethod)),
                this.GetType().GetMethod(nameof(BoolMethodWithParameter))));
    }

    [TestMethod, ExcludeFromCodeCoverage]
    public void CreateDelegateCommandWithNullMethodsTest()
    {
        Assert.ThrowsExactly<NullReferenceException>(() => 
            DelegateCommandHelpers.CreateDelegateCommand(this, null, null));
    }

    [ExcludeFromCodeCoverage]
    public bool MethodWithParameter(string parameter) => false;

    [ExcludeFromCodeCoverage]
    public void NotSupportedMethod(string parameter1, string parameter2) { }


    [ExcludeFromCodeCoverage]
    public bool BoolMethod() => false;
    [ExcludeFromCodeCoverage]
    public bool BoolMethodWithParameter(string parameter) => false;
    [ExcludeFromCodeCoverage]
    public void VoidMethod() { }
    [ExcludeFromCodeCoverage]
    public void VoidMethodWithParameter(string parameter) { }
    [ExcludeFromCodeCoverage]
    public Task AsyncVoidMethod() => throw new NotImplementedException();

}
