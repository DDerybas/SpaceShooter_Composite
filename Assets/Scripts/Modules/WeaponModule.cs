using UnityEngine;
using Managers;

namespace Entities.Modules
{
    /// <summary>
    /// A module that handles the entity weapon.
    /// </summary>
    public class WeaponModule : MonoBehaviour, IWeaponModule
    {
        private IGlobalManager manager;                         // Cached global manager.

        [SerializeField] private ObjectType bulletType;         // The type of the bullet entity.
        [SerializeField] private Transform shootPoint;          // Start point of the bullet spawn.
        private IPool<Entity> bulletPool;                       // Bullet entity pool.

        private Vector2 directionFacing;                        // The direction of the bullet entity.

        [SerializeField] private float shootDelay;              // Shooting delay.
        private float shootTimer;                               // Shooting delay timer value.

        /// <summary>
        /// Initializes the module with the passed ModuleHandler.
        /// </summary>
        /// <param name="handler">Entity handler.</param>
        public virtual void Init(IModuleHandler handler)
        {
            manager = handler.GlobalManager;
            bulletPool = manager.GetManager<IPoolManager>().
                GetPool<IPool<Entity>>(bulletType);

            // Sets the direction of the bullet entity in the direction of the owner entity.  
            directionFacing = handler.GetEntity().Direction;
            shootTimer = shootDelay;
        }

        // Counts the timer.
        protected virtual void Update() => shootTimer += Time.deltaTime;

        /// <summary>
        /// Spawns the bullet entity.
        /// </summary>
        public void Shoot()
        {
            if (shootTimer < shootDelay)
                return;

            shootTimer = 0;
            Entity bullet = bulletPool.GetPoolableObject();
            bullet.Init(manager);
            bullet.Position = shootPoint.position;
            bullet.Direction = directionFacing;
        }

        /// <summary>
        /// Returns the game object to which the module is attached.
        /// </summary>
        /// <returns>Entity gameObject.</returns>
        public GameObject GetGameObject() => gameObject;
    }
}
