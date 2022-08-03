using System;

namespace Managers
{
    /// <summary>
    /// A manager that handles a missions(tasks).
    /// </summary>
    public interface IMissionManager : IManager
    {
        /// <summary>
        /// Starts the mission.
        /// </summary>
        void StartMission();

        /// <summary>
        /// Stops all missions.
        /// </summary>
        void StopAllMissions();

        /// <summary>
        /// Resets all missions.
        /// </summary>
        void ResetAllMissions();

        /// <summary>
        /// When all missions are completed event.
        /// </summary>
        Action OnAllMissionsComplete { get; set; }
    }
}
