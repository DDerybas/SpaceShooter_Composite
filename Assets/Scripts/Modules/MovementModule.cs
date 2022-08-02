using UnityEngine;

namespace Entities.Modules
{
    public class MovementModule : MonoBehaviour, IMovementModule
    {
        protected Transform entityTransform;            // The transform of the movable entity.
        [SerializeField] protected Vector2 direction;   // Direction of movement.
        [SerializeField] private float speed;           // Movement speed.

        /// <summary>
        /// Initializes the module with the passed ModuleHandler.
        /// </summary>
        /// <param name="handler">Entity handler.</param>
        public virtual void Init(IModuleHandler handler) => entityTransform = handler.GetEntity().GetTransform();

        // Moves the entity in the desired direction 
        protected void LateUpdate() => MoveDirection();

        /// <summary>
        /// Sets the direction of movement of the entity.
        /// </summary>
        /// <param name="direction">The direction of movement.</param>
        public void SetDirection(Vector2 direction) => this.direction = direction;

        // Moves the entity in the desired direction
        protected virtual void MoveDirection() => entityTransform.Translate(direction * speed * Time.deltaTime);

        /// <summary>
        /// // Sets the speed of movement of the entity.
        /// </summary>
        /// <param name="speed">Movement speed</param>
        public void SetSpeed(float speed) => this.speed = speed;

        /// <summary>
        /// Returns the game object to which the module is attached.
        /// </summary>
        /// <returns>Entity gameObject.</returns>
        public GameObject GetGameObject() => gameObject;
    }
}
