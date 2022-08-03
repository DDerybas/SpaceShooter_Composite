using System.Linq;
using UnityEngine;
using System.Collections.Generic;

namespace Managers
{
    /// <summary>
    /// A manager that handles all object pools.
    /// </summary>
    public class PoolManager : MonoBehaviour, IPoolManager
    {
        private IGlobalManager manager;                     // Cached global manager.
        private List<IPoolBase> poolCollection;             // The collection of object pools.
        [SerializeField] private Transform container;       // The container of MonoBehaviour pools.

        /// <summary>
        /// Initializes the manager. 
        /// </summary>
        /// <param name="manager">A manager that implements the IGlobalManager interface.</param>
        public void Init(IGlobalManager manager)
        {
            this.manager = manager;
            poolCollection = new List<IPoolBase>();

            // Gets all child MonoBehaviour pools.
            if (container.childCount > 0)
                poolCollection.AddRange(container.GetComponentsInChildren<IPoolBase>());

            // Subscribes for init end event of global manager.
            this.manager.OnInitEndAction += OnInitEndAction;
        }

        /// <summary>
        /// On global manager init end event.
        /// </summary>
        void OnInitEndAction()
        {
            // Inits all the pools.
            foreach (var pool in poolCollection)
                pool.Init(this.manager);
        }

        /// <summary>
        /// Adds a new pool to the collection.
        /// </summary>
        /// <param name="pool">A pool that implements IPoolBase interface.</param>
        public void AddPool(IPoolBase pool) => poolCollection.Add(pool);

        /// <summary>
        /// Gets a pool from the collection.
        /// </summary>
        /// <typeparam name="T">A pool that implements IPoolBase interface.</typeparam>
        /// <returns>A pool that implements IPoolBase interface.</returns>
        public T GetPool<T>() where T : IPoolBase => poolCollection.OfType<T>().First();

        /// <summary>
        /// Gets a pool of the specific object type from the collection.
        /// </summary>
        /// <typeparam name="T">A pool that implements IPoolBase interface.</typeparam>
        /// <param name="poolType">Specific object type.</param>
        public T GetPool<T>(ObjectType poolType) where T : IPoolBase
        {
            foreach (var pool in poolCollection.OfType<T>())
                if (pool.PoolType.name == poolType.name)
                    return pool;

#if UNITY_EDITOR || DEVELOPMENT_BUILD
            Debug.LogError($"Pool of type '{typeof(T)}' not found!");
#endif
            return default;
        }

        /// <summary>
        /// Removes a pool from the collection.
        /// </summary>
        /// <param name="pool">A pool that implements IPoolBase interface.</param>
        public void RemovePool(IPoolBase pool)
        {
            if (poolCollection.Contains(pool))
                poolCollection.Remove(pool);
        }
    }
}
