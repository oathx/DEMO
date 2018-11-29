using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
public sealed class XAssetCache
{
    /// <summary>
    /// 
    /// </summary>
    public enum AssetSource
    {
        Resources, Bundle
    }

    /// <summary>
    /// 
    /// </summary>
    public class Info
    {
        public UnityEngine.Object   asset;
        public string               path;
        public int                  refCount;
        public AssetSource          source;
    }

    /// <summary>
    /// 
    /// </summary>
    public static XAssetCache instance 
    { get; internal set; }


    /// <summary>
    /// 
    /// </summary>
    static XAssetCache()
    {
        instance = new XAssetCache();
    }

    /// <summary>
    /// 
    /// </summary>
    private Dictionary<string, Info> 
        loadedAssets = new Dictionary<string, Info>();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <param name="asset"></param>
    /// <param name="source"></param>
    public void                 Add(string path, UnityEngine.Object asset, AssetSource source)
    {
        Info loadedAsset = GetLoadedAsset(path);
        if (loadedAsset == null)
        {
            loadedAsset = new Info {
                path = path, asset = asset, source = source
            };

            loadedAssets[path] = loadedAsset;
        }

        loadedAsset.refCount++;
    }

    /// <summary>
    /// 
    /// </summary>
    public void                 ClearLoaded()
    {
        loadedAssets.Clear();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public UnityEngine.Object   Find(string path)
    {
        Info loadedAsset = GetLoadedAsset(path);
        if (loadedAsset != null)
        {
            loadedAsset.refCount++;
            return loadedAsset.asset;
        }
        return null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    private Info                GetLoadedAsset(string path)
    {
        Info info = null;
        loadedAssets.TryGetValue(path, out info);
        return info;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Info>    GetLoadedAssets()
    {
        return loadedAssets.Values;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public int                  Size()
    {
        return loadedAssets.Count;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="info"></param>
    private void                Unload(Info info)
    {
        if (info.asset != null)
        {
            GLog.Log("[AssetCache] Unloading asset {0}", info.path);
            switch (info.source)
            {
                case AssetSource.Resources:
                    if ((!typeof(GameObject).IsInstanceOfType(info.asset) && !typeof(UnityEngine.Component).IsInstanceOfType(info.asset)) && !typeof(AssetBundle).IsInstanceOfType(info.asset))
                    {
                        Resources.UnloadAsset(info.asset);
                    }
                    break;
            }
            info.asset = null;
        }
        loadedAssets.Remove(info.path);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public bool                 Unload(string name)
    {
        Info loadedAsset = null;
        loadedAsset = GetLoadedAsset(name);
        if (loadedAsset != null)
        {
            loadedAsset.refCount--;
            if (loadedAsset.refCount == 0)
            {
                Unload(loadedAsset);
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="asset"></param>
    /// <returns></returns>
    public bool                 Unload(UnityEngine.Object asset)
    {
        Info info = null;
        foreach (Info info2 in loadedAssets.Values)
        {
            if (info2.asset == asset)
            {
                info = info2;
                break;
            }
        }

        if (info != null)
        {
            info.refCount--;
            if (info.refCount == 0)
            {
                Unload(info);
                return true;
            }
        }

        return false;
    }

}