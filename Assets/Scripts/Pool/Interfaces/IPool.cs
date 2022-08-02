/// <summary>
/// A base interface of a pool with the specified type T.
/// </summary>
public interface IPool<T> : IPoolBase where T : class
{
    /// <summary>
    /// Returns a pool object.
    /// </summary>
    /// <returns>An object of type T.</returns>
    T GetPoolableObject();

    /// <summary>
    /// Extends the pool and returns the object.
    /// </summary>
    /// <param name="activateObj">Whether the object should be activated/enabled?</param>
    /// <returns>An object of type T.</returns>
    T ExtendPool(bool activateObj = false);

    /// <summary>
    /// Is pool ready.
    /// </summary>
    bool IsReady();
}
