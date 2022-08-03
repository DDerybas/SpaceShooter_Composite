using System.Collections.Generic;

namespace BehaviourTree
{
    /// <summary>
    /// A node that handles the output node result.
    /// </summary>
    public class BTSelector : BTNode
    {
        [Output] public NodeState _out;     // Out node state.

        /// <summary>
        /// Executes the node with the context. Handles the output node result.
        /// </summary>
        /// <param name="context">Context of the node passed from Entity.</param>
        /// <returns>The state of the node</returns>
        public override NodeState Execute(BTContext context)
        {
            SetCurrentActiveNode();

            for (int i = 0; i < connections.Count; i++)
            {
                NodeState state = (connections[i].node as BTNode).Execute(context);

                switch (state)
                {
                    case NodeState.RUNNING:
                        _in = NodeState.RUNNING;
                        return _in;
                    case NodeState.SUCCESS:
                        _in = NodeState.SUCCESS;
                        return _in;
                    case NodeState.FAIL:
                        continue;
                }
            }

            _in = NodeState.FAIL;
            return _in;
        }

        /// <summary>
        /// Returns the description of the node.
        /// </summary>
        public override string GetDescription() =>
            "<b><color='#A3EA8A'>continues on FAIL</color>\n" +
            "<color='#FFE4E4'>returns on RUNNING</color>\n" +
            "<color='#FFE4E4'>returns on SUCCESS</color></b>";
    }
}
