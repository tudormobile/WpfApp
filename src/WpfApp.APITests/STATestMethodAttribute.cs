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
                return Invoke(testMethod);
            }

            TestResult[] result = [];
            var thread = new Thread(() => result = Invoke(testMethod));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
            return result;
        }

        private static TestResult[] Invoke(ITestMethod testMethod)
        {
            return [testMethod.Invoke(null)];
        }
    }
}
