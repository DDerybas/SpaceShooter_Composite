using System;

namespace Entities.Modules
{
    /// <summary>
    /// A module that handles the entity collision.
    /// </summary>
    public interface ICollisionModule : IModule
    {
        /// <summary>
        /// On trigger collision event.
        /// </summary>
        Action<Entity> OnTriggerEnterAction { get; set; }
    }
}
