using UnityEngine;

namespace Entities.Modules
{
    /// <summary>
    /// A module that rotates the transform.
    /// </summary>
    public class RotateModule : MonoBehaviour, IRotateModule
    {
        [SerializeField] private Vector3 rotateSpeed;               // Rotation speed.
        [SerializeField] private Transform transformToRotate;       // Rotation transform.

        /// <summary>
        /// Initializes the module with the passed ModuleHandler.
        /// </summary>
        /// <param name="handler">Entity handler.</param>
        public void Init(IModuleHandler handler) { }
        private void Update() => Rotate();

        /// <summary>
        /// Rotates the transform at the specified speed.
        /// </summary>
        public void Rotate() => transformToRotate.Rotate(rotateSpeed * Time.deltaTime);

        /// <summary>
        /// Returns the game object to which the module is attached.
        /// </summary>
        /// <returns>Entity gameObject.</returns>
        public GameObject GetGameObject() => gameObject;
    }
}
