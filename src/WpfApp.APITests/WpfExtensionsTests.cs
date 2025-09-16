using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using Tudormobile.Wpf;

namespace WpfAppAPITests;

[STATestClass]
public class WpfExtensionsTests
{
    [TestMethod]
    public void GetParentTest()
    {
        var target = new Rectangle();
        var expected = new Border();
        expected.Child = target;
        var actual = target.GetParent();
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void GetParentIsNullTest()
    {
        var target = new Rectangle();
        var actual = target.GetParent();
        Assert.IsNull(actual);
    }

    [TestMethod]
    public void GetParentByTypeTest()
    {
        var target = new Rectangle();
        var expected = new Border();
        expected.Child = target;
        var actual = target.GetParent<Border>();
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void GetParentByTypeIsNullTest()
    {
        var target = new Rectangle();
        var expected = new Border();
        expected.Child = target;
        var actual = target.GetParent<Grid>();
        Assert.IsNull(actual);
    }

    [TestMethod]
    public void GetParentOrSelfByTypeTest()
    {
        var target = new Rectangle();
        var expected = new Border();
        expected.Child = target;
        var actual = target.GetParentOrSelf<Border>();
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void GetParentOrSelfByTypeIsNullTest()
    {
        var target = new Rectangle();
        var expected = new Border();
        expected.Child = target;
        var actual = target.GetParentOrSelf<Grid>();
        Assert.IsNull(actual);
    }

    [TestMethod]
    public void GetParentOrSelfByTypeIsSelfTest()
    {
        var target = new Rectangle();
        var expected = new Border();
        expected.Child = target;
        var actual = target.GetParentOrSelf<Rectangle>();
        Assert.AreEqual(target, actual);
    }

    [TestMethod]
    public void GetParentsTest()
    {
        var target = new Rectangle();
        var root = new Border();
        var grid = new Grid();
        root.Child = grid;
        grid.Children.Add(target);
        List<DependencyObject> expected = [grid, root];
        var actual = target.GetParents();
        CollectionAssert.AreEqual(expected, actual.ToList());
    }

    [TestMethod]
    public void GetChildrenTest()
    {
        var target = new Border();
        var rect = new Rectangle();
        var grid = new Grid();
        target.Child = grid;
        grid.Children.Add(rect);
        List<DependencyObject> expected = [grid, rect];
        var actual = target.GetChildren();
        CollectionAssert.AreEqual(expected, actual.ToList());
    }

    [TestMethod]
    public void GetChildrenOrSelfTest()
    {
        var target = new Border();
        var rect = new Rectangle();
        var grid = new Grid();
        var childBorder1 = new Border();
        var childBorder2 = new Border();
        grid.Children.Add(childBorder1);
        grid.Children.Add(childBorder2);
        childBorder1.Child = rect;
        target.Child = grid;

        List<DependencyObject> expected = [target, childBorder1, childBorder2];

        var actual = target.GetChildrenOrSelf<Border>();
        CollectionAssert.AreEqual(expected, actual.ToList());
    }

    [TestMethod]
    public void GetChildrenOfTypeTest()
    {
        var target = new Border();
        var rect = new Rectangle();
        var grid = new Grid();
        var childBorder1 = new Border();
        var childBorder2 = new Border();
        grid.Children.Add(childBorder1);
        grid.Children.Add(childBorder2);
        childBorder1.Child = rect;
        target.Child = grid;

        List<DependencyObject> expected = [childBorder1, childBorder2];

        var actual = target.GetChildren<Border>();
        CollectionAssert.AreEqual(expected, actual.ToList());
    }

    [TestMethod]
    public void GetChildTest()
    {
        var target = new Border();
        var expected = new Rectangle();
        target.Child = expected;

        var actual = target.GetChild<Rectangle>();
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void GetChildOrSelfTest()
    {
        var target = new Border();
        var expected = new Rectangle();
        target.Child = expected;

        var actual = target.GetChildOrSelf<Border>();
        Assert.AreEqual(target, actual);
    }

    [TestMethod]
    public void GetChildOrSelfWithNullTest()
    {
        var target = new Border();
        var rect = new Rectangle();
        var grid = new Grid();
        var childBorder1 = new Border();
        var childBorder2 = new Border();
        grid.Children.Add(childBorder1);
        grid.Children.Add(childBorder2);
        childBorder1.Child = rect;
        target.Child = grid;

        Assert.IsNull(target.GetChildOrSelf<ProgressBar>());
    }




}
