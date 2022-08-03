using System.Collections;
using UnityEngine;
using Missions;
using System;

namespace Managers
{
    /// <summary>
    /// A manager that handles a missions(tasks).
    /// </summary>
    public class MissionManager : MonoBehaviour, IMissionManager
    {
        private IGlobalManager manager;                             // Cached global manager.

        private int currentMissonId;                                // Current mission id.
        private Mission currentMisson;                              // Current active mission.

        [SerializeField] private MissionContainer container;        // ScriptableObject mission container.
        private Coroutine missionCoroutine;                         // Current mission coroutine.

        /// <summary>
        /// Gets the ScriptableObject mission container.
        /// </summary>
        private IMissionContainer currentContainer { get => container; }
        
        /// <summary>
        /// When all missions are completed event.
        /// </summary>
        public Action OnAllMissionsComplete { get; set; }

        /// <summary>
        /// Initializes the manager. Subscribes game win/lose actions.
        /// </summary>
        /// <param name="manager">A manager that implements the IGlobalManager interface.</param>
        public void Init(IGlobalManager manager)
        {
            this.manager = manager;
            StartMission();
        }

        /// <summary>
        /// Gets the start mission from container and starts it.
        /// </summary>
        public void StartMission()
        {
            currentMisson = currentContainer.GetStartMission();
            missionCoroutine = StartCoroutine(MissionTimer());
        }

        /// <summary>
        /// Resets all missions.
        /// </summary>
        public void ResetAllMissions()
        {
            int length = currentContainer.GetMissionCount();
            for (int i = 0; i < length; i++)
                currentContainer.GetMission(i).ResetMission();

            currentMissonId = 0;
        }

        /// <summary>
        /// Stops all missions.
        /// </summary>
        public void StopAllMissions()
        {
            StopAllCoroutines();
            currentMissonId = 0;
            currentMisson = null;

            int length = currentContainer.GetMissionCount();
            for (int i = 0; i < length; i++)
                currentContainer.GetMission(i).StopMission();
        }

        /// <summary>
        /// Current mission tick timer.
        /// </summary>
        private IEnumerator MissionTimer()
        {
            // Waits for MissionStartTime.
            yield return new WaitForSeconds(currentMisson.MissionStartTime);

            // Inits current mission with manager and starts it.
            currentMisson.Init(manager);
            currentMisson.StartMission();
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            Debug.Log($"MissionManager::Mission '{currentMisson.name}' started!");
#endif

            while (true)
            {
                yield return new WaitForSeconds(currentMisson.MissionWorkDelay);

                // If current mission isn't finished - process it.
                if (!currentMisson.IsMissionFinished)
                    currentMisson.DoWork();
                // else if mission is finished but current mission id is not the last in container - get next mission.
                else if (currentMissonId < currentContainer.GetMissionCount() - 1)
                {
                    currentMisson = currentContainer.GetMission(++currentMissonId);
                    currentMisson.Init(manager);
                    missionCoroutine = StartCoroutine(MissionTimer());
                    yield break;
                }
                else break;
            }

            // If all missions are finished - proceed to success.
            FinishAllMissions();
        }

        /// <summary>
        /// Invokes OnAllMissionsComplete event.
        /// </summary>
        private void FinishAllMissions()
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            Debug.Log("All missions are finished!");
#endif
            OnAllMissionsComplete?.Invoke();
        }
    }
}
