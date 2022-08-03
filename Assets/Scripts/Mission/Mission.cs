using UnityEngine;
using Managers;

namespace Missions
{
    /// <summary>
    /// A ScriptableObject mission base class that contains all mission details.
    /// </summary>
    public abstract class Mission : ScriptableObject
    {
        [SerializeField] private float _missionStartTime;       // The time after which the mission starts.
        [SerializeField] private float _missionWorkDelay;       // The time between DoWork() method calls.

        /// <summary>
        /// The time after which the mission starts.
        /// </summary>
        public float MissionStartTime { get => _missionStartTime; protected set => _missionStartTime = value; }

        /// <summary>
        /// The time between DoWork() method calls.
        /// </summary>
        public float MissionWorkDelay { get => _missionWorkDelay; protected set => _missionWorkDelay = value; }

        /// <summary>
        /// Was mission started?
        /// </summary>
        public bool IsMissionStarted { get; protected set; }

        /// <summary>
        /// Was mission finished?
        /// </summary>
        public bool IsMissionFinished { get; protected set; }

        /// <summary>
        /// Inits the mission with a global manager.
        /// </summary>
        /// <param name="manager">The manager that implements IGlobalManager interface.</param>
        public abstract void Init(IGlobalManager manager);

        /// <summary>
        /// Starts the mission.
        /// </summary>
        public abstract void StartMission();

        /// <summary>
        /// Proceeds the mission.
        /// </summary>
        public abstract void DoWork();

        /// <summary>
        /// Checks if the mission is finished.
        /// </summary>
        public abstract void CheckForMissionEnd();

        /// <summary>
        /// Resets the mission.
        /// </summary>
        public abstract void ResetMission();

        /// <summary>
        /// Stops the mission.
        /// </summary>
        public abstract void StopMission();
    }
}
