using UnityEngine;
using Entities;

namespace BehaviourTree.AI
{
    /// <summary>
    /// A node that checks if an entity is at the specified position.
    /// </summary>
    public class BTCheckPosition : BTNode
    {
        [SerializeField] private Vector2 pos;           // Position to check.
        private Entity owner;                           // Entity to check.
        private float distanceAccuracy = .2f;           // How accurate the position check should be.

        /// <summary>
        /// Initializes the node with a context.
        /// </summary>
        /// <param name="context">Context of the node passed from Entity.</param>
        public override void InitNode(BTContext context)
        {
            base.InitNode(context);
            owner = context.owner;
        }

        /// <summary>
        /// Executes the node with the context.
        /// </summary>
        /// <param name="context">Context of the node passed from Entity.</param>
        /// <returns>The state of the node</returns>
        public override NodeState Execute(BTContext context)
        {
            SetCurrentActiveNode();

            _in = (Vector2.Distance(owner.GetPosition(), pos) <= distanceAccuracy) ?
                NodeState.SUCCESS : NodeState.FAIL;

            return _in;
        }

        /// <summary>
        /// Returns the description of the node.
        /// </summary>
        public override string GetDescription() =>
            "<b><color='#A3EA8A'>returns SUCCESS if at position</color>\n" +
            "<color='#FFE4E4'>returns FAIL if not in position</color></b>";
    }
}
