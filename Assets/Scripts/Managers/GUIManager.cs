using UnityEngine;
using GUI;
using System.Linq;

namespace Managers
{
    /// <summary>
    /// A manager that handles all game gui.
    /// </summary>
    public class GUIManager : MonoBehaviour, IGUIManager
    {
        private IWindow[] windowCollection;         // The collection of windows.

        /// <summary>
        /// Initializes the manager.
        /// </summary>
        /// <param name="manager">A manager that implements the IGlobalManager interface.</param>
        public void Init(IGlobalManager manager) => 
            windowCollection = GetComponentsInChildren<IWindow>(true);

        /// <summary>
        /// Activates the window of specified type if found.
        /// </summary>
        /// <param name="windowType">The type of the window.</param>
        public void ShowWindow(ObjectType windowType)
        {
            IWindow window = FindWindowWithType(windowType);
            if (window != null)
                window.Show();
        }

        /// <summary>
        /// Deactivates the window of specified type if found.
        /// </summary>
        /// <param name="windowType">The type of the window.</param>
        public void HideWindow(ObjectType windowType)
        {
            IWindow window = FindWindowWithType(windowType);
            if (window != null)
                window.Hide();
        }

        /// <summary>
        /// Searches for a window of the specified type.
        /// </summary>
        IWindow FindWindowWithType(ObjectType windowType) =>
            windowCollection.FirstOrDefault(x => x.GetWindowType() == windowType);

        /// <summary>
        /// Deactivate all windows.
        /// </summary>
        public void HideAllWindows()
        {
            for (int i = 0; i < windowCollection.Length; i++)
                windowCollection[i].Hide();
        }
    }
}
