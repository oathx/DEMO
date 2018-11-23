using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using SLua;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CustomLuaClass]
public static class XRes
{
	private static AsyncOperation cleanupTask = null;

	/// <summary>
	/// Starts the coroutine.
	/// </summary>
	/// <returns>The coroutine.</returns>
	/// <param name="em">Em.</param>
    private static Coroutine StartCoroutine(IEnumerator em)
    {
        return XCoroutine.Run(em);
    }

	/// <summary>
	/// Find the specified path.
	/// </summary>
	/// <param name="path">Path.</param>
    private static Object Find(string path)
    {
        return null;
    }

	/// <summary>
	/// Load the specified path.
	/// </summary>
	/// <param name="path">Path.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
    [DoNotToLua]
    public static T Load<T>(string path) where T : Object
    {
        return (Load(path, typeof(T)) as T);
    }

	/// <summary>
	/// Load the specified path and type.
	/// </summary>
	/// <param name="path">Path.</param>
	/// <param name="type">Type.</param>
    [DoNotToLua]
    public static Object Load(string path, System.Type type)
    {
        string name = path.ToLower();
        if (string.IsNullOrEmpty(path))
        {
            GLog.LogError("[XRes::Load] sent empty/null path!");
            return null;
        }

        UnityEngine.Object obj = Find(name);

#if UNITY_EDITOR
        AssetBundleDependence.instance.BeginRecord(name);
#endif

        if (obj == null)
        {
            XAssetInfo info = XResourceIndexSheet.instance.Find(name);
            if (info != null)
            {
                switch (info.location)
                {
                    case Location.Resource:
                        {
                            if (type == typeof(BufferAsset))
                            {
                                BufferAsset asset = ScriptableObject.CreateInstance<BufferAsset>();
                                asset.init(Resources.Load<TextAsset>(name));
                                obj = asset;
                            }
                            else
                            {
                                obj = Resources.Load(name, type);
                            }
                        }
                        break;

                    case Location.Bundle:
                        {
                            GLog.LogError("[XRes] can't load bundle in sync load {0}", name);
                        }
                        break;

                    case Location.Data:
                        {
                            string filePath = System.IO.Path.Combine(Application.persistentDataPath, info.fullName);
                            if (System.IO.File.Exists(filePath))
                            {
                                System.IO.FileStream fs = System.IO.File.OpenRead(filePath);
                                BufferAsset asset = ScriptableObject.CreateInstance<BufferAsset>();
                                asset.init((int)fs.Length);
                                fs.Read(asset.bytes, 0, (int)fs.Length);
                                fs.Close();

                                obj = asset;

                                if (type == typeof(Texture2D))
                                {
                                    Texture2D texture = new Texture2D(1, 1);
                                    texture.LoadImage(asset.bytes);
                                    obj = texture;
                                }
                            }
                        }
                        break;
                }
            }
            else
            {
                if (type == typeof(BufferAsset))
                {
                    BufferAsset asset = ScriptableObject.CreateInstance<BufferAsset>();
                    asset.init(Resources.Load<TextAsset>(name));
                    obj = asset;
                }
                else
                    obj = Resources.Load(name, type);
            }

            if (obj == null)
            {
                if (info != null)
                    GLog.LogError("[XRes] Can't find {0} in Location({1})", name, info.location);
                else
                    GLog.LogError("[XRes] Can't find {0} in Resources", name);
            }
            else
            {
                GLog.Log("[XRes] ({0} : {1}) Loaded.", name, type.ToString());
            }
        }

        return obj;
    }

	/// <summary>
	/// Loads the async.
	/// </summary>
	/// <returns>The async.</returns>
	/// <param name="path">Path.</param>
	/// <param name="callback">Callback.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
    public static Coroutine LoadAsync<T>(string path, System.Action<Object> callback) where T : Object
    {
        return LoadAsync(path, typeof(T), callback);
    }

	/// <summary>
	/// Loads the async.
	/// </summary>
	/// <returns>The async.</returns>
	/// <param name="path">Path.</param>
	/// <param name="type">Type.</param>
	/// <param name="callback">Callback.</param>
    public static Coroutine LoadAsync(string path, System.Type type, System.Action<Object> callback)
    {
        return StartCoroutine(DoLoadAsync(path, type, callback));
    }

