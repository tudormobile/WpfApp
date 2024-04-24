using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Tudormobile.Wpf;

namespace WpfAppAPITests
{
    [TestClass]
    public class WpfAppBuilderTests
    {
        [STATestMethod]
        public async Task AddMainWindowTest()
        {
            var target = WpfApp.CreateBuilder().AddMainWindow<TestWindow>().Build();
            await target.Start();

            Assert.IsInstanceOfType(target.Windows.First(), typeof(TestWindow), "Failed to create the MainWindow object.");
        }
        [STATestMethod]
        public async Task AddWindowTest()
        {
            var target = WpfApp.CreateBuilder().AddWindow<TestWindow, TestModel>().Build();
            await target.Start();
            var actual = target.CreateWindow<TestWindow, TestModel>();

            Assert.IsInstanceOfType(actual.DataContext, typeof(TestModel));
        }
        [STATestMethod]
        public async Task AddViewTest()
        {
            var model = new TestModel();

            var target = WpfApp.CreateBuilder().AddView<TestWindow, TestModel>().Build();
            await target.Start();

            var actual = target.CreateWindow<TestWindow, TestModel>();

            Assert.IsInstanceOfType(actual.DataContext, typeof(TestModel));
        }
    }
}
