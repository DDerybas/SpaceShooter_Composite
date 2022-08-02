using UnityEngine;
/// <summary>
/// A base interface of MonoBehaviours factory with a specified type T.
/// </summary>
public interface IMonoFactory<T> : IFactory<T> where T : MonoBehaviour
{
    /// <summary>
    /// Creates the object of type T with specified ObjectType.
    /// </summary>
    /// <returns>Object that derives from MonoBehaviour class.</returns>
    T CreateObject(ObjectType objectType);

    /// <summary>
    /// Creates the object of type T with specified ObjectType and sets its parent.
    /// </summary>
    /// <returns>Object that derives from MonoBehaviour class.</returns>
    T CreateObject(ObjectType objectType, Transform parent);

    /// <summary>
    /// Creates the object of type T with specified ObjectType and place it in position.
    /// </summary>
    /// <returns>Object that derives from MonoBehaviour class.</returns>
    T CreateObject(ObjectType objectType, Vector2 position);

    /// <summary>
    /// Creates the object of type T with specified ObjectType and sets its position and rotation.
    /// </summary>
    /// <returns>Object that derives from MonoBehaviour class.</returns>
    T CreateObject(ObjectType objectType, Vector2 position, Quaternion rotation);
}
