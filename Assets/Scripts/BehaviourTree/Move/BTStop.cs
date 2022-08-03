using Entities.Modules;

namespace BehaviourTree.AI
{
    /// <summary>
    /// A node that stops the entity movement.
    /// </summary>
    public class BTStop : BTNode
    {
        private IMovementModule movementModule;         // Entity's movement module.

        /// <summary>
        /// Initializes the node with a context.
        /// </summary>
        /// <param name="context">Context of the node passed from Entity.</param>
        public override void InitNode(BTContext context)
        {
            base.InitNode(context);
            IModuleHandler handler = context.owner.GetHandler();
            movementModule = handler.GetModule<IMovementModule>();
        }

        /// <summary>
        /// Executes the node with the context. Stops an entity.
        /// </summary>
        /// <param name="context">Context of the node passed from Entity.</param>
        /// <returns>The state of the node</returns>
        public override NodeState Execute(BTContext context)
        {
            SetCurrentActiveNode();
            // Sets the movement speed to 0.
            movementModule.SetSpeed(0);
            _in = NodeState.SUCCESS;
            return _in;
        }

        /// <summary>
        /// Returns the description of the node.
        /// </summary>
        public override string GetDescription() =>
            "<b><color='#A3EA8A'>returns SUCCESS</color></b>";
    }
}
