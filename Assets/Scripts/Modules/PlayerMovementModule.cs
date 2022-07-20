using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Modules
{
    /// <summary>
    /// Moves the player in the direction it receives from the input module.
    /// </summary>
    public class PlayerMovementModule : MonoBehaviour, IMovementModule
    {
        #region IModule
        public List<IModule> BindedModules { get; set; }
        #endregion

        private IModuleHandler handler;             // A modules handler that processes all the modules it contains.
        private IInputModule inputModule;           // Player input module.
        private Vector2 direction;                  // Direction of movement.
        private Transform entityTransform;          // The transform of the movable entity.
        [SerializeField] private float speed;       // Movement speed.

        public void Init(IModuleHandler handler)
        {
            this.handler = handler;
            BindedModules = new List<IModule>();
            this.handler.OnInitModulesEnd += OnInitModulesEnd;
            entityTransform = this.handler.GetEntity().transform;
        }

        // Gets the input module and subscribes to the input axis event.
        void OnInitModulesEnd()
        {
            inputModule = handler.RetrieveModule<IInputModule>(this);
            inputModule.OnAxisAction += SetDirection;
        }

        // Causes the direction of movement of the entity.
        private void Update()
        {
            MoveDirection();
        }

        /// <summary>
        /// Sets the desired direction of movement.
        /// </summary>
        /// <param name="direction">Direction of movement.</param>
        public void SetDirection(Vector2 direction)
        {
            this.direction = direction;
        }

        // Moves the entity in the desired direction.
        void MoveDirection()
        {
            entityTransform.Translate(direction.x * speed * Time.deltaTime, 0, 0);
        }

        // Returns the game object to which the module is attached.
        public GameObject GetGameObject()
        {
            return gameObject;
        }

        // Unsubscribes from initialization and axis events.
        private void OnDestroy()
        {
            handler.OnInitModulesEnd -= OnInitModulesEnd;
            inputModule.OnAxisAction -= SetDirection;
        }
    }
}
