using UnityEngine;
using System;

namespace Entities.Modules
{
    /// <summary>
    /// Player input button type.
    /// </summary>
    public enum ButtonType
    {
        Action,
        Cancel,
        Pause
    }

    /// <summary>
    /// A module that handles the player inputs.
    /// </summary>
    public interface IInputModule : IModule
    {
        /// <summary>
        /// On axis action. Passes the input axis as Vector2.
        /// </summary>
        Action<Vector2> OnAxisAction { get; set; }

        /// <summary>
        /// On pressed/released button action. Passes the button type (ButtonType) and the 'pressed/released' condition (bool).
        /// </summary>
        Action<ButtonType, bool> OnButtonAction { get; set; }
    }
}
