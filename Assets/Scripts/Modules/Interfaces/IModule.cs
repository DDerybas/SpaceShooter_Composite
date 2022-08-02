using UnityEngine;

namespace Entities.Modules
{
    /// <summary>
    /// An interface for all Entity's modules.
    /// </summary>
    public interface IModule
    {
        /// <summary>
        /// Initializes the module with the passed ModuleHandler.
        /// </summary>
        /// <param name="handler">Entity handler.</param>
        void Init(IModuleHandler handler);

        /// <summary>
        /// Returns the game object to which the module is attached.
        /// </summary>
        /// <returns>Entity gameObject.</returns>
        GameObject GetGameObject();
    }
}
