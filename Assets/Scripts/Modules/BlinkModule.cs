using System.Collections;
using UnityEngine;

namespace Entities.Modules
{
    /// <summary>
    /// A module that highlights an entity with color.
    /// </summary>
    public class BlinkModule : MonoBehaviour, IBlinkModule
    {
        private IHitModule hitModule;                               // Entity's hit module.

        private Coroutine blinkCoroutine;                           // Blink(flash) coroutine.
        [SerializeField] private SpriteRenderer blinkSprite;        // A sprite to apply color.
        [SerializeField] private float blinkSpeed;                  // Blink(flash) speed.

        /// <summary>
        /// Initializes the module with the passed ModuleHandler.
        /// </summary>
        /// <param name="handler">Entity handler.</param>
        public void Init(IModuleHandler handler)
        {
            hitModule = handler.GetModule<IHitModule>();
            hitModule.OnHitAction += BlinkHit;
        }
        
        /// <summary>
        /// Blinks the sprite on hit.
        /// </summary>
        private void BlinkHit() => Blink(Color.red);

        /// <summary>
        /// Blinks the sprite with a color.
        /// </summary>
        /// <param name="color"></param>
        public void Blink(Color color)
        {
            if (!gameObject.activeInHierarchy)
                return;

            if (blinkCoroutine != null)
                StopCoroutine(blinkCoroutine);

            blinkCoroutine = StartCoroutine(BlinkCoroutine(color));
        }

        /// <summary>
        /// Changes the color of the sprite from white to the specified color and back. 
        /// </summary>
        private IEnumerator BlinkCoroutine(Color color)
        {
            yield return StartCoroutine(LerpColor(Color.white, color));
            yield return StartCoroutine(LerpColor(color, Color.white));

            blinkSprite.color = Color.white;
        }

        /// <summary>
        /// Smoothly changes color from - to.
        /// </summary>
        private IEnumerator LerpColor(Color from, Color to)
        {
            blinkSprite.color = from;
            float time = 0;
            while(time < 1)
            {
                blinkSprite.color = Color.Lerp(from, to, time);
                time += Time.deltaTime * blinkSpeed;
                yield return null;
            }
        }

        /// <summary>
        /// Returns the game object to which the module is attached.
        /// </summary>
        /// <returns>Entity gameObject.</returns>
        public GameObject GetGameObject() => gameObject;
    }
}
