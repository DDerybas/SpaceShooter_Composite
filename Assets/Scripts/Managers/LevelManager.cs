using UnityEngine;

/// <summary>
/// Where should the object be spawned.
/// </summary>
public enum SpawnPositionType
{
    Forward,
    Backward,
    Random,
    Center
}

namespace Managers
{
    /// <summary>
    /// A manager that handles scene stuff. Like a spawn positions mentioned here.
    /// </summary>
    public class LevelManager : MonoBehaviour, ILevelManager
    {
        [SerializeField] private Transform spawnPointParent;        // Container for the spawn points.
        private Vector2[] _spawnPoints;                             // Spawn positions array.
        private int currentSpawnPoint = -1;                         // Current spawn point.

        /// <summary>
        /// Returns the Vector2 position of the next spawn point.
        /// </summary>
        public Vector2 GetNextSpawnPoint()
        {
            currentSpawnPoint++;
            if (currentSpawnPoint >= _spawnPoints.Length)
                currentSpawnPoint = 0;
            return _spawnPoints[currentSpawnPoint];
        }

        /// <summary>
        /// Returns the Vector2 position of the previous spawn point.
        /// </summary>
        public Vector2 GetPreviousSpawnPoint()
        {
            currentSpawnPoint--;
            if (currentSpawnPoint < 0)
                currentSpawnPoint = _spawnPoints.Length-1;
            return _spawnPoints[currentSpawnPoint];
        }

        /// <summary>
        /// Returns the Vector2 position of the random spawn point.
        /// </summary>
        public Vector2 GetRandomSpawnPoint() => _spawnPoints[Random.Range(0, _spawnPoints.Length)];

        /// <summary>
        /// Returns the Vector2 position of the center spawn point.
        /// </summary>
        public Vector2 GetCenterSpawnPoint()
        {
            float y = _spawnPoints[0].y;
            return new Vector2(0, y);
        }

        /// <summary>
        /// Initializes the manager. Subscribes game win/lose actions.
        /// </summary>
        /// <param name="manager">A manager that implements the IGlobalManager interface.</param>
        public void Init(IGlobalManager manager)
        {
            var spawnTransforms = spawnPointParent.GetComponentsInChildren<Transform>();
            int spawnersCount = spawnTransforms.Length;
            _spawnPoints = new Vector2[spawnersCount-1];

            //1 - ignoring container Transform.
            for (int i = 1; i < spawnersCount; i++)
                _spawnPoints[i-1] = spawnTransforms[i].position;
        }
    }
}
