using UnityEngine;
using Entities;
using Entities.Modules;

namespace BehaviourTree.AI
{
    /// <summary>
    /// The node that moves the object to the specified position.
    /// </summary>
    public class BTMoveToPos : BTNode
    {
        [SerializeField] private Vector2 destination;           // Target position.
        [SerializeField] private float speed;                   // Entity speed.
        private IMovementModule movementModule;                 // Entity's movement module.
        private Entity owner;                                   // The entity.

        /// <summary>
        /// Initializes the node with a context.
        /// </summary>
        /// <param name="context">Context of the node passed from Entity.</param>
        public override void InitNode(BTContext context)
        {
            base.InitNode(context);
            owner = context.owner;
            IModuleHandler handler = owner.GetHandler();
            movementModule = handler.GetModule<IMovementModule>();
        }

        /// <summary>
        /// Executes the node with the context.
        /// </summary>
        /// <param name="context">Context of the node passed from Entity.</param>
        /// <returns>The state of the node</returns>
        public override NodeState Execute(BTContext context)
        {
            SetCurrentActiveNode();

            // Sets the movement direction.
            movementModule.SetDirection((destination - owner.GetPosition()).normalized);
            // Sets the movement speed.
            movementModule.SetSpeed(speed);
            _in = NodeState.RUNNING;
            return _in;
        }

        /// <summary>
        /// Returns the description of the node.
        /// </summary>
        public override string GetDescription() =>
            "<b><color='#A3EA8A'>returns RUNNING</color></b>";
    }
}
