using Entities.Modules;
using Entities;

namespace BehaviourTree.AI
{
    /// <summary>
    /// Node that causes all entity weapon modules to fire.
    /// </summary>
    public class BTShoot : BTNode
    {
        private IWeaponModule[] weaponModules;      // Entity weapon modules.
        private int weaponsCount;                   // Cached weapon modules count.

        /// <summary>
        /// Initializes the node with a context.
        /// </summary>
        /// <param name="context">Context of the node passed from Entity.</param>
        public override void InitNode(BTContext context)
        {
            base.InitNode(context);
            Entity entity = context.owner;
            weaponModules = entity.GetHandler().GetAllModulesOfType<IWeaponModule>();
            weaponsCount = weaponModules.Length;
        }

        /// <summary>
        /// Executes the node with the context. Calls the shoot method on all weapon modules.
        /// </summary>
        /// <param name="context">Context of the node passed from Entity.</param>
        /// <returns>The state of the node</returns>
        public override NodeState Execute(BTContext context)
        {
            SetCurrentActiveNode();
            Shoot();
            _in = NodeState.SUCCESS;
            return _in;
        }

        /// <summary>
        /// Calls the shoot method on all weapon modules.
        /// </summary>
        void Shoot()
        {
            for (int i = 0; i < weaponsCount; i++)
                weaponModules[i].Shoot();
        }

        /// <summary>
        /// Returns the description of the node.
        /// </summary>
        public override string GetDescription() => "<color='#A3EA8A'><b>returns SUCCESS</b></color>";
    }
}
