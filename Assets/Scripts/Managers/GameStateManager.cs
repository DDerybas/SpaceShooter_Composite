using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Managers
{
    /// <summary>
    /// A manager that handles game states win/lose and triggers the ui.
    /// </summary>
    public class GameStateManager : MonoBehaviour, IGameStateManager
    {
        private IPlayerManager playerManager;           // Cached player manager.
        private IMissionManager missionManager;         // Cached mission manager.
        private IGUIManager guiManager;                 // Cached gui manager.
        private InputActions inputActions;              // Player input actions.

        [SerializeField] private ObjectType loseWindowType;         // Lose game window type.
        [SerializeField] private ObjectType winWindowType;          // Win game window type.

        /// <summary>
        /// Initializes the manager. Subscribes game win/lose actions. Subscribes to player's input 'reset game' button.
        /// </summary>
        /// <param name="manager">A manager that implements the IGlobalManager interface.</param>
        public void Init(IGlobalManager manager)
        {
            playerManager = manager.GetManager<IPlayerManager>();
            missionManager = manager.GetManager<IMissionManager>();
            guiManager = manager.GetManager<IGUIManager>();

            // Subscribes game win/lose actions.
            missionManager.OnAllMissionsComplete += WinGame;
            playerManager.OnPlayerDeath += LoseGame;

            // Subscribes to player's input 'reset game' button.
            inputActions = new InputActions();
            inputActions.Player.Reset.performed += ResetGame;

            // Disables the input.
            SetInputsActive(false);

            // Subscribes for scene loaded event.
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        }

        /// <summary>
        /// Game scene loaded.
        /// </summary>
        private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            guiManager.HideAllWindows();
            playerManager.SpawnPlayer();
            missionManager.StartMission();
        }

        /// <summary>
        /// Activates/Deactivates 'reset game' input key.
        /// </summary>
        private void SetInputsActive(bool value)
        {
            if(value) inputActions.Player.Reset.Enable();
            else inputActions.Player.Reset.Disable();
        }

        /// <summary>
        /// Resets the game on input. Deactivates the inputs.
        /// </summary>
        private void ResetGame(InputAction.CallbackContext obj)
        {
            SetInputsActive(false);
            missionManager.ResetAllMissions();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        /// <summary>
        /// Win game event.
        /// </summary>
        public void WinGame()
        {
            // Shows the "win" window. 
            guiManager.ShowWindow(winWindowType);
            // Activates reset key input.
            SetInputsActive(true);
        }

        /// <summary>
        /// Lose game event.
        /// </summary>
        public void LoseGame()
        {
            // Shows the "lose" window.
            guiManager.ShowWindow(loseWindowType);
            // Stops all missions.
            missionManager.StopAllMissions();
            // Deactivates reset key input.
            SetInputsActive(true);
        }
    }
}
