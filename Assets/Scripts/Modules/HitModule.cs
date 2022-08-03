using System;
using UnityEngine;
using Managers;

namespace Entities.Modules
{
    /// <summary>
    /// A module that handles the hit/death actions and stores health.
    /// </summary>
    public class HitModule : MonoBehaviour, IHitModule
    {
        /// <summary>
        /// When the entity was hit.
        /// </summary>
        public Action OnHitAction { get; set; }

        private Entity ownerEntity;                                 // Entity that handles this module.

        [SerializeField] private int maxHealth;                     // Max entity health.
        private int health;                                         // Current entity health.
        private ICollisionModule collisionModule;                   // Cached collision module.

        [SerializeField] private ObjectType deathEffectType;        // Death particle.
        private IMonoPool<Entity> effectPool;                       // Cached effects/particles pool.

        /// <summary>
        /// Initializes the module with the passed ModuleHandler.
        /// </summary>
        /// <param name="handler">Entity handler.</param>
        public void Init(IModuleHandler handler)
        {
            // Sets current health to max on init.
            health = maxHealth;

            ownerEntity = handler.GetEntity();

            effectPool = handler.GlobalManager.GetManager<IPoolManager>().
                GetPool<IMonoPool<Entity>>(deathEffectType);

            collisionModule = handler.GetModule<ICollisionModule>();

            // Subscribes on the collision event.
            collisionModule.OnTriggerEnterAction += Hit;
            
            // Subscribes on modules reset event (used when the player restarts the game).
            handler.OnModulesReset += ResetModule;
        }

        /// <summary>
        /// Hit an entity with collided entity.
        /// </summary>
        /// <param name="entity">The entity being collided.</param>
        public void Hit(Entity entity)
        {
            // We don't collide entities with the same tag. 
            if (entity.tag == ownerEntity.tag)
                return;

            // Inokes hit event.
            OnHitAction?.Invoke();

            // Reduces health.
            health--;
            if(health <= 0)
            {
                // Resetting a value.
                health = maxHealth;
                Death();
            }
        }

        // Deactivates entity on Death event to reuse it in pool.
        private void Death()
        {
            // Deactivates owner entity.
            ownerEntity.SetActive(false);
            // Creates death effect in transform position
            effectPool.GetPoolableObject(transform.position);
            // Invokes entity death event.
            ownerEntity.OnEntityDeath?.Invoke();
        }

        /// <summary>
        /// Returns the game object to which the module is attached.
        /// </summary>
        /// <returns>Entity gameObject.</returns>
        public GameObject GetGameObject() => gameObject;

        /// <summary>
        /// Resets the module values.
        /// </summary>
        public void ResetModule() => health = maxHealth;
    }
}
