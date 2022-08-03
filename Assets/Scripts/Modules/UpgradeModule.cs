using System.Collections.Generic;
using UnityEngine;

namespace Entities.Modules
{
    /// <summary>
    /// A module that checks for collision with the upgrade bonus and upgrades the entity.
    /// </summary>
    public class UpgradeModule : MonoBehaviour, IUpgradeModule
    {
        [SerializeField] private GameObject[] modulesToActivate;        // GameObjects to upgrade(activate).
        [SerializeField] private ObjectType upgradableObjType;          // Upgrade bonus type.

        private IModuleHandler handler;                                 // Cached module handler.
        private IBlinkModule blinkModule;                               // Cached blink module.
        private ICollisionModule collisionModule;                       // Cached collision module.
        private Color blinkColor = Color.green;                         // Upgrade blink color.

        /// <summary>
        /// Initializes the module with the passed ModuleHandler.
        /// </summary>
        /// <param name="handler">Entity handler.</param>
        public void Init(IModuleHandler handler)
        {
            this.handler = handler;
            blinkModule = handler.GetModule<IBlinkModule>();
            collisionModule = handler.GetModule<ICollisionModule>();
            collisionModule.OnTriggerEnterAction += CheckForUpgradeTriggered;
        }

        /// <summary>
        /// Check if the entity has collided with the upgrade bonus.
        /// </summary>
        /// <param name="obj">Collided entity</param>
        private void CheckForUpgradeTriggered(Entity obj)
        {
            // If collided entity type is upgrade bonus type then upgrade self.
            if (obj.EntityType == upgradableObjType)
            {
                Upgrade();
                // Deactivate collided bonus.
                obj.SetActive(false);
            }
        }

        /// <summary>
        /// Upgrade self.
        /// </summary>
        public void Upgrade()
        {
            bool isUpgraded = false;
            List<IModule> modules = new List<IModule>();
            for (int i = 0; i < modulesToActivate.Length; i++)
            {
                // If objects are deactivated.
                if (!modulesToActivate[i].activeInHierarchy)
                {
                    // Activate the object.
                    modulesToActivate[i].SetActive(true);
                    // Add child modules to the collection.
                    modules.AddRange(modulesToActivate[i].GetComponentsInChildren<IModule>(true));
                    isUpgraded = true;
                }
            }

            // If something has been activated.
            if (isUpgraded)
            {
                // Inits cached modules.
                for (int i = 0; i < modules.Count; i++)
                {
                    if (modules[i] != null)
                        modules[i].Init(handler);
                }
                // Blinks upgrade color.
                blinkModule.Blink(blinkColor);
            }
        }

        /// <summary>
        /// Returns the game object to which the module is attached.
        /// </summary>
        /// <returns>Entity gameObject.</returns>
        public GameObject GetGameObject() => gameObject;
    }
}
