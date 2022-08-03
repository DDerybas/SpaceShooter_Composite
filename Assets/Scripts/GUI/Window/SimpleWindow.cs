using UnityEngine;

namespace GUI
{
    /// <summary>
    /// A simple window that shows some info.
    /// </summary>
    public class SimpleWindow : MonoBehaviour, IWindow
    {
        [SerializeField] ObjectType windowType;     // Window type.

        /// <summary>
        /// Returns the window type.
        /// </summary>
        public ObjectType GetWindowType() => windowType;

        /// <summary>
        /// Activates the window.
        /// </summary>
        public void Show() => gameObject.SetActive(true);

        /// <summary>
        /// Deactivates the window.
        /// </summary>
        public void Hide() => gameObject.SetActive(false);
    }
}
