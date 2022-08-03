using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Managers;
using System;

/// <summary>
/// A base MonoBehaviour abstract factory class that loads and creates objects of specified type.
/// </summary>
public abstract class MonoFactory<T> : MonoBehaviour, IMonoFactory<T> where T : MonoBehaviour
{
    protected List<T> objList = new List<T>();                                                  // The collection of loaded objects.
    private List<AssetReference> assetsLoadRequestList = new List<AssetReference>();            // The collection of asset-requests to load.
    private bool allAssetsLoaded;                                                               // Are all assets were loaded.

    /// <summary>
    /// When all objects are loaded.
    /// </summary>
    public Action OnLoadingAssetsComplete { get; set; }
    
    /// <summary>
    /// Inits the factory with the global manager and subscribes to it init end action.
    /// </summary>
    public void Init(IGlobalManager manager) => manager.OnInitEndAction += ManagerOnInitEnd;

    /// <summary>
    /// After the initialization of the global manager is complete, the resource loading process begins.
    /// </summary>
    private void ManagerOnInitEnd() => StartCoroutine(LoadAllAssetsCoroutine());

    /// <summary>
    /// Adds the asset reference to the list of loading requests.
    /// </summary>
    public void AddAssetToFactory(AssetReference asset) => assetsLoadRequestList.Add(asset);

    /// <summary>
    /// Loads assets using Addressables system.
    /// </summary>
    private IEnumerator LoadAllAssetsCoroutine()
    {
        for (int i = 0; i < assetsLoadRequestList.Count; i++)
        {
            // Loads the asset from asset-requests list.
            AsyncOperationHandle<GameObject> operation = Addressables.LoadAssetAsync<GameObject>(assetsLoadRequestList[i]);
            // Yields until the operation is done.
            yield return operation;

            // If operation status is succeeded - adds the component from the loaded GameObject to the collection.
            if (operation.Status == AsyncOperationStatus.Succeeded)
                objList.Add(operation.Result.GetComponent<T>());
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            else Debug.LogError($"Can't load asset {assetsLoadRequestList[i]}");
#endif
        }

#if UNITY_EDITOR || DEVELOPMENT_BUILD
        Debug.Log($"Factory '{gameObject.name}' is loaded and initialized!");
#endif
        allAssetsLoaded = true;
        assetsLoadRequestList.Clear();
        // Infokes assets loading complete event.
        OnLoadingAssetsComplete?.Invoke();
    }

    /// <summary>
    /// Creates the object of type T.
    /// </summary>
    /// <returns>Object of type T.</returns>
    public T CreateObject() => CreateObject(null);

    /// <summary>
    /// Creates the object of type T with specified ObjectType.
    /// </summary>
    /// <returns>Object that derives from MonoBehaviour class.</returns>
    public abstract T CreateObject(ObjectType objectType);

    /// <summary>
    /// Creates the object of type T with specified ObjectType and sets its parent.
    /// </summary>
    /// <returns>Object that derives from MonoBehaviour class.</returns>
    public T CreateObject(ObjectType objectType, Transform parent)
    {
        T obj = CreateObject(objectType, Vector3.zero, Quaternion.identity);
        obj.transform.parent = parent;
        return obj;
    }

    /// <summary>
    /// Creates the object of type T with specified ObjectType and place it in position.
    /// </summary>
    /// <returns>Object that derives from MonoBehaviour class.</returns>
    public T CreateObject(ObjectType objectType, Vector2 position)
    {
        T obj = CreateObject(objectType);
        obj.transform.position = position;
        return obj;
    }

    /// <summary>
    /// Creates the object of type T with specified ObjectType and sets its position and rotation.
    /// </summary>
    /// <returns>Object that derives from MonoBehaviour class.</returns>
    public T CreateObject(ObjectType objectType, Vector2 position, Quaternion rotation)
    {
        T obj = CreateObject(objectType);
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        return obj;
    }

    /// <summary>
    /// Is factory ready?
    /// </summary>
    public bool IsReady() => allAssetsLoaded;
}
