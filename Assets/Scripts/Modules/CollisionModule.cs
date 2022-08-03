using System;
using UnityEngine;

namespace Entities.Modules
{
    /// <summary>
    /// A module that handles the entity collision.
    /// </summary>
    public class CollisionModule : MonoBehaviour, ICollisionModule
    {
        private Entity entityOwner;                                 // The cached entity.
        [SerializeField] private bool destroyOnCollision;           // Destroy the entity on collision if true.

        /// <summary>
        /// On trigger collision event.
        /// </summary>
        public Action<Entity> OnTriggerEnterAction { get; set; }

        /// <summary>
        /// Initializes the module with the passed ModuleHandler.
        /// </summary>
        /// <param name="handler">Entity handler.</param>
        public void Init(IModuleHandler handler) => entityOwner = handler.GetEntity();

        /// <summary>
        /// On trigger collision with another entity.
        /// </summary>
        private void OnTriggerEnter2D(Collider2D collision)
        {
            // if true - destroy entity.
            if (destroyOnCollision)
                entityOwner.SetActive(false);

            var entity = collision.GetComponent<Entity>();
            if (entity == null)
                return;

            // Invokes OnTriggerEnter action.
            OnTriggerEnterAction?.Invoke(entity);
        }

        /// <summary>
        /// Returns the game object to which the module is attached.
        /// </summary>
        /// <returns>Entity gameObject.</returns>
        public GameObject GetGameObject() => gameObject;
    }
}
