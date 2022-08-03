using UnityEngine;

namespace Missions
{
    /// <summary>
    /// A ScriptableObject mission container that stores missions.
    /// </summary>
    [CreateAssetMenu(fileName = "MissionContaner", menuName = "Missions/Container")]
    public class MissionContainer : ScriptableObject, IMissionContainer
    {
        [SerializeField] private int id;                    // Mission container id.
        [SerializeField] private Mission[] missions;        // A collection of missions.

        /// <summary>
        /// Gets the mission container id.
        /// </summary>
        public int ID { get => id; set => id = value; }

        /// <summary>
        /// Gets the mission by id.
        /// </summary>
        public Mission GetMission(int id)
        {
            if (id > missions.Length - 1)
                return null;
            return missions[id];
        }

        /// <summary>
        /// Returns mission count.
        /// </summary>
        public int GetMissionCount() => missions.Length;

        /// <summary>
        /// Returns the starting mission.
        /// </summary>
        public Mission GetStartMission() => missions[0];
    }
}
