namespace Managers
{
    /// <summary>
    /// A manager that handles game states win/lose.
    /// </summary>
    public interface IGameStateManager : IManager
    {
        /// <summary>
        /// Win game event.
        /// </summary>
        void WinGame();

        /// <summary>
        /// Lose game event.
        /// </summary>
        void LoseGame();
    }
}
