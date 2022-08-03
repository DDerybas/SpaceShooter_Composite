using System;

namespace Entities.Modules
{
    /// <summary>
    /// A module that handles the hit/death actions and stores health.
    /// </summary>
    public interface IHitModule : IModule
    {
        /// <summary>
        /// Hit an entity with collided entity.
        /// </summary>
        /// <param name="entity">The entity being collided.</param>
        void Hit(Entity entity);

        /// <summary>
        /// When the entity was hit.
        /// </summary>
        Action OnHitAction { get; set; }
    }
}
