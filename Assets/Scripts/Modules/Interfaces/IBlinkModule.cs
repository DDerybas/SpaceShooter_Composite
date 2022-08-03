using UnityEngine;

namespace Entities.Modules
{
    /// <summary>
    /// A module that highlights an entity with color.
    /// </summary>
    public interface IBlinkModule : IModule
    {
        /// <summary>
        /// Blinks the sprite with a color.
        /// </summary>
        /// <param name="color"></param>
        void Blink(Color color);
    }
}
