using UnityEngine;
using UnityEngine.AddressableAssets;
using Entities;
using System;

namespace Managers
{
    /// <summary>
    /// A manager that handles the player.
    /// </summary>
    public class PlayerManager : MonoBehaviour, IPlayerManager
    {
        private Entity player;                                          // Cached player entity.
        [SerializeField] private AssetReference playerReference;        // Cached player asset reference.
        [SerializeField] private ObjectType playerType;                 // Player type.

        private IMonoFactory<Entity> factory;                               // Entity factory for spawning the player.
        private IGlobalManager manager;                                 // Cached global manager.

        public Action OnPlayerDeath { get; set; }                       // On player death event.

        /// <summary>
        /// Initializes the manager. Subscribes game win/lose actions. Subscribes to player's input 'reset game' button.
        /// </summary>
        /// <param name="manager">A manager that implements the IGlobalManager interface.</param>
        public void Init(IGlobalManager manager)
        {
            this.manager = manager;
            // Gets the entity factory for player.
            factory = this.manager.GetManager<IFactoryManager>().
                GetFactory<IMonoFactory<Entity>>();

            // Adds player to the entity factory.
            factory.AddAssetToFactory(playerReference);

            // Usually called from CustomSceneManager when the player enters the game. But we have only one level,
            // so we just call it here.
            OnGameStarted();
        }

        /// <summary>
        /// When the actual game is started.
        /// </summary>
        private void OnGameStarted()
        {
            // If factory is loaded - spawn the player, otherwise - subscribe for factory loaded event.
            if (factory.IsReady())
                SpawnPlayer();
            else factory.OnLoadingAssetsComplete += SpawnPlayer;
        }

        /// <summary>
        /// Spawns the player and subscribes for it entity death event.
        /// </summary>
        public void SpawnPlayer()
        {
            player = factory.CreateObject(playerType);
            // Inits the player with the global manager.
            player.Init(manager);
            // Subscribes for it entity death event.
            player.OnEntityDeath += OnPlayerDeath;
        }

        /// <summary>
        /// Returns current player entity.
        /// </summary>
        public Entity GetPlayer() => player;
    }
}
