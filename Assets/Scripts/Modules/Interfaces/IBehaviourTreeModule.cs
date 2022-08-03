using BehaviourTree;

namespace Entities.Modules
{
    /// <summary>
    /// A module that handles the behaviour tree.
    /// </summary>
    public interface IBehaviourTreeModule : IModule
    {
        /// <summary>
        /// Executes the root node with the context.
        /// </summary>
        void Run(BTRoot root);

        /// <summary>
        /// Sets the root node.
        /// </summary>
        void SetRoot(BTRoot root);
    }
}