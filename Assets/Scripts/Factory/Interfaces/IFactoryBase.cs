using Managers;

/// <summary>
/// A base factory interface that inits the factory.
/// </summary>
public interface IFactoryBase
{
    /// <summary>
    /// Inits the factory with the global manager.
    /// </summary>
    void Init(IGlobalManager manager);
}
