using System.Collections.Generic;
using UnityEngine;

namespace Entities.Modules
{
    /// <summary>
    /// An interface for all Entity's modules.
    /// </summary>
    public interface IModule
    {
        ModuleHandler handler { get; set; }             // A modules handler that processes all the modules it contains.
        List<IModule> bindedModules { get; set; }       // All modules on which this particular module depends. 
        void Init(ModuleHandler handler);               // Initializes the module with the passed ModuleHandler.
        void OnInitEnd();                               // Completes initialization of the module.
        GameObject GetGameObject();                     // Returns the game object to which the module is attached.
    }
}
