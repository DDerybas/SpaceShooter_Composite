using System.Collections.Generic;
using UnityEngine;
using Managers;
using UnityEngine.AddressableAssets;

/// <summary>
/// A base class of MonoBehaviours pool with a specified type T.
/// </summary>
public class MonoPool<T> : MonoBehaviour, IMonoPool<T> where T : MonoBehaviour
{
    [SerializeField] private int startObjectsCount = 5;         // The number of objects to create at startup.
    
    [SerializeField] protected AssetReference baseObject;       // A base asset from which to create a pool.
    private List<T> pooledObjects = new List<T>();              // Pool object collection.
    protected IFactory<T> factory;                              // A factory of the same type from which an asset can be obtained.

    [SerializeField] private ObjectType _poolType;              // A type of the pool.
    public ObjectType PoolType { get => _poolType; set => _poolType = value; }
    private bool isReady;                                       // Is pool initialized and ready.

    /// <summary>
    /// Inits a pool with the global manager.
    /// </summary>
    /// <param name="manager">A manager that implemets IGlobalManager interface.</param>
    public virtual void Init(IGlobalManager manager)
    {
        IFactoryManager fManager = manager.GetManager<IFactoryManager>();
        // Gets a factory of the same type.
        factory = fManager.GetFactory<IFactory<T>>();
        // Adds an asset to the factory.
        factory.AddAssetToFactory(baseObject);
        // Subscribes to the factory loading complete event.
        factory.OnLoadingAssetsComplete += OnFactoryLoadingAssetsComplete;
    }

    /// <summary>
    /// Expand the pool after loading the asset in the factory.
    /// </summary>
    private void OnFactoryLoadingAssetsComplete()
    {
        for (int i = 0; i < startObjectsCount; i++)
            ExtendPool();

#if UNITY_EDITOR || DEVELOPMENT_BUILD
        Debug.Log($"Pool '{gameObject.name}' is ready!");
#endif
        isReady = true;
    }

    /// <summary>
    /// Returns a pool object.
    /// </summary>
    /// <returns>An object that derives from MonoBehaviour class.</returns>
    public T GetPoolableObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].gameObject.activeInHierarchy)
            {
                pooledObjects[i].gameObject.SetActive(true);
                return pooledObjects[i];
            }
        }

        return ExtendPool(true);
    }

    /// <summary>
    /// Returns a pool object and place it in position specified.
    /// </summary>
    /// <param name="position">Position of the pool object.</param>
    /// <returns>An object that derives from MonoBehaviour class.</returns>
    public T GetPoolableObject(Vector2 position)
    {
        T obj = GetPoolableObject();
        obj.transform.position = position;
        return obj;
    }

    /// <summary>
    /// Extends the pool and returns the object.
    /// </summary>
    /// <param name="activateObj">Whether the object should be activated?</param>
    /// <returns>An object that derives from MonoBehaviour class.</returns>
    public T ExtendPool(bool activateObj = false)
    {
        T obj = factory.CreateObject(PoolType, transform);
        obj.gameObject.SetActive(activateObj);
        pooledObjects.Add(obj);
        return obj;
    }

    /// <summary>
    /// Is pool ready.
    /// </summary>
    public bool IsReady() => isReady;
}