	/// <summary>
	/// Dos the load async.
	/// </summary>
	/// <returns>The load async.</returns>
	/// <param name="path">Path.</param>
	/// <param name="type">Type.</param>
	/// <param name="callback">Callback.</param>
    private static IEnumerator DoLoadAsync(string path, System.Type type, System.Action<Object> callback)
    {
        if (string.IsNullOrEmpty(path))
        {
            GLog.LogError("[XRes::Load] sent empty/null path!");
            yield break;
        }

        string name = path.ToLower();
        UnityEngine.Object obj = Find(name);
        if (obj == null)
        {
            XAssetInfo info = XResourceIndexSheet.instance.Find(name);
#if UNITY_EDITOR
            StartCoroutine(AssetBundleDependence.instance.BeginRecordAsync(name));
#endif
            if (info != null)
            {
                switch (info.location)
                {
                    case Location.Resource:
                        {
                            ResourceRequest request = null;
                            if (type == typeof(BufferAsset))
                            {
                                request = Resources.LoadAsync<TextAsset>(name);
                                yield return request;
                                BufferAsset asset = ScriptableObject.CreateInstance<BufferAsset>();
                                asset.init((TextAsset)request.asset);
                                obj = asset;
                            }
                            else
                            {
                                request = Resources.LoadAsync(name, type);
                                yield return request;
                                obj = request.asset;
                            }
                        }
                        break;

                    case Location.Bundle:
                        {
                            yield return XAssetBundleFileSystem.instance.LoadAssetAsync(info, type, delegate(Object result) { obj = result; });
                        }
                        break;

                    case Location.Data:
                        {
                            string fileUrl = XResourceIndexSheet.GetLocalFileUrl(System.IO.Path.Combine(Application.persistentDataPath, info.fullName));
                            WWW loader = new WWW(fileUrl);
                            yield return loader;

                            if (!string.IsNullOrEmpty(loader.error))
                            {
                                BufferAsset asset = ScriptableObject.CreateInstance<BufferAsset>();
                                asset.init(loader.bytes);
                                obj = asset;

                                if (type == typeof(Texture2D))
                                {
                                    Texture2D texture = new Texture2D(1, 1);
                                    texture.LoadImage(asset.bytes);
                                    obj = texture;
                                }
                            }
                        }
                        break;

                    case Location.Streaming:
                        {
                            string fileUrl = XResourceIndexSheet.GetLocalFileUrl(System.IO.Path.Combine(Application.streamingAssetsPath, info.fullName));
                            if (Application.platform != RuntimePlatform.Android)
                                fileUrl = XResourceIndexSheet.GetLocalFileUrl(fileUrl);

                            WWW loader = new WWW(fileUrl);
                            yield return loader;

                            if (!string.IsNullOrEmpty(loader.error))
                            {
                                BufferAsset asset = ScriptableObject.CreateInstance<BufferAsset>();
                                asset.init(loader.bytes);
                                obj = asset;

                                if (type == typeof(Texture2D))
                                {
                                    Texture2D texture = new Texture2D(1, 1);
                                    texture.LoadImage(asset.bytes);
                                    obj = texture;
                                }
                            }
                        }
                        break;
                }

            }
            else
            {
                ResourceRequest request = Resources.LoadAsync(name, type);
                yield return request;
                obj = request.asset;
            }

            if (obj == null)
            {
                if (info != null)
                    GLog.LogError("[XRes] Can't find {0} in Location({1})", name, info.location);
                else
                    GLog.LogError("[XRes] Can't find {0} in Resources", name);
            }
        }
        else
        {
            callback(obj);
        }

        callback(obj);
    }

	/// <summary>
	/// Loads the async.
	/// </summary>
	/// <returns>The async.</returns>
	/// <param name="path">Path.</param>
	/// <param name="type">Type.</param>
	/// <param name="callback">Callback.</param>
	/// <param name="param">Parameter.</param>
    private static Coroutine LoadAsync(string path, System.Type type, System.Action<Object, int> callback, int param)
    {
        return StartCoroutine(DoLoadAsync(path, type, delegate(Object obj)
        {
            callback(obj, param);
        }));
    }

	/// <summary>
	/// Loads the multi async.
	/// </summary>
	/// <returns>The multi async.</returns>
	/// <param name="paths">Paths.</param>
	/// <param name="callback">Callback.</param>
    public static Coroutine LoadMultiAsync(string[] paths, System.Action<Object[]> callback)
    {
        return StartCoroutine(DoLoadMultiAsync(paths, callback));
    }

	/// <summary>
	/// Dos the load multi async.
	/// </summary>
	/// <returns>The load multi async.</returns>
	/// <param name="paths">Paths.</param>
	/// <param name="callback">Callback.</param>
    private static IEnumerator DoLoadMultiAsync(string[] paths, System.Action<Object[]> callback)
    {
        Object[] results = new Object[paths.Length];
        bool[] loadDone = new bool[paths.Length];
        for (int i = 0; i < paths.Length; i++)
        {
            loadDone[i] = false;
            LoadAsync(paths[i], typeof(Object), delegate(Object obj, int index)
            {
                results[index] = obj;
                loadDone[index] = true;
            }, i);
        }

        for (int i = 0; i < paths.Length; i++)
            while (!loadDone[i])
                yield return null;

        callback(results);
    }

