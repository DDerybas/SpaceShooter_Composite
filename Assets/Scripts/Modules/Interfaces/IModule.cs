using System.Collections.Generic;
using UnityEngine;

namespace Entities.Modules
{
    /// <summary>
    /// An interface for all Entity's modules.
    /// </summary>
    public interface IModule
    {
        /// <summary>
        /// A modules handler that processes all the modules it contains.
        /// </summary>
        ModuleHandler Handler { get; set; }

        /// <summary>
        /// All modules on which this particular module depends. 
        /// </summary>
        List<IModule> BindedModules { get; set; }

        /// <summary>
        /// Initializes the module with the passed ModuleHandler.
        /// </summary>
        /// <param name="handler">Entity handler.</param>
        void Init(ModuleHandler handler);

        /// <summary>
        /// Returns the game object to which the module is attached.
        /// </summary>
        /// <returns>Entity gameObject.</returns>
        GameObject GetGameObject();
    }
}
