namespace Entities.Modules
{
    /// <summary>
    /// A module that rotates the transform.
    /// </summary>
    public interface IRotateModule : IModule
    {
        /// <summary>
        /// Rotates the object.
        /// </summary>
        void Rotate();
    }
}
