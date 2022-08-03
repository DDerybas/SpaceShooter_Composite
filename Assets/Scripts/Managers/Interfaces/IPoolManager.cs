namespace Managers
{
    /// <summary>
    /// A manager that handles all object pools.
    /// </summary>
    public interface IPoolManager : IManager
    {
        /// <summary>
        /// Gets a pool from the collection.
        /// </summary>
        /// <typeparam name="T">A pool that implements IPoolBase interface.</typeparam>
        /// <returns>A pool that implements IPoolBase interface.</returns>
        T GetPool<T>() where T : IPoolBase;

        /// <summary>
        /// Gets a pool of the specific object type from the collection.
        /// </summary>
        /// <typeparam name="T">A pool that implements IPoolBase interface.</typeparam>
        /// <param name="poolType">Specific object type.</param>
        T GetPool<T>(ObjectType objectType = null) where T : IPoolBase;

        /// <summary>
        /// Adds a new pool to the collection.
        /// </summary>
        /// <param name="pool">A pool that implements IPoolBase interface.</param>
        void AddPool(IPoolBase pool);

        /// <summary>
        /// Removes a pool from the collection.
        /// </summary>
        /// <param name="pool">A pool that implements IPoolBase interface.</param>
        void RemovePool(IPoolBase pool);
    }
}
