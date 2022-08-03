namespace Entities.Modules
{
    /// <summary>
    /// A module that disables an entity outside the screen.
    /// </summary>
    public interface IDisableOutsideModule : IModule
    {
        /// <summary>
        /// Disables an entity when outside the screen.
        /// </summary>
        void Disable();
    }
}
