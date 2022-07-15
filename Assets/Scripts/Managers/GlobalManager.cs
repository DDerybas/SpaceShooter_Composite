using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Managers
{
    /// <summary>
    /// A global singleton manager containing all the managers in the game.
    /// </summary>
    public class GlobalManager : MonoBehaviour, IGlobalManager
    {
        #region Singleton
        /// <summary>
        /// Loads prefab with GlobalManager on it and then instantiates/initiates.
        /// </summary>
        [RuntimeInitializeOnLoadMethod]
        static void OnRuntimeMethodLoad()
        {
            IManager globalManager = Instantiate(Resources.Load<GameObject>("GlobalManager")).GetComponent<IManager>();
            globalManager.Init(null);
        }
        #endregion

        private List<IManager> managers = new List<IManager>();     // Holds all child managers.

        /// <summary>
        /// Initializes the GlobalManager.
        /// </summary>
        /// <param name="manager">A manager that implements the IManager interface.</param>
        public void Init(IManager manager)
        {
            DontDestroyOnLoad(gameObject);

            if (transform.childCount <= 0)
                return;

            managers.AddRange(GetComponentsInChildren<IManager>());
            foreach (IManager c in managers) c.Init(this);
            foreach (IManager c in managers) c.OnInitEnd();
        }

        public void OnInitEnd() { }

        /// <summary>
        /// Looks for a manager in the collection of managers and returns if found.
        /// </summary>
        /// <typeparam name="T">Manager of type IManager</typeparam>
        /// <returns>Manager of type IManager</returns>
        public T GetManager<T>() where T : IManager { return managers.OfType<T>().First(); }

        /// <summary>
        /// Adds manager to the collection of managers.
        /// </summary>
        /// <param name="manager">Manager of type IManager</param>
        public void AddManager(IManager manager) { managers.Add(manager); }

        /// <summary>
        /// Removes the passed manager from the collection of managers, if the collection contains one.
        /// </summary>
        /// <param name="manager">Manager of type IManager</param>
        public void RemoveManager(IManager manager)
        {
            if (managers.Contains(manager))
                managers.Remove(manager);
        }
    }
}
