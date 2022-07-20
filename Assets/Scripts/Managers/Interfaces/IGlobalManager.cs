namespace Managers
{
    /// <summary>
    /// A global manager containing all the managers in the game.
    /// </summary>
    public interface IGlobalManager : IManager
    {
        /// <summary>
        /// Looks for a manager in the collection of managers and returns if found.
        /// </summary>
        /// <typeparam name="T">Manager of type IManager</typeparam>
        /// <returns>Manager of type IManager</returns>
        T GetManager<T>() where T : IManager;

        /// <summary>
        /// Adds manager to the collection of managers.
        /// </summary>
        /// <param name="manager">Manager of type IManager</param>
        void AddManager(IManager manager);

        /// <summary>
        /// Removes the passed manager from the collection of managers, if the collection contains one.
        /// </summary>
        /// <param name="manager">Manager of type IManager</param>
        void RemoveManager(IManager manager);

        /// <summary>
        /// Calls after manager initialization.
        /// </summary>
        System.Action OnInitEndAction { get; set; }
    }
}
