namespace Entities.Modules
{
    /// <summary>
    /// A module that checks for collision with the upgrade bonus and upgrades the entity.
    /// </summary>
    public interface IUpgradeModule : IModule
    {
        /// <summary>
        /// Upgrade self.
        /// </summary>
        void Upgrade();
    }
}
