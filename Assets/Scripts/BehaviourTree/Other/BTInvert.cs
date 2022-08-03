using System.Collections.Generic;

namespace BehaviourTree
{
    /// <summary>
    /// A node that inverts the input state.
    /// </summary>
    public class BTInvert : BTNode
    {
        [Output] public NodeState _out;         // Out node state.

        /// <summary>
        /// Executes the node with the context.
        /// </summary>
        /// <param name="context">Context of the node passed from Entity.</param>
        /// <returns>The state of the node</returns>
        public override NodeState Execute(BTContext context)
        {
            SetCurrentActiveNode();
            _in = NodeState.FAIL;

            NodeState state = (connections[0].node as BTNode).Execute(context);

            // Returns inverted state.
            switch (state)
            {
                case NodeState.RUNNING:
                    _in = NodeState.FAIL;
                    return _in;
                case NodeState.FAIL:
                    _in = NodeState.SUCCESS;
                    return _in;
                case NodeState.SUCCESS:
                    _in = NodeState.FAIL;
                    return _in;
            }

            return _in;
        }

        /// <summary>
        /// Returns the description of the node.
        /// </summary>
        public override string GetDescription() => "<color='#A3EA8A'><b>returns inverted state</b></color>";
    }
}
