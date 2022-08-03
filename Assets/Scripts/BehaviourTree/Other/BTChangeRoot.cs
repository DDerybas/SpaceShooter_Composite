using UnityEngine;
using Entities.Modules;
using Entities;

namespace BehaviourTree
{
    /// <summary>
    /// A node that changes the root node.
    /// </summary>
    public class BTChangeRoot : BTNode
    {
        [SerializeField] private BTRoot rootNode;               // The root for the transition.
        private IBehaviourTreeModule behaviourTreeModule;       // Entity's behaviour tree module.

        /// <summary>
        /// Initializes the node with a context.
        /// </summary>
        /// <param name="context">Context of the node passed from Entity.</param>
        public override void InitNode(BTContext context)
        {
            base.InitNode(context);
            Entity entity = context.owner;
            behaviourTreeModule = entity.GetHandler().GetModule<IBehaviourTreeModule>();
        }

        /// <summary>
        /// Executes the node with the context.
        /// </summary>
        /// <param name="context">Context of the node passed from Entity.</param>
        /// <returns>The state of the node</returns>
        public override NodeState Execute(BTContext context)
        {
            SetCurrentActiveNode();
            behaviourTreeModule.SetRoot(rootNode);
            _in = NodeState.SUCCESS;
            return _in;
        }

        /// <summary>
        /// Returns the description of the node.
        /// </summary>
        public override string GetDescription() => "<color='#A3EA8A'><b>returns SUCCESS</b></color>";
    }
}
