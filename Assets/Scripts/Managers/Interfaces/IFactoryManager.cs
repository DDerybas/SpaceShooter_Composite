namespace Managers
{
    /// <summary>
    /// Handles a collection of factories.
    /// </summary>
    public interface IFactoryManager : IManager
    {
        /// <summary>
        /// Adds the new factory to the collection.
        /// </summary>
        /// <param name="factory">A factory that implements IFactoryBase interface.</param>
        void AddFactory(IFactoryBase factory);

        /// <summary>
        /// Returns the factory of specified T type if found. If not, returns null.
        /// </summary>
        /// <typeparam name="T">A factory that implements IFactoryBase interface.</typeparam>
        T GetFactory<T>() where T : IFactoryBase;

        /// <summary>
        /// Removes the factory of specified T type if found.
        /// </summary>
        /// <typeparam name="T">A factory that implements IFactoryBase interface.</typeparam>
        void RemoveFactory<T>() where T : IFactoryBase;
    }
}
