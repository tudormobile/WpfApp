using Tudormobile.Wpf.Services;

namespace WpfAppTests;

[TestClass]
public class NavigationServiceTests
{
    [TestMethod]
    public void DefaultCurrentTest()
    {
        string? expected = null;
        var target = new NavigationService<String>();
#pragma warning disable CS8604 // Possible null reference argument.
        Assert.IsFalse(target.TryFind(expected));
#pragma warning restore CS8604 // Possible null reference argument.
        target.Navigate("test");
        Assert.IsFalse(target.TryNavigateBack(out _));
        Assert.IsFalse(target.TryNavigateForward(out _));
        Assert.IsFalse(target.CanNavigateBack());
        Assert.IsFalse(target.CanNavigateForward());
    }

    [TestMethod]
    public void ForwardBackTest()
    {
        var target = new NavigationService<String>();
        target.Navigate("first");
        Assert.IsFalse(target.CanNavigateBack());
        Assert.IsFalse(target.CanNavigateForward());
        Assert.IsFalse(target.TryFind("first"));
        target.Navigate("second");
        Assert.IsTrue(target.CanNavigateBack());
        Assert.IsFalse(target.CanNavigateForward());
        Assert.IsTrue(target.TryFind("first"));
        Assert.IsFalse(target.TryFind("second"));

        // at this point we can go back to "first", and then forward to "second"
        Assert.IsTrue(target.TryNavigateBack(out var result));
        Assert.AreEqual("first", result);
        Assert.IsTrue(target.CanNavigateForward());
        Assert.IsTrue(target.TryNavigateForward(out result));
        Assert.AreEqual("second", result);

        // Finally, if we navigate to a new value, the forward stack is cleared
        target.Navigate("third");
        Assert.IsTrue(target.CanNavigateBack());
        Assert.IsFalse(target.CanNavigateForward());

    }

}
