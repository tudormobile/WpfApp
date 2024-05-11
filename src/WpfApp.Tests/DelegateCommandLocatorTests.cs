using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tudormobile.Wpf.Commands;

namespace WpfAppTests;

[TestClass]
public class DelegateCommandLocatorTests
{
    [TestMethod]
    public void RegisterHandlerTest()
    {
        var info = this.GetType().GetMethod(nameof(RegisterHandlerTest))!;
        var target = new DelegateCommandLocator();
        target.RegisterHandler(nameof(DelegateCommandLocatorTests), "test", this, info, isCanExecute: false);

        // Make sure registered
        var actual = target.GetType().GetField("_handlers", BindingFlags.Instance | BindingFlags.NonPublic)!.GetValue(target) as IDictionary;
        Assert.AreEqual(1, actual!.Count);

        // Add a canExecute
        target.RegisterHandler(nameof(DelegateCommandLocatorTests), "test", this, info, isCanExecute: true);
        Assert.AreEqual(1, actual!.Count);

        // Finally, add with no class name (should be added separately)
        target.RegisterHandler("", "test", this, info, isCanExecute: true);
        Assert.AreEqual(2, actual!.Count);
        target.RegisterHandler("", "test", this, info, isCanExecute: false);
        Assert.AreEqual(2, actual!.Count);
    }

    [TestMethod]
    public void RegisterAndResolveHandlersTest()
    {
        var model = new TestModel();
        var target = new DelegateCommandLocator();
        target.RegisterHandlers(model); // Model is doing double-duty here as both the class hosting handlers
        target.ResolveHandlers(model);  // and the class with ICommand properties (command source and sink).

        Assert.IsNotNull(model.TestCommand, "Failed to resolve the TestCommand object.");
        Assert.IsNotNull(model.TestCommand2, "Failed to resolve the TestCommand2 object.");
        Assert.IsNull(model.TestReadOnlyCommand, "Should NOT have resolved the readonly ICommand property.");
        Assert.IsNull(model.MissingCommand, "Should NOT have resolved the 'missing' ICommand property.");

        // Validate operation
        var expected = "this is a test";
        model.TestCommand.Execute(expected);
        _  = model.TestCommand.CanExecute(expected);

        Assert.AreEqual(expected, model.CanExecuteParameter);
        Assert.AreEqual(expected, model.ExecuteParameter);
        Assert.AreEqual(1, model.CanExecuteCounter);
        Assert.AreEqual(1, model.ExecuteCounter);

        model.TestCommand2.Execute(expected);
        _ = model.TestCommand2.CanExecute(expected);
        Assert.AreEqual(2, model.CanExecuteCounter);
        Assert.AreEqual(2, model.ExecuteCounter);
        Assert.AreEqual(expected, model.CanExecuteParameter);
        Assert.AreEqual(expected, model.ExecuteParameter);

    }

    [ExcludeFromCodeCoverage]
    internal class TestModel
    {
        public ICommand? TestCommand { get; set; }
        public ICommand? TestCommand2 { get; set; }
        public ICommand? TestReadOnlyCommand { get; }
        public ICommand? MissingCommand { get; set; }

        public int CanExecuteCounter { get; set; }
        public string? CanExecuteParameter { get; private set; }
        public int ExecuteCounter { get; set; }
        public string? ExecuteParameter { get; private set; }

        [CanExecute(nameof(TestCommand))]
        public bool CanExecuteTestCommand(string parameter)
        {
            CanExecuteParameter = parameter;
            CanExecuteCounter++;
            return false;
        }
        [Execute(nameof(TestCommand))]
        public void ExecuteTestCommand(string parameter)
        {
            ExecuteParameter = parameter;
            ExecuteCounter++;
        }
        [Execute(nameof(TestModel), nameof(TestCommand2))]
        public void ExecuteTestCommand2()
        {
            ExecuteCounter++;
        }
        [CanExecute(nameof(TestModel), nameof(TestCommand2))]
        public bool CanExecuteTest2Command()
        {
            CanExecuteCounter++;
            return false;
        }


        [Execute(nameof(TestReadOnlyCommand))]
        public void ExecuteTestReadOnlyCommand(string parameter)
        {
            ExecuteParameter = parameter;
            ExecuteCounter++;
        }
    }
}
