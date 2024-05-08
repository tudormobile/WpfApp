using System.Windows;

namespace WpfAppAPITests
{
    public static class PropertyTester
    {
        public static void TestDependencyProperty<T>(DependencyObject target, DependencyProperty prop, string name, T value, T defaultValue, Action<T> setter, Func<T> getter)
        {
            // Validate defaults
            Assert.AreEqual(name, prop.Name, "Check dependency property name.");
            Assert.AreEqual(defaultValue, target.GetValue(prop), "Default value not set correctly.");
            Assert.AreEqual(defaultValue, getter(), "Get method did not return dependency property value.");

            // Validate setter/getter
            setter(value);
            Assert.AreEqual(value, target.GetValue(prop));
            Assert.AreEqual(value, getter());
            target.SetValue(prop, defaultValue);
            Assert.AreEqual(defaultValue, getter());
        }
    }
}
