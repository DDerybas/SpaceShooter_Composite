using UnityEngine;
using XNode;
using System.Linq;

namespace BehaviourTree
{ 
    /// <summary>
    /// A node graph(container) class.
    /// </summary>
    [CreateAssetMenu(fileName = "NodeTreeGraph", menuName = "BehaviourTree", order = 0)]
    public class BTGraphBase : NodeGraph
    {
        public BTNode currentActiveNode;        // Current active node.

        /// <summary>
        /// Initializes the graph and sets the current node to the root.
        /// </summary>
        public void Init() => currentActiveNode = GetRootNode();

        /// <summary>
        /// Sets the current active node.
        /// </summary>
        public void SetActiveNode(BTNode bnode)
        {
            if (currentActiveNode != null)
                currentActiveNode.IsCurrentNode = false;

            currentActiveNode = bnode;
            currentActiveNode.IsCurrentNode = true;
        }

        /// <summary>
        /// Returns the default root node.
        /// </summary>
        public BTRoot GetRootNode() => nodes.OfType<BTRoot>().FirstOrDefault(x => x.IsDefaultNode());

        /// <summary>
        /// Resets the graph by setting the current node to the root.
        /// </summary>
        public void ResetGraph() => currentActiveNode = GetRootNode();
    }
}
