using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

public sealed class XAssetBundleFileSystem
{
    public static XAssetBundleFileSystem instance { get; internal set; }

    /// <summary>
    /// 
    /// </summary>
    static XAssetBundleFileSystem()
    {
        instance = new XAssetBundleFileSystem();
    }

    /// <summary>
    /// 
    /// </summary>
    public const string assetBasePath   = "assets/resources";

    /// <summary>
    /// 
    /// </summary>
    public const string BundlePrefix    = "bundle";

#if UNITY_EDITOR
    /// <summary>
    /// 
    /// </summary>
    public static string assetBundleOutputPath = System.IO.Path.Combine(System.Environment.CurrentDirectory,
        string.Format("build/bundle/{0}", XResourceIndexSheet.GetPlatformFolder(EditorUserBuildSettings.activeBuildTarget))).Replace("\\", "/");

    /// <summary>
    /// 
    /// </summary>
    public static string dataBasePath = System.IO.Path.Combine(XResourceIndexSheet.resOutputPath, "bundle").Replace("\\", "/");

#else
	public static string dataBasePath = System.IO.Path.Combine(Application.persistentDataPath, BundlePrefix);
#endif

    /// <summary>
    /// 
    /// </summary>
    private static string streamBasePath = System.IO.Path.Combine(Application.streamingAssetsPath, BundlePrefix).Replace("\\", "/");


    /// <summary>
    /// 
    /// </summary>
    private Dictionary<string, AssetBundleInfo> 
        bundles = new Dictionary<string, AssetBundleInfo>();

    /// <summary>
    /// 
    /// </summary>
    private List<AssetBundleInfo> bundleInfos = new List<AssetBundleInfo>();

    /// <summary>
    /// 
    /// </summary>
    public void     Initialize()
    {
        foreach (KeyValuePair<string, AssetBundleInfo> item in bundles)
        {
            AssetBundleInfo info = item.Value;
            if (info.isDone)
                info.bundle.Unload(false);
        }
        bundles.Clear();
        bundleInfos.Clear();

        foreach (KeyValuePair<string, XResPack> item in XResourceIndexSheet.instance.GetPacks())
        {
            AssetBundleInfo info = new AssetBundleInfo(item.Value);
            bundles.Add(item.Key, info);
            bundleInfos.Add(info);
        }

        Resources.UnloadUnusedAssets();
    }

    /// <summary>
    /// 
    /// </summary>
    public void     Update()
    {
        ReleaseBundle(CacheType.None);
    }