	/// <summary>
	/// Loads the scene async.
	/// </summary>
	/// <returns>The scene async.</returns>
	/// <param name="name">Name.</param>
	/// <param name="callback">Callback.</param>
    public static Coroutine LoadSceneAsync(string name, System.Action<string> callback)
    {
        XAssetBundleFileSystem.instance.ReleaseSceneCachedBundleOnSceneSwitch();
#if RECORD_FPS
        FPSDisplay.Instance.SwitchWithScene(name);
#endif

        return XCoroutine.Run(DoLoadSceneAsync(name, callback));
    }

	/// <summary>
	/// Dos the load scene async.
	/// </summary>
	/// <returns>The load scene async.</returns>
	/// <param name="name">Name.</param>
	/// <param name="callback">Callback.</param>
    private static IEnumerator DoLoadSceneAsync(string name, System.Action<string> callback)
    {
        yield return null;

        GLog.Log("XRes.LoadSceneAsync loading: {0}", name);

        XResScene info = XResourceIndexSheet.instance.GetScene(name);
#if UNITY_EDITOR
        if (info != null)
            StartCoroutine(AssetBundleDependence.instance.LoadSceneAsync(info));

        if (XResourceIndexSheet.instance.isInit && XResourceIndexSheet.instance.isDependentRis)
        {
            info = null;
        }
#endif

        if (info != null)
        {
            GLog.Log("XRes.LoadSceneAsync bundle loading: {0}", name);
            XResPack packInfo = XResourceIndexSheet.instance.GetPack(info.bundle);
            switch (packInfo.location)
            {
                case Location.Resource:
                    GLog.LogError("XRes.LoadSceneAsync can't load bundled scene {0} in resource!", name);
                    break;
                default:
                    yield return XAssetBundleFileSystem.instance.LoadSceneAsync(info);
                    break;
            }
        }
        else
        {
            GLog.Log("XRes.LoadSceneAsync native loading: {0}", name);
            yield return SceneManager.LoadSceneAsync(name);
        }

        callback(name);
    }

	/// <summary>
	/// Unload the specified path.
	/// </summary>
	/// <param name="path">Path.</param>
    public static bool Unload(string path)
    {
        return XAssetCache.instance.Unload(path.ToLower());
    }

	/// <summary>
	/// Unload the specified asset.
	/// </summary>
	/// <param name="asset">Asset.</param>
    public static bool Unload(Object asset)
    {
        return XAssetCache.instance.Unload(asset);
    }

	/// <summary>
	/// Unloads the unused assets.
	/// </summary>
	/// <returns>The unused assets.</returns>
    private static AsyncOperation UnloadUnusedAssets()
    {
        if ((cleanupTask == null) || cleanupTask.isDone)
        {
            GLog.Log("[XRes] Running cleanup task");
            cleanupTask = Resources.UnloadUnusedAssets();
        }
        return cleanupTask;
    }

	/// <summary>
	/// Flushs all and unload.
	/// </summary>
	/// <returns>The all and unload.</returns>
    [DoNotToLua]
    public static AsyncOperation FlushAllAndUnload()
    {
        XAssetCache.instance.ClearLoaded();
        return UnloadUnusedAssets();
    }

	/// <summary>
	/// Initialize this instance.
	/// </summary>
    [DoNotToLua]
    public static IEnumerator Initialize()
    {
        yield return Initialize(null);
    }

	/// <summary>
	/// Initialize the specified progressCallback.
	/// </summary>
	/// <param name="progressCallback">Progress callback.</param>
    [DoNotToLua]
    public static IEnumerator Initialize(System.Action<XResUpdater.UpdateStage, float, string> progressCallback)
    {
        XResourceIndexSheet.instance.Initialize();
        yield return null;
        XAssetBundleFileSystem.instance.Initialize();
        yield return null;
#if UNITY_ANDROID && !UNITY_EDITOR && EXTERNAL_BUNDLE
        if (ProcessPackRes.Instance.CheckFatOut())
        {   
            MsdkAgent.instance.ReportEvent("j4zrxc");//CheckDataIntegrity
            yield return ProcessPackRes.Instance.CheckDataIntegrity(delegate(Updater.UpdateStage stage, float progress) {
                progressCallback(stage, progress, string.Empty);
            });
        }
        else
        {   
            MsdkAgent.instance.ReportEvent("y94sm8");//ExtractPack
            yield return ProcessPackRes.Instance.MoveZipResOutOfPack(delegate(Updater.UpdateStage stage, float progress) {
                 progressCallback(stage, progress, string.Empty);
            }); 
        }
        MsdkAgent.instance.ReportEvent("g9jopw");//ExtractComplete
        yield return null;
#endif
    }

	/// <summary>
	/// Update this instance.
	/// </summary>
    [DoNotToLua]
    public static void Update()
    {
        XAssetBundleFileSystem.instance.Update();
    }
}
