namespace Missions
{
    /// <summary>
    /// A mission container that handles missions.
    /// </summary>
    public interface IMissionContainer
    {
        /// <summary>
        /// Gets the mission container id.
        /// </summary>
        int ID { get; set; }

        /// <summary>
        /// Gets the mission by id.
        /// </summary>
        Mission GetMission(int id);

        /// <summary>
        /// Returns the starting mission.
        /// </summary>
        Mission GetStartMission();

        /// <summary>
        /// Returns mission count.
        /// </summary>
        int GetMissionCount();
    }
}
