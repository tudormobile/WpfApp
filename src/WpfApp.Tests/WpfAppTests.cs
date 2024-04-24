using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tudormobile.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppTests
{
    [TestClass]
    public class WpfAppTests
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var target = new WpfApp();
            Assert.IsInstanceOfType<IWpfApp>(target);
            Assert.IsNotNull(target.Windows, "Windows colletion must be initialized to []");
        }
    }
}