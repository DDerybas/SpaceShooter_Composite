using System;

namespace Entities.Modules
{
    /// <summary>
    /// Initializes and processes all modules. Allows to Get/Add/Remove a module.
    /// </summary>
    public interface IModuleHandler
    {
        /// <summary>
        /// A global manager containing all the managers in the game.
        /// </summary>
        public Managers.IGlobalManager GlobalManager { get; set; }

        /// <summary>
        /// Initializes all handled modules.
        /// </summary>
        void InitModules();

        /// <summary>
        /// Returns the module if found.
        /// </summary>
        /// <typeparam name="T">The requested type of the module that implements the IModule interface.</typeparam>
        /// <returns>A module of type T that implements IModule interface.</returns>
        T GetModule<T>() where T : IModule;

        /// <summary>
        /// Returns all modules of type T if found.
        /// </summary>
        /// <typeparam name="T">The requested type of the module that implements the IModule interface.</typeparam>
        /// <returns>Modules of type T that implement IModule interface.</returns>
        T[] GetAllModulesOfType<T>() where T : IModule;

        /// <summary>
        /// Adds a module of type T.
        /// </summary>
        /// <typeparam name="T">A module of type T that implements IModule interface.</typeparam>
        void AddModule<T>() where T : IModule;

        /// <summary>
        /// Removes a module of type T if found.
        /// </summary>
        /// <typeparam name="T">A module of type T that implements IModule interface.</typeparam>
        void RemoveModule<T>() where T : IModule;

        /// <summary>
        /// Returns the entity that holds the handler.
        /// </summary>
        /// <returns></returns>
        Entity GetEntity();

        /// <summary>
        /// Invokes after all modules have been initialized.
        /// </summary>
        Action OnInitModulesEnd { get; set; }

        /// <summary>
        /// Resets the modules values.
        /// </summary>
        Action OnModulesReset { get; set; }

        /// <summary>
        /// Returns true after all modules have been initialized.
        /// </summary>
        bool AreAllModulesInitialized { get; set; }
    }
}
