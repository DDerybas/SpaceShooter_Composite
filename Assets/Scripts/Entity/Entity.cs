using System.Collections.Generic;
using UnityEngine;
using System;
using Entities.Modules;

namespace Entities
{
    /// <summary>
    /// Handles its own list of modules. Each module can handle own logic.
    /// </summary>
    public class Entity : MonoBehaviour
    {
        public Transform modulesParent;         // A Transform that contains all modules as a separated gameObjects.
        IModuleHandler moduleHandler;           // Processes all modules. Allows to Get/Add/Remove a module.

        /// <summary>
        /// Initializes the Entity.
        /// </summary>
        public void Init()
        {
            moduleHandler = new ModuleHandler(this);
        }
    }
}