    /// <summary>
    /// 
    /// </summary>
    public void     ReleaseSceneCachedBundleOnSceneSwitch()
    {
        ReleaseBundle(CacheType.InScene);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cacheType"></param>
    private void    ReleaseBundle(CacheType cacheType)
    {
        float now = Time.time;
        for (int i = 0; i < bundleInfos.Count; i++)
        {
            AssetBundleInfo info = bundleInfos[i];
            if (info.isDone && info.Unused(now))
            {
                if (info.pack.cacheType != cacheType && info.pack.cacheType != CacheType.None)
                    continue;
#if !RELEASE
                if (info.Ref() < 0)
                    Debug.LogError(string.Format("AssetManager.Unload bundle:{0} refCount({1}) incorrect!", info.pack.name, info.Ref()));
#endif

                info.ResetRef();

                for (int depIndex = 0; depIndex < info.pack.dependencies.Length; depIndex++)
                {
                    string dependence = info.pack.dependencies[depIndex];
                    AssetBundleInfo dependenceInfo = GetAssetBundleInfo(dependence);
                    if (dependenceInfo != null)
                        dependenceInfo.RemoveDepended(info.pack.name);
                }
            }
        }

        for (int i = 0; i < bundleInfos.Count; i++)
        {
            AssetBundleInfo info = bundleInfos[i];
            if (info.isDone && info.Unused(now))
            {
                if (info.pack.cacheType != cacheType && info.pack.cacheType != CacheType.None)
                    continue;
                if (info.DependedCount() == 0)
                {
                    info.bundle.Unload(false);
                    info.bundle = null;
                    info.isDone = false;
                }
            }
        }
    }

    public AssetBundleInfo GetAssetBundleInfo(string bundleName)
    {
        AssetBundleInfo loaded = null;
        bundles.TryGetValue(bundleName, out loaded);
        return loaded;
    }

    private IEnumerator LoadDependenciesAsync(XResPack pack)
    {
        for (int i = 0; i < pack.dependencies.Length; i++)
        {
            AssetBundleInfo bundleInfo = GetAssetBundleInfo(pack.dependencies[i]);
            if (bundleInfo != null)
            {
                if (!bundleInfo.isDone)
                    XCoroutine.Run(LoadAssetBundleAsync(bundleInfo, false));

                bundleInfo.AddDepended(pack.name);
            }
        }
        for (int i = 0; i < pack.dependencies.Length; i++)
        {
            AssetBundleInfo bundleInfo = GetAssetBundleInfo(pack.dependencies[i]);
            if (bundleInfo != null)
            {
                while (bundleInfo.isLoading)
                    yield return null;
            }
        }
    }

    private IEnumerator LoadAssetBundleAsync(AssetBundleInfo bundleInfo, bool loadDependence = true)
    {
        while (bundleInfo.isLoading)
            yield return null;

        if (bundleInfo.isDone)
            yield break;

        bundleInfo.isLoading = true;
        if (loadDependence)
            yield return XCoroutine.Run(LoadDependenciesAsync(bundleInfo.pack));

        string bundleName = bundleInfo.pack.name;
        AssetBundleCreateRequest loader = AssetBundle.LoadFromFileAsync(bundleInfo.url); // new WWW(bundleInfo.url);
        yield return loader;

        if (!loader.isDone)
        {
            bundleInfo.isLoading = false;
        }
        else
        {
            bundleInfo.isLoading = false;
            if (loader.assetBundle != null)
            {
                bundleInfo.bundle = loader.assetBundle;
                bundleInfo.isDone = true;
            }
        }
    }



    public Coroutine LoadAssetAsync(XAssetInfo info, System.Type type, System.Action<Object> callback)
    {
        return XCoroutine.Run(DoLoadAssetAsync(info, type, callback));
    }

    public IEnumerator DoLoadAssetAsync(XAssetInfo info, System.Type type, System.Action<Object> callback)
    {
        AssetBundleInfo bundleInfo = GetAssetBundleInfo(info.bundle);
        bool needLoad = false;
        if (!bundleInfo.isDone)
        {
            needLoad = true;
            yield return XCoroutine.Run(LoadAssetBundleAsync(bundleInfo));
        }

        if (bundleInfo.isDone)
        {
            bundleInfo.IncRef(Time.time);

            AssetBundleRequest request = null;
            Object obj = null;
            if (type == typeof(BufferAsset))
            {
                request = bundleInfo.bundle.LoadAssetAsync<TextAsset>(System.IO.Path.Combine(assetBasePath, info.fullName));
                yield return request;
                BufferAsset asset = ScriptableObject.CreateInstance<BufferAsset>();
                asset.init((TextAsset)request.asset);
                obj = asset;
            }
            else
            {
                request = bundleInfo.bundle.LoadAssetAsync(System.IO.Path.Combine(assetBasePath, info.fullName), type);
                yield return request;
                obj = request.asset;
            }

            bundleInfo.DecRef();

            callback(obj);
        }
    }

    public Coroutine LoadSceneAsync(XResScene info)
    {
        return XCoroutine.Run(DoLoadSceneAsync(info));
    }

    private IEnumerator DoLoadSceneAsync(XResScene info)
    {
        AssetBundleInfo bundleInfo = GetAssetBundleInfo(info.bundle);
        bool needLoad = false;
        if (!bundleInfo.isDone)
        {
            needLoad = true;
            yield return XCoroutine.Run(LoadAssetBundleAsync(bundleInfo));
        }

        if (bundleInfo.isDone)
        {
            bundleInfo.IncRef(Time.time);

            yield return SceneManager.LoadSceneAsync(info.name);

            bundleInfo.DecRef();
        }
    }

    public class AssetBundleInfo
    {
        public XResPack pack;
        public AssetBundle bundle;
        public bool isDone;
        public bool isLoading;
        private int refCount;
        private float lastReadTime;

        private HashSet<string> depended = new HashSet<string>();

        public AssetBundleInfo(XResPack info)
        {
            pack = info;
            bundle = null;
            isDone = false;
            isLoading = false;
            refCount = 0;
            lastReadTime = 0;
        }

        public int Ref() { return refCount; }
        public void IncRef(float time)
        {
            refCount++;
            lastReadTime = time;
        }
        public void DecRef() { refCount--; }
        public bool Unused(float time)
        {
            return refCount <= 0;
        }
        public void ResetRef()
        {
            refCount = 0;
            lastReadTime = 0;
        }

        public int DependedCount() { return depended.Count; }
        public void AddDepended(string bundleName)
        {
            depended.Add(bundleName);
            lastReadTime = Time.time;
        }

        public void RemoveDepended(string bundleName)
        {
            depended.Remove(bundleName);
        }

        public string path
        {
            get
            {
                return System.IO.Path.Combine(XAssetBundleFileSystem.dataBasePath, pack.name);
            }
        }

        public string url
        {
            get
            {
                switch (pack.location)
                {
                    case Location.Data:
                        {
                            return System.IO.Path.Combine(XAssetBundleFileSystem.dataBasePath, pack.name);
                        }

                    case Location.Streaming:
                        {
                            switch (Application.platform)
                            {
                                case RuntimePlatform.Android:
#if EXTERNAL_BUNDLE
                                    return System.IO.Path.Combine(ProcessPackRes.OutCachePath, pack.name);
#else
                                    return System.IO.Path.Combine(XAssetBundleFileSystem.streamBasePath, pack.name);
#endif
                                default:
                                    return System.IO.Path.Combine(XAssetBundleFileSystem.streamBasePath, pack.name);
                            }
                        }

                    default:
                        return "";
                }
            }
        }

    }
}

