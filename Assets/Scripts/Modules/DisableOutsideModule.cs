using UnityEngine;

namespace Entities.Modules
{
    /// <summary>
    /// A module that disables an entity outside the screen.
    /// </summary>
    public class DisableOutsideModule : MonoBehaviour, IDisableOutsideModule
    {
        private GameObject entityGameObject;    // The cached gameObject of the entity.

        /// <summary>
        /// Initializes the module with the passed ModuleHandler.
        /// </summary>
        /// <param name="handler">Entity handler.</param>
        public void Init(IModuleHandler handler) => entityGameObject = handler.GetEntity().GetGameObject();

        /// <summary>
        /// Disables an entity when outside the screen.
        /// </summary>
        private void OnBecameInvisible() => Disable();

        /// <summary>
        /// Disables an entity when outside the screen.
        /// </summary>
        public void Disable() => entityGameObject.SetActive(false);

        /// <summary>
        /// Returns the game object to which the module is attached.
        /// </summary>
        /// <returns>Entity gameObject.</returns>
        public GameObject GetGameObject() => gameObject;
    }
}
