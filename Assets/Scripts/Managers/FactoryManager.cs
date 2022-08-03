using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Managers
{
    /// <summary>
    /// Handles a collection of factories.
    /// </summary>
    public class FactoryManager : MonoBehaviour, IFactoryManager
    {
        [SerializeField] private List<IFactoryBase> factoriesList;      // A collection of the factories.
        [SerializeField] private Transform container;                   // Transform container for all monoBehaviour factories.

        /// <summary>
        /// Initializes the module with the passed ModuleHandler.
        /// </summary>
        /// <param name="handler">Entity handler.</param>
        public void Init(IGlobalManager manager)
        {
            factoriesList = new List<IFactoryBase>();
            if (container.childCount > 0)
                factoriesList.AddRange(container.GetComponentsInChildren<IFactoryBase>());

            foreach (var factory in factoriesList)
                factory.Init(manager);
        }

        /// <summary>
        /// Adds the new factory to the collection.
        /// </summary>
        /// <param name="factory">A factory that implements IFactoryBase interface.</param>
        public void AddFactory(IFactoryBase factory) => factoriesList.Add(factory);

        /// <summary>
        /// Returns the factory of specified T type if found. If not, returns null.
        /// </summary>
        /// <typeparam name="T">A factory that implements IFactoryBase interface.</typeparam>
        public T GetFactory<T>() where T : IFactoryBase => factoriesList.OfType<T>().FirstOrDefault();

        /// <summary>
        /// Removes the factory of specified T type if found.
        /// </summary>
        /// <typeparam name="T">A factory that implements IFactoryBase interface.</typeparam>
        public void RemoveFactory<T>() where T : IFactoryBase
        {
            IFactoryBase factory = factoriesList.OfType<T>().FirstOrDefault();
            if(factory != null) factoriesList.Remove(factory);
        }
    }
}
