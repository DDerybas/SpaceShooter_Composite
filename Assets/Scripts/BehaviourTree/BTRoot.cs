using UnityEngine;

namespace BehaviourTree
{
    /// <summary>
    /// The root node of the behaviour tree.
    /// </summary>
    public class BTRoot : BTNode
    {
        [Output] public NodeState _out;                 // Out node state.
        [SerializeField] private bool isDefault;        // Is default(start) root.

        /// <summary>
        /// Executes the node with the context.
        /// </summary>
        /// <param name="context">Context of the node passed from Entity.</param>
        /// <returns>The state of the node</returns>
        public override NodeState Execute(BTContext context)
        {
            SetCurrentActiveNode();
            (outPort.GetConnection(0).node as BTNode).Execute(context);

            return NodeState.FAIL;
        }

        /// <summary>
        /// Check if the node is default(start) root.
        /// </summary>
        public bool IsDefaultNode() => isDefault;

        /// <summary>
        /// Returns the description of the node.
        /// </summary>
        public override string GetDescription() => "<color='#00A2F9'><b>Start node</b></color>";
    }
}
