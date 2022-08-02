using UnityEngine.AddressableAssets;
using System;

/// <summary>
/// An abstract factory interface that loads and creates objects of specified type.
/// </summary>
public interface IFactory<T> : IFactoryBase where T : class
{
    /// <summary>
    /// Is factory ready?
    /// </summary>
    bool IsReady();

    /// <summary>
    /// Adds the asset reference to the list of loading requests.
    /// </summary>
    void AddAssetToFactory(AssetReference asset);

    /// <summary>
    /// Creates the object of type T.
    /// </summary>
    /// <returns>Object of type T.</returns>
    T CreateObject();

    /// <summary>
    /// When all objects are loaded.
    /// </summary>
    Action OnLoadingAssetsComplete { get; set; }
}
