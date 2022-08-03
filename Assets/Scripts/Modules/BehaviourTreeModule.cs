using UnityEngine;
using BehaviourTree;

namespace Entities.Modules
{
    /// <summary>
    /// A module that handles the behaviour tree.
    /// </summary>
    public class BehaviourTreeModule : MonoBehaviour, IBehaviourTreeModule
    {
        [SerializeField] private BTGraphBase graph;         // Behaviour tree graph to handle.
        private BTContext context;                          // The context to pass to the behaviour tree.
        private BTRoot rootNode;                            // Behaviour tree root node to start from.

        /// <summary>
        /// Initializes the module with the passed ModuleHandler.
        /// </summary>
        /// <param name="handler">Entity handler.</param>
        public void Init(IModuleHandler handler)
        {
            graph.Init();
            rootNode = graph.GetRootNode();
            context = new BTContext(handler.GetEntity());
            for (int i = 0; i < graph.nodes.Count; i++)
                ((BTNode)graph.nodes[i]).InitNode(context);

            handler.OnModulesReset += OnModuleReset;
        }

        /// <summary>
        /// Set the default root node when resetting the module.
        /// </summary>
        private void OnModuleReset() => rootNode = graph.GetRootNode();

        /// <summary>
        /// Sets the root node.
        /// </summary>
        public void SetRoot(BTRoot root) => rootNode = root;
        private void Update() => Run(rootNode);

        /// <summary>
        /// Executes the root node with the context.
        /// </summary>
        public void Run(BTRoot root) => root.Execute(context);

        /// <summary>
        /// Returns the game object to which the module is attached.
        /// </summary>
        /// <returns>Entity gameObject.</returns>
        public GameObject GetGameObject() => gameObject;
    }
}
