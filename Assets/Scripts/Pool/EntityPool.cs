using Entities;
using Managers;

/// <summary>
/// A pool of entities.
/// </summary>
public class EntityPool : MonoPool<Entity> 
{
    /// <summary>
    /// Inits a pool with the global manager.
    /// </summary>
    /// <param name="manager">A manager that implemets IGlobalManager interface.</param>
    public override void Init(IGlobalManager manager) => base.Init(manager);
}
