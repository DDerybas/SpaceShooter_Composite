using System.Collections.Generic;
using UnityEngine;
using Entities;
using System.Linq;
using Managers;

namespace Missions
{
    [CreateAssetMenu(fileName = "MissionSpawn_", menuName = "Missions/Type/Spawn")]
    public class MissionSpawn : Mission
    {
        private IGlobalManager globalManager;                               // Cached global manager.
        private ILevelManager levelManager;                                 // Cached level manager.
        private IPool<Entity> entityPool;                                   // Entities to spawn pool.

        [SerializeField] private ObjectType entityType;                     // Entity to spawn type.
        [SerializeField] private int entitiesToSpawnCount;                  // Entities spawn count.
        [SerializeField] private SpawnPositionType spawnPositionType;       // Entities spawn position type.
        private bool allEntitiesAreSpawned;                                 // Are all entities spawned?

        private List<Entity> spawnedEntities = new List<Entity>();          // List of spawned entities.

        /// <summary>
        /// Inits the mission with a global manager.
        /// </summary>
        /// <param name="manager">The manager that implements IGlobalManager interface.</param>
        public override void Init(IGlobalManager manager)
        {
            globalManager = manager;

            if (entityPool == null)
                entityPool = globalManager.GetManager<IPoolManager>().GetPool<IPool<Entity>>(entityType);

            if (levelManager == null)
                levelManager = globalManager.GetManager<ILevelManager>();

            // Resets the mission on init.
            ResetMission();
        }

        /// <summary>
        /// Starts the mission.
        /// </summary>
        public override void StartMission() => IsMissionStarted = true;

        /// <summary>
        /// Resets the mission.
        /// </summary>
        public override void ResetMission()
        {
            spawnedEntities.Clear();
            IsMissionFinished = false;
            IsMissionStarted = false;
            allEntitiesAreSpawned = false;
        }

        /// <summary>
        /// Proceeds the mission.
        /// </summary>
        public override void DoWork()
        {
            // If mission is finished or pool is not ready - return.
            if (IsMissionFinished || !entityPool.IsReady())
                return;

            // If there are entities to spawn - spawn.
            if (!allEntitiesAreSpawned)
                Spawn();
            // Otherwise if there are no entities to spawn and no active entities - finish mission.
            else if (!IsAnyEntityActive())
            {
                IsMissionFinished = true;
#if UNITY_EDITOR || DEVELOPMENT_BUILD
                Debug.Log($"Mission '{name}' finished!");
#endif
                return;
            }
        }

        /// <summary>
        /// Checks if the mission is finished.
        /// </summary>
        public override void CheckForMissionEnd() => allEntitiesAreSpawned = 
            spawnedEntities.Count >= entitiesToSpawnCount;

        /// <summary>
        /// Spawns an entity from entity pool.
        /// </summary>
        private void Spawn()
        {
            Entity entity = entityPool.GetPoolableObject();
            entity.SetPosition(GetSpawnPosition());
            entity.Init(globalManager);

            // Adds spawned entity to spawned entity collection.
            spawnedEntities.Add(entity);
            // Checks if there is something else to spawn.
            CheckForMissionEnd();
        }

        /// <summary>
        /// Returns the position of the spawn point, which depends on the mission spawn position type.
        /// </summary>
        private Vector2 GetSpawnPosition() => spawnPositionType switch
        {
            SpawnPositionType.Forward => levelManager.GetNextSpawnPoint(),
            SpawnPositionType.Backward => levelManager.GetPreviousSpawnPoint(),
            SpawnPositionType.Random => levelManager.GetRandomSpawnPoint(),
            SpawnPositionType.Center => levelManager.GetCenterSpawnPoint(),
            _ => levelManager.GetRandomSpawnPoint(),
        };

        /// <summary>
        /// Are there any active entities on the screen?
        /// </summary>
        private bool IsAnyEntityActive() => spawnedEntities.Any(x => x.GetGameObject().activeSelf == true);

        /// <summary>
        /// Stops the mission.
        /// </summary>
        public override void StopMission()
        {
            int length = spawnedEntities.Count;
            for (int i = 0; i < length; i++)
                spawnedEntities[i].SetActive(false);
        }
    }
}
