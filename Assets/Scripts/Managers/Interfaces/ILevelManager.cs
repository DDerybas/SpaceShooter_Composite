using UnityEngine;

namespace Managers
{
    /// <summary>
    /// A manager that handles scene stuff. Like a spawn positions mentioned here.
    /// </summary>
    public interface ILevelManager : IManager
    {
        /// <summary>
        /// Returns the Vector2 position of the next spawn point.
        /// </summary>
        Vector2 GetNextSpawnPoint();

        /// <summary>
        /// Returns the Vector2 position of the previous spawn point.
        /// </summary>
        Vector2 GetPreviousSpawnPoint();

        /// <summary>
        /// Returns the Vector2 position of the random spawn point.
        /// </summary>
        Vector2 GetRandomSpawnPoint();

        /// <summary>
        /// Returns the Vector2 position of the center spawn point.
        /// </summary>
        Vector2 GetCenterSpawnPoint();
    }
}
