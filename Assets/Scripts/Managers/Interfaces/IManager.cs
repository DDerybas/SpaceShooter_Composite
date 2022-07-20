namespace Managers
{
    /// <summary>
    /// Interface for all game managers (audio, inputs, enemies, etc.) 
    /// </summary>
    public interface IManager
    {
        /// <summary>
        /// Initializes the manager.
        /// </summary>
        void Init(IGlobalManager manager);
    }
}
