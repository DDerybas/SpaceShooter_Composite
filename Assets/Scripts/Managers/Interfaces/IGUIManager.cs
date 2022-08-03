namespace Managers
{
    /// <summary>
    /// A manager that handles all game gui.
    /// </summary>
    public interface IGUIManager : IManager 
    {
        /// <summary>
        /// Activates the window of specified type if found.
        /// </summary>
        /// <param name="windowType">The type of the window.</param>
        void ShowWindow(ObjectType windowType);

        /// <summary>
        /// Deactivates the window of specified type if found.
        /// </summary>
        /// <param name="windowType">The type of the window.</param>
        void HideWindow(ObjectType windowType);

        /// <summary>
        /// Deactivate all windows.
        /// </summary>
        void HideAllWindows();
    }
}
