using UnityEngine;

namespace Entities.Modules
{
    /// <summary>
    /// Movement module interface for any movable entity in the game (player, ai, etc.)
    /// </summary>
    public interface IMovementModule : IModule
    {
        /// <summary>
        /// Sets the direction of movement of the entity.
        /// </summary>
        /// <param name="direction">The direction of movement.</param>
        void SetDirection(Vector2 direction);

        /// <summary>
        /// // Sets the speed of movement of the entity.
        /// </summary>
        /// <param name="speed">Movement speed.</param>
        void SetSpeed(float speed);
    }
}
