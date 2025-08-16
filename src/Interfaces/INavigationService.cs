namespace Tudormobile.Wpf;

/// <summary>
/// Navigation service supporing forward and backward navigation to a generic type.
/// </summary>
/// <typeparam name="T">Subject of the navigation; can be any type.</typeparam>
/// <remarks>
/// The navigation type can be a reference type or a value type but cannot be null or the default value of the type.
/// </remarks>
public interface INavigationService<T>
{
    /// <summary>
    /// Check if can currently navigate backwards.
    /// </summary>
    /// <returns>True if can navigate back, false otherwise.</returns>
    bool CanNavigateBack();

    /// <summary>
    /// Check if can currently navigate forwards.
    /// </summary>
    /// <returns>True if can navigate forward, false otherwise.</returns>
    bool CanNavigateForward();

    /// <summary>
    /// Navigate to a specific value.
    /// </summary>
    /// <param name="value">Value representing the current navigation state.</param>
    /// <remarks>
    /// Note that this method will clear the forward history.
    /// </remarks>
    void Navigate(T value);

    /// <summary>
    /// Navigate back to the previous value in the history, if any.
    /// </summary>
    /// <param name="result">Previous state value in history.</param>
    /// <returns>True if navigation was successful, otherwise false.</returns>
    bool TryNavigateBack(out T result);

    /// <summary>
    /// Navigate forward to the next value in the history, if any.
    /// </summary>
    /// <param name="result">Next value in history.</param>
    /// <returns>True if navigation was successful; otherwise false.</returns>
    bool TryNavigateForward(out T result);

    /// <summary>
    /// Attempt to find a specific state in the forward or back history. Does not consider the current state.
    /// </summary>
    /// <param name="predicate">Comparison function for locating state.</param>
    /// <param name="result">Resulting state, if found.</param>
    /// <returns>True if state was found; otherwise false.</returns>
    /// <remarks>
    /// Note that the comparer can be used to define custom equality logic for the type T, and does not
    /// need to strictly compare the T objects themselves. This allows for more flexible concept of "state"
    /// in which the state object can be a complex object yet the compare can focus on a specific 'key' or property.
    /// </remarks>
    bool TryFind(Func<T, bool> predicate, out T result);

    /// <summary>
    /// Attempt to find a specific state in the forward or back history.Does not consider the current state.
    /// </summary>
    /// <param name="value">State to search.</param>
    /// <returns>True if state was found; otherwise false.</returns>
    bool TryFind(T value);
}
