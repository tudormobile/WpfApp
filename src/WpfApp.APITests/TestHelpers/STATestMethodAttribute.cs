using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppAPITests
{
    [ExcludeFromCodeCoverage]
    internal class STATestMethodAttribute : TestMethodAttribute
    {
        public override TestResult[] Execute(ITestMethod testMethod)
        {
            if (Thread.CurrentThread.GetApartmentState() == ApartmentState.STA)
            {
                return invoke(testMethod);
            }

            TestResult[] result = [];
            var thread = new Thread(() => result = invoke(testMethod));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
            return result;
        }

        private static TestResult[] invoke(ITestMethod testMethod)
        {
            return [testMethod.Invoke(null)];
        }
    }
}
