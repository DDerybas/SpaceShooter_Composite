using UnityEngine;

/// <summary>
/// A base interface of MonoBehaviours pool with a specified type T.
/// </summary>
public interface IMonoPool<T> : IPool<T> where T : MonoBehaviour
{
    /// <summary>
    /// Returns a pool object and place it in position specified.
    /// </summary>
    /// <param name="position">Position of the pool object.</param>
    /// <returns>An object that derives from MonoBehaviour class.</returns>
    T GetPoolableObject(Vector2 position);
}
