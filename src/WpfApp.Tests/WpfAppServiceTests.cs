using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tudormobile.Wpf.Services;

namespace WpfAppTests
{
    [TestClass]
    public class WpfAppServiceTests
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var target = new TestAppService();
            Assert.AreEqual(nameof(TestAppService), target.Name, "All services must use class name by default.");
            Assert.IsTrue(target.IsSingleton, "All services must indicate they are singletons by default.");
        }

        [ExcludeFromCodeCoverage]
        internal class TestAppService : WpfAppServiceBase { }
    }
}
