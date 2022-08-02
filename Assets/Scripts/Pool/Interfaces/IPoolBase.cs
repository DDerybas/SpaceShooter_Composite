using Managers;

/// <summary>
/// Base pool interface.
/// </summary>
public interface IPoolBase
{
    /// <summary>
    /// Inits a pool with the global manager.
    /// </summary>
    /// <param name="manager">A manager that implements the IGlobalManager interface.</param>
    void Init(IGlobalManager manager);

    /// <summary>
    /// Pool type.
    /// </summary>
    ObjectType PoolType { get; set; }
}
