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
    public class WpfApplicationCommandsTests
    {
        [TestMethod]
        public void CommandsTests()
        {
            Assert.AreEqual(WpfApplicationCommands.Open.Name, WpfApplicationCommands.Open.Text);
            Assert.AreEqual(WpfApplicationCommands.Exit.Name, WpfApplicationCommands.Exit.Text);
            Assert.AreEqual(WpfApplicationCommands.SelectWindow.Name, WpfApplicationCommands.SelectWindow.Text);
            Assert.AreEqual(WpfApplicationCommands.CloseAll.Name, WpfApplicationCommands.CloseAll.Text);

        }
    }
}
