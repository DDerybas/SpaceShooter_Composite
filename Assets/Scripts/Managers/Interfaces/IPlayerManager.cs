using Entities;
using System;

namespace Managers
{
    /// <summary>
    /// A manager that handles the player.
    /// </summary>
    public interface IPlayerManager : IManager
    {
        /// <summary>
        /// Spawns the player and subscribes for it entity death event.
        /// </summary>
        void SpawnPlayer();

        /// <summary>
        /// Returns current player entity.
        /// </summary>
        Entity GetPlayer();

        /// <summary>
        /// On player death event.
        /// </summary>
        Action OnPlayerDeath { get; set; }
    }
}
