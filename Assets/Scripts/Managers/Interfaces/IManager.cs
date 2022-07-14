public interface IManager
{
    /// <summary>
    /// Initializes the manager.
    /// </summary>
    void Init(IManager manager);

    /// <summary>
    /// Calls after manager initialization.
    /// </summary>
    void OnInitEnd();
}
