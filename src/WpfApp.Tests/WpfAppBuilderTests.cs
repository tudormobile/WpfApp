using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tudormobile.Wpf;

namespace WpfApp.Tests
{
    [TestClass]
    public class WpfAppBuilderTests
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var target = new WpfAppBuilder();
            Assert.IsInstanceOfType<IWpfAppBuilder>(target);
            // what to validate...
        }

        [TestMethod]
        public void BuildTest()
        {
            var target = new WpfAppBuilder().Build();
            Assert.IsInstanceOfType<IWpfApp>(target);
            // what to validate...
        }
    }
}
