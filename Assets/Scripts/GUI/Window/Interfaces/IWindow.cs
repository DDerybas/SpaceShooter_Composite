namespace GUI
{
    public interface IWindow
    {
        /// <summary>
        /// Returns the window type.
        /// </summary>
        ObjectType GetWindowType();

        /// <summary>
        /// Activates the window.
        /// </summary>
        void Show();

        /// <summary>
        /// Deactivates the window.
        /// </summary>
        void Hide();
    }
}
