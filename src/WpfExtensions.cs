using System.Windows;
using System.Windows.Media;

namespace Tudormobile.Wpf;

/// <summary>
/// Some useful extensions for WPF.
/// </summary>
public static class WpfExtensions
{
    /// <summary>
    /// Retrieves the parent of the specified dependency object in the visual tree.
    /// </summary>
    /// <param name="obj">The dependency object whose parent is to be retrieved. Cannot be <see langword="null"/>.</param>
    /// <returns>The parent of the specified dependency object, or <see langword="null"/> if the object has no parent.</returns>
    public static DependencyObject? GetParent(this DependencyObject obj)
         => VisualTreeHelper.GetParent(obj);

    /// <summary>
    /// Retrieves the first parent of the specified type from the visual tree of the given <see
    /// cref="DependencyObject"/>.
    /// </summary>
    /// <typeparam name="T">The type of the parent to search for. Must be a non-nullable <see cref="DependencyObject"/>.</typeparam>
    /// <param name="obj">The starting <see cref="DependencyObject"/> from which to search for a parent.</param>
    /// <returns>The first parent of type <typeparamref name="T"/> if found; otherwise, <see langword="null"/>.</returns>
    public static T? GetParent<T>(this DependencyObject obj)
        where T : notnull, DependencyObject
        => (T?)obj.GetParents().FirstOrDefault(x => x is T);

    /// <summary>
    /// Retrieves the first ancestor of the specified type, including the object itself, if it matches the type.
    /// </summary>
    /// <typeparam name="T">The type of the ancestor to search for. Must be a non-nullable type that derives from <see
    /// cref="DependencyObject"/>.</typeparam>
    /// <param name="obj">The starting <see cref="DependencyObject"/> from which to begin the search.</param>
    /// <returns>The first ancestor of type <typeparamref name="T"/>, including the object itself if it matches; otherwise, <see
    /// langword="null"/>.</returns>
    public static T? GetParentOrSelf<T>(this DependencyObject obj)
        where T : notnull, DependencyObject
    {
        if (obj is T) return (T?)obj;
        return (T?)obj.GetParents().FirstOrDefault(x => x is T);
    }

    /// <summary>
    /// Retrieves the first child of the specified type from the children of the given <see cref="DependencyObject"/>.
    /// </summary>
    /// <typeparam name="T">The type of the child to retrieve. Must be a non-nullable <see cref="DependencyObject"/>.</typeparam>
    /// <param name="obj">The parent <see cref="DependencyObject"/> whose children are searched.</param>
    /// <returns>The first child of type <typeparamref name="T"/> if found; otherwise, <see langword="null"/>.</returns>
    public static T? GetChild<T>(this DependencyObject obj)
        where T : notnull, DependencyObject
        => (T?)obj.GetChildren().First(x => x is T);

    /// <summary>
    /// Retrieves the first child of the specified type from the visual tree of the given <see
    /// cref="DependencyObject"/>,  or the object itself if it matches the specified type.
    /// </summary>
    /// <typeparam name="T">The type of <see cref="DependencyObject"/> to search for. Must be a non-nullable type.</typeparam>
    /// <param name="obj">The <see cref="DependencyObject"/> to search within. Cannot be <see langword="null"/>.</param>
    /// <returns>The first child of type <typeparamref name="T"/> found in the visual tree, or the object itself if it is of type
    /// <typeparamref name="T"/>.  Returns <see langword="null"/> if no matching object is found.</returns>
    public static T? GetChildOrSelf<T>(this DependencyObject obj)
        where T : notnull, DependencyObject
    {
        if (obj is T) return (T?)obj;
        return (T?)obj.GetChildren().FirstOrDefault(x => x is T);
    }

    /// <summary>
    /// Retrieves all child elements of the specified type from the given <see cref="DependencyObject"/>.
    /// </summary>
    /// <typeparam name="T">The type of child elements to retrieve. Must be a non-nullable type that derives from <see
    /// cref="DependencyObject"/>.</typeparam>
    /// <param name="obj">The <see cref="DependencyObject"/> whose child elements are to be retrieved.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> containing all child elements of type <typeparamref name="T"/>. If no matching
    /// elements are found, the collection will be empty.</returns>
    public static IEnumerable<T> GetChildren<T>(this DependencyObject obj)
        where T : notnull, DependencyObject
        => obj.GetChildren().Where(x => x is T).Cast<T>();

    /// <summary>
    /// Retrieves all child elements of the specified type, including the specified object itself if it matches the
    /// type.
    /// </summary>
    /// <typeparam name="T">The type of elements to retrieve. Must be a non-nullable <see cref="DependencyObject"/>.</typeparam>
    /// <param name="obj">The starting <see cref="DependencyObject"/> from which to retrieve child elements.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> containing all child elements of type <typeparamref name="T"/> within the visual
    /// tree, including the specified object itself if it is of type <typeparamref name="T"/>.</returns>
    public static IEnumerable<T> GetChildrenOrSelf<T>(this DependencyObject obj)
        where T : notnull, DependencyObject
    {
        if (obj is T) yield return (T)obj;
        foreach (var child in obj.GetChildren().Where(x => x is T).Cast<T>()) { yield return child; }
    }

    /// <summary>
    /// Retrieves all parent elements of the specified <see cref="DependencyObject"/> in the visual tree.
    /// </summary>
    /// <remarks>This method traverses the visual tree of the specified object, yielding each parent element
    /// in sequence.  If the specified object has no parent, the returned sequence will be empty.</remarks>
    /// <param name="obj">The <see cref="DependencyObject"/> for which to retrieve the parent elements.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="DependencyObject"/> instances representing the parent elements  of
    /// the specified object, starting with its immediate parent and continuing up the visual tree.</returns>
    public static IEnumerable<DependencyObject> GetParents(this DependencyObject obj)
    {
        var parent = VisualTreeHelper.GetParent(obj);
        if (parent != null)
        {
            yield return parent;
            foreach (var p in parent.GetParents()) { yield return p; }
        }
    }

    /// <summary>
    /// Retrieves all child elements of the specified <see cref="DependencyObject"/>, including nested descendants.
    /// </summary>
    /// <remarks>This method performs a recursive traversal of the visual tree, returning each child element
    /// in depth-first order.</remarks>
    /// <param name="obj">The <see cref="DependencyObject"/> whose child elements are to be retrieved.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> containing all child elements of the specified <see cref="DependencyObject"/>, 
    /// including direct children and their descendants.</returns>
    public static IEnumerable<DependencyObject> GetChildren(this DependencyObject obj)
    {
        int count = VisualTreeHelper.GetChildrenCount(obj);
        for (var i = 0; i < count; i++)
        {
            var child = VisualTreeHelper.GetChild(obj, i);
            yield return child;
            foreach (var c in child.GetChildren()) { yield return c; }
        }
    }

}
