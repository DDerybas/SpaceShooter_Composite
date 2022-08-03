namespace Entities.Modules
{
    /// <summary>
    /// A module that handles the entity weapon.
    /// </summary>
    public interface IWeaponModule : IModule
    {
        /// <summary>
        /// Spawns the bullet entity.
        /// </summary>
        void Shoot();
    }
}
