using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Modules
{
    /// <summary>
    /// Movement module interface for any movable entity in the game (player, ai, etc.)
    /// </summary>
    public interface IMovementModule : IModule
    {
        void SetDirection(Vector2 direction);       // Sets the direction of movement of the entity.
    }
}
