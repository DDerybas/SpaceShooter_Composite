using System.Collections.Generic;
using UnityEngine;
using System;
using Managers;
using System.Linq;

namespace Entities.Modules
{
    /// <summary>
    /// Initializes and processes all modules. Allows to Get/Add/Remove a module.
    /// </summary>
    public class ModuleHandler : IModuleHandler
    {
        private Entity entity;                                      // The parent entity that holds the handler.
        private Transform modulesParent;                            // Transform that holds all the modules.
        private List<IModule> modules = new List<IModule>();        // List of all handled modules.

        /// <summary>
        /// A global manager containing all the managers in the game.
        /// </summary>
        public IGlobalManager GlobalManager { get; set; }

        /// <summary>
        /// Invokes after all modules have been initialized.
        /// </summary>
        public Action OnInitModulesEnd { get; set; }

        /// <summary>
        /// Invokes after the handler is reactivated.
        /// </summary>
        public Action OnModulesReset { get; set; }

        /// <summary>
        /// Returns true after all modules have been initialized.
        /// </summary>
        public bool AreAllModulesInitialized { get; set; }

        public ModuleHandler(Entity entity, Transform modulesParent, IGlobalManager globalManager)
        {
            this.entity = entity;
            this.modulesParent = modulesParent;
            GlobalManager = globalManager;
        }

        /// <summary>
        /// Initializes all handled modules.
        /// </summary>
        public void InitModules()
        {
            modules.AddRange(modulesParent.GetComponentsInChildren<IModule>());
            for (int i = 0; i < modules.Count; i++)
                modules[i].Init(this);

            // Completes initialization of handled modules. Required to retrieve the module after initialization.
            OnInitModulesEnd?.Invoke();
            AreAllModulesInitialized = true;
        }

        /// <summary>
        /// Returns the module if found, and places it in the bindedModules collection of the passed parent module.
        /// </summary>
        /// <typeparam name="T">The requested type of the module that implements the IModule interface.</typeparam>
        /// <param name="module">The parent module in which the requested module is returned.</param>
        /// <returns>A module of type T that implements IModule interface.</returns>
        public T GetModule<T>() where T : IModule
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            T moduleFound = modules.OfType<T>().FirstOrDefault();
            if (moduleFound == null) Debug.LogError($"Module of type '{typeof(T)}' not found!");
            return moduleFound;
#else
            return modules.OfType<T>().FirstOrDefault();
#endif
        }

        /// <summary>
        /// Returns all modules of type T if found.
        /// </summary>
        /// <typeparam name="T">The requested type of the module that implements the IModule interface.</typeparam>
        /// <returns>Modules of type T that implement IModule interface.</returns>
        public T[] GetAllModulesOfType<T>() where T : IModule
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            T[] modulesFound = modules.OfType<T>().ToArray();
            if (modulesFound == null) Debug.LogError($"Modules of type '{typeof(T)}' not found!");
            return modulesFound;
#else
            return modules.OfType<T>().ToArray();
#endif
        }

        /// <summary>
        /// Adds a module of type T.
        /// </summary>
        /// <typeparam name="T">A module of type T that implements IModule interface.</typeparam>
        public void AddModule<T>() where T : IModule
        {
            // Creates a new game object that will contain the module and sets its parent to the modulesParent transform.
            var moduleGo = new GameObject(typeof(T).Name);
            moduleGo.transform.parent = modulesParent;
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
        public Entity GetEntity() => entity;
    }
}
