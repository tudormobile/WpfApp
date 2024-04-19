﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tudormobile.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tudormobile.Wpf.Tests
{
    [TestClass]
    public class WpfAppTests
    {
        [TestMethod]
        public void CreateBuilderTest()
        {
            var actual = WpfApp.CreateBuilder();
            Assert.IsInstanceOfType(actual, typeof(IWpfAppBuilder));
            // what to validate ...
        }
    }
}