using UnityEngine;
using Entities.Modules;
using Managers;
using System;

namespace Entities
{
    /// <summary>
    /// Handles its own list of modules. Each module can handle own logic.
    /// </summary>
    public class Entity : MonoBehaviour
    {
        bool isInitialized = false;                                     // Is entity initialized?    

        [SerializeField] private ObjectType _entityType;                // A ScriptableObject type of the entity.
        public ObjectType EntityType { get => _entityType; set => _entityType = value; }

        [SerializeField] private Transform modulesParent;               // A Transform that contains all modules as a separated gameObjects.
        private IModuleHandler moduleHandler;                           // Processes all modules. Allows to Get/Add/Remove a module.

        // Entity position property.
        public Vector2 Position { get => transform.position; set => transform.position = value; }
        // Entity direction property.
        public Vector2 Direction { get => transform.rotation.eulerAngles; set => transform.rotation = Quaternion.Euler(value); }

        public Action OnEntityDeath;

        /// <summary>
        /// Initializes the Entity.
        /// </summary>
        public void Init(IGlobalManager globalManager)
        {
            // If already initialized - resets all modules. Used to simulate restarting the game.
            if (isInitialized)
            {
                // Calls a reset action for all subscribers.
                moduleHandler.OnModulesReset?.Invoke();
                return;
            }
            
            // Initializes a new module handler.
            moduleHandler = new ModuleHandler(this, modulesParent, globalManager);
            
            // Initializes all handler modules.
            moduleHandler.InitModules();
            isInitialized = true;
        }

        /// <summary>
        /// Retruns the entity gameObject.
        /// </summary>
        public GameObject GetGameObject() => gameObject;

        /// <summary>
        /// Retruns the entity transform.
        /// </summary>
        public Transform GetTransform() => transform;

        /// <summary>
        /// Retruns the entity position.
        /// </summary>
        public Vector2 GetPosition() => transform.position;

        /// <summary>
        /// Retruns the entity module handler.
        /// </summary>
        public IModuleHandler GetHandler() => moduleHandler;

        /// <summary>
        /// Sets the entity position.
        /// </summary>
        public void SetPosition(Vector2 pos) => transform.position = pos;

        /// <summary>
        /// Sets the entity active state.
        /// </summary>
        public void SetActive(bool active) => gameObject.SetActive(active);
    }
}
