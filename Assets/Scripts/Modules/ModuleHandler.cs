using System.Collections.Generic;
using UnityEngine;
using System;

namespace Entities.Modules
{
    /// <summary>
    /// Initializes and processes all modules. Allows to Get/Add/Remove a module.
    /// </summary>
    public class ModuleHandler : IModuleHandler
    {
        private Entity entity;                                      // The parent entity that holds the handler.
        private List<IModule> modules = new List<IModule>();        // List of all handled modules.
        public Action OnInitModulesEnd { get; set; }                // Invokes after all modules have been initialized.

        public ModuleHandler(Entity entity)
        {
            this.entity = entity;
            InitModules();
        }

        /// <summary>
        /// Initializes all handled modules.
        /// </summary>
        void InitModules()
        {
            modules.AddRange(entity.modulesParent.GetComponentsInChildren<IModule>());
            for (int i = 0; i < modules.Count; i++)
                modules[i].Init(this);

            // Completes initialization of handled modules. Required to retrieve the module after initialization.
            OnInitModulesEnd?.Invoke();
        }

        /// <summary>
        /// Returns the module if found, and places it in the bindedModules collection of the passed parent module.
        /// </summary>
        /// <typeparam name="T">The requested type of the module that implements the IModule interface.</typeparam>
        /// <param name="module">The parent module in which the requested module is returned.</param>
        /// <returns>A module of type T that implements IModule interface.</returns>
        public T RetrieveModule<T>(IModule module) where T : IModule
        {
            for (int i = 0; i < modules.Count; i++)
            {
                if (modules[i] is T)
                {
                    module.BindedModules.Add(modules[i]);
                    return (T)modules[i];
                }
            }

#if UNITY_EDITOR || DEVELOPMENT_BUILD
            Debug.LogError(string.Format("Module of type '{0}' not found!", module.GetType().ToString()));
#endif
            return default;
        }

        /// <summary>
        /// Adds a module of type T.
        /// </summary>
        /// <typeparam name="T">A module of type T that implements IModule interface.</typeparam>
        public void AddModule<T>() where T : IModule
        {
            // Creates a new game object that will contain the module and sets its parent to the modulesParent transform.
            var moduleGo = new GameObject(typeof(T).Name);
            moduleGo.transform.parent = entity.modulesParent;
            // Adds a module to the created game object and to the handled modules collection.
            var module = moduleGo.AddComponent(typeof(T)) as IModule;
            modules.Add(module);
        }

        /// <summary>
        /// Removes a module of type T if found.
        /// </summary>
        /// <typeparam name="T">A module of type T that implements IModule interface.</typeparam>
        public void RemoveModule<T>() where T : IModule
        {
            for (int i = 0; i < modules.Count; i++)
            {
                if (modules[i] is T)
                {
                    // Destroys the game object with the found module.
                    UnityEngine.Object.Destroy(modules[i].GetGameObject());
                    // Removes from the handled modules collection.
                    modules.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Returns the entity that holds the handler.
        /// </summary>
        /// <returns></returns>
        public Entity GetEntity()
        {
            return entity;
        }
    }
}
