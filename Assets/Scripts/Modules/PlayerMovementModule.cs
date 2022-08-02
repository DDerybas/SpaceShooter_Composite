using UnityEngine;

namespace Entities.Modules
{
    /// <summary>
    /// Moves the player in the direction it receives from the input module.
    /// </summary>
    public class PlayerMovementModule : MovementModule
    {
        private IModuleHandler handler;                 // A modules handler that processes all the modules it contains.
        private IInputModule inputModule;               // Player input module.

        [SerializeField] private Vector2 moveRange;     // Movement range.

        /// <summary>
        /// Initializes the module with the passed ModuleHandler.
        /// </summary>
        /// <param name="handler">Entity handler.</param>
        public override void Init(IModuleHandler handler)
        {
            base.Init(handler);
            this.handler = handler;
            this.handler.OnInitModulesEnd += OnInitModulesEnd;
        }

        /// <summary>
        /// Gets the input module and subscribes to the input axis event.
        /// </summary>
        private void OnInitModulesEnd()
        {
            inputModule = handler.GetModule<IInputModule>();
            inputModule.OnAxisAction += SetDirection;
        }

        /// <summary>
        /// Moves the entity in the desired direction, with range 'moveRange'.
        /// </summary>
        protected override void MoveDirection()
        {
            base.MoveDirection();
            Vector3 entityPos = entityTransform.position;
            entityPos.x = Mathf.Clamp(entityPos.x, -moveRange.x, moveRange.x);
            entityPos.y = Mathf.Clamp(entityPos.y, -moveRange.y, moveRange.y);
            entityTransform.position = entityPos;
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(Vector2.zero, moveRange * 2);
        }
#endif

        // Unsubscribes from initialization and axis events.
        private void OnDestroy()
        {
            handler.OnInitModulesEnd -= OnInitModulesEnd;
            inputModule.OnAxisAction -= SetDirection;
        }
    }
}
