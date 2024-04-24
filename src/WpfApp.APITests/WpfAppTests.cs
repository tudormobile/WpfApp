using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tudormobile.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace WpfAppAPITests
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
        [TestMethod]
        public void CurrentTest()
        {
            var target = WpfApp.CreateBuilder().Build();
            var actual = WpfApp.Current;
            Assert.IsNotNull(actual, "No current application when running in unit tests.");
        }

        [STATestMethod]
        public void CreateWindowWithoutHostTest()
        {

            var target = WpfApp.CreateBuilder().Build();
            var actual = target.CreateWindow<TestWindow, TestModel>();
            Assert.IsInstanceOfType(actual, typeof(TestWindow), "Failed to create the Window object.");
            Assert.IsInstanceOfType<TestModel>(actual.DataContext, "Failed to set the data context for the window.");
        }

        [STATestMethod]
        public async Task CreateWindowWithHostTest()
        {
            var builder = WpfApp.CreateBuilder().AddHosting();
            builder.HostBuilder.ConfigureServices((context, services) =>
            {
                services.AddTransient<TestWindow>()
                        .AddTransient<TestModel>();
            });
            var target = builder.Build();
            await target.Start();
            var actual = target.CreateWindow<TestWindow, TestModel>();
            Assert.IsInstanceOfType(actual, typeof(TestWindow), "Failed to create the Window object.");
            Assert.IsInstanceOfType<TestModel>(actual.DataContext, "Failed to set the data context for the window.");
        }

    }
    class TestModel { }
    class TestWindow : Window { }
}