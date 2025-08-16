namespace Tudormobile.Wpf.Services;

/// <inheritdoc/>
internal class NavigationService<T> : INavigationService<T>
{
    private readonly Stack<T> _backStack = [];
    private readonly Stack<T> _forwardStack = [];
    private T? _current = default;

    /// <inheritdoc/>
    public bool CanNavigateBack() => _backStack.Count > 0;

    /// <inheritdoc/>
    public bool CanNavigateForward() => _forwardStack.Count > 0;

    /// <inheritdoc/>
    public void Navigate(T value)
    {
        _forwardStack.Clear();
        if (!EqualityComparer<T>.Default.Equals(_current, default)) _backStack.Push(_current!);
        _current = value;
    }

    /// <inheritdoc/>
    public bool TryFind(Func<T, bool> predicate, out T result)
    {
        var item = _backStack.Concat(_forwardStack).FirstOrDefault(x => predicate(x));
        if (item != null)
        {
            result = item;
            return true;
        }
        result = default;
        return false;
    }

    /// <inheritdoc/>
    public bool TryFind(T value)
    {
        var comparer = EqualityComparer<T>.Default;
        return TryFind(x => comparer.Equals(x, value), out _);
    }

    /// <inheritdoc/>
    public bool TryNavigateBack(out T result)
    {
        if (_backStack.TryPop(out result))
        {
            _forwardStack.Push(_current!);
            _current = result;
            return true;
        }
        return false;
    }

    /// <inheritdoc/>
    public bool TryNavigateForward(out T result)
    {
        if (_forwardStack.TryPop(out result))
        {
            _backStack.Push(_current!);
            _current = result;
            return true;
        }
        return false;
    }

}