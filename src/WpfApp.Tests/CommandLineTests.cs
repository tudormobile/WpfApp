using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tudormobile.Wpf;

namespace WpfAppTests;
[TestClass]
public class CommandLineTests
{
    [TestMethod]
    public void CommandLineTest()
    {
        var target = new CommandLine();
        Assert.IsTrue(target.Arguments.Any(), "Should have determined command line arguments.");
        Assert.IsTrue(target.ProgramName.Length > 0, "Should have determined the current process name.");
    }

    [TestMethod]
    public void CommandLineTestWithProgramName()
    {
        var expected = "program_name";
        var target = new CommandLine(expected);
        var actual = target.ProgramName;
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void CommandLineTestWithDefaults()
    {
        var expected = Environment.GetCommandLineArgs();
        var target = new CommandLine(programName: null, args: null);
        var actual = target.ProgramName;
        var actualArgs = target.Arguments;
        CollectionAssert.AreEqual(expected, actualArgs, "Must obtain arguments from environment when not provided.");
    }

    [TestMethod]
    public void CommandLineTestWithArguments()
    {
        var expected = "test.txt";
        var commandLine = $"--verbose -o {expected} -x -z";
        var target = new CommandLine(commandLine.Split(' '));

        Assert.IsTrue(target["-o"].Exists);
        Assert.AreEqual(expected, target["-o"].Value);

        Assert.IsTrue(target["--verbose"].Exists);
        Assert.IsTrue(target["-x"].Exists);
        Assert.IsTrue(target["-z"].Exists);

        Assert.IsNull(target["--verbose"].Value);
        Assert.IsNull(target["-x"].Value);
        Assert.IsNull(target["-z"].Value);

        // this argument is not present.
        Assert.IsFalse(target["-a"].Exists);
        Assert.IsNull(target["-a"].Value);

    }
}
