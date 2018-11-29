using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;
using System.Text;

/// <summary>
/// Asset dependence.
/// </summary>
public sealed class XAssetDependence
{
    /// <summary>
    /// 
    /// </summary>
    public static XAssetDependence instance 
	{ get; internal set; }

	/// <summary>
	/// Initializes the <see cref="AssetBundleDependence"/> class.
	/// </summary>
    static XAssetDependence()
    {
        instance = new XAssetDependence();
    }

#if UNITY_EDITOR
    public Dictionary<string, int> 
        bundleNames = new Dictionary<string, int>();
    
	/// <summary>
	/// The is record.
	/// </summary>
    private bool isRecord = false;
    private XAssetBundleFileSystem.AssetBundleInfo bundleInfo;
    private float bundleSize = 0;
 
    /// <summary>
    /// 
    /// </summary>
    public void 		            BegainRecordBundleName()
    {
        bundleNames.Clear();
        isRecord = true;
    }

	/// <summary>
	/// Sets the record flag.
	/// </summary>
	/// <param name="flag">If set to <c>true</c> flag.</param>
    public void 		            SetRecordFlag(bool flag = false)
    {
        isRecord = flag;
    }

	/// <summary>
	/// Gets the size of the bundle.
	/// </summary>
	/// <returns>The bundle size.</returns>
    public float 		            GetBundleSize()
    {
        return bundleSize;
    }

    /// <summary>
    /// 
    /// </summary>
    public void 		            ClearBundleSize()
    {
        bundleSize = 0;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public Dictionary<string, int>  GetBundleName()
    {
        return bundleNames;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    public void 		            BeginRecord(string name)
    {
        if (XResourceIndexSheet.instance.isDependentRis)
        {
            XAssetInfo index = null;
            XResourceIndexSheet.instance.GetFastIndex().TryGetValue(name, out index);

            if (index.location == Location.Bundle)
                LoadBundle(index);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public IEnumerator 	            BeginRecordAsync(string name)
    {
        if (XResourceIndexSheet.instance.isDependentRis)
        {
            Dictionary<string, XAssetInfo> 
                dict = XResourceIndexSheet.instance.GetFastIndex();

            XAssetInfo index = null;
            if (dict.TryGetValue(name, out index)) 
            {
                if (index.location == Location.Bundle)
                {
                    yield return XCoroutine.Run(LoadBundleAsync(index));
                }
                else
                {
                    yield return null;   
                }   
            }
        }

        yield return null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="info"></param>
    public void 		            LoadBundle(XAssetInfo info)
    {
        bundleInfo = LoadAssetBundle(info.bundle);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pack"></param>
    private void 		            LoadDependencies(XResPack pack)
    {
        foreach (string dependence in pack.dependencies)
        {
            XAssetBundleFileSystem.AssetBundleInfo bundleInfo = LoadAssetBundle(dependence);
            if (bundleInfo != null && bundleInfo.isDone)
                bundleInfo.AddDepended(pack.name);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="bundleName"></param>
    /// <returns></returns>
    private XAssetBundleFileSystem.AssetBundleInfo 
        LoadAssetBundle(string bundleName)
    {
        XAssetBundleFileSystem.AssetBundleInfo bundleInfo = XAssetBundleFileSystem.instance.GetAssetBundleInfo(bundleName);
        if (bundleInfo == null)
            return null;

        if (bundleInfo.isLoading)
        {
            return null;
        }

        if (isRecord)
        {
            if (!bundleNames.ContainsKey(bundleInfo.pack.name))
                bundleNames[bundleInfo.pack.name] = 1;
            else
                bundleNames[bundleInfo.pack.name] = 1 + bundleNames[bundleInfo.pack.name];
        }

        bundleSize += bundleInfo.pack.size;
        LoadDependencies(bundleInfo.pack);

        return bundleInfo;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="info"></param>
    /// <returns></returns>
    public IEnumerator LoadBundleAsync(XAssetInfo info)
    {
        XAssetBundleFileSystem.AssetBundleInfo bundleInfo = XAssetBundleFileSystem.instance.GetAssetBundleInfo(info.bundle);
        bool needLoad = false;
        if (!bundleInfo.isDone)
        {
            needLoad = true;

            yield return XCoroutine.Run(
				LoadAssetBundleAsyncCustom(bundleInfo)
			);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="bundleInfo"></param>
    /// <param name="loadDependence"></param>
    /// <returns></returns>
    private IEnumerator LoadAssetBundleAsyncCustom(XAssetBundleFileSystem.AssetBundleInfo bundleInfo, bool loadDependence = true)
    {
        if (isRecord)
        {
            if (!bundleNames.ContainsKey(bundleInfo.pack.name))
            {
                bundleNames[bundleInfo.pack.name] = 1;
            }
            else
            {
                int count = bundleNames[bundleInfo.pack.name];
                count += 1;
                bundleNames[bundleInfo.pack.name] = count;
            }
        }

        while (bundleInfo.isLoading)
            yield return null;

        if (bundleInfo.isDone)
        {
            if (isRecord)
            {
                if (!bundleNames.ContainsKey(bundleInfo.pack.name))
                    bundleNames[bundleInfo.pack.name] = 1;
                else
                    bundleNames[bundleInfo.pack.name] = 1 + bundleNames[bundleInfo.pack.name];
            }
        }

        if (bundleInfo.isDone)
            yield break;

        bundleSize += bundleInfo.pack.size;

        bundleInfo.isLoading = true;
        if (loadDependence)
            yield return XCoroutine.Run(LoadDependenciesAsyncCustom(bundleInfo.pack));

        yield return null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pack"></param>
    /// <returns></returns>
    private IEnumerator LoadDependenciesAsyncCustom(XResPack pack)
    {
        for (int i = 0; i < pack.dependencies.Length; i++)
        {
            XAssetBundleFileSystem.AssetBundleInfo bundleInfo = XAssetBundleFileSystem.instance.GetAssetBundleInfo(pack.dependencies[i]);
            if (bundleInfo != null)
            {
                if (!bundleInfo.isDone)
                    XCoroutine.Run(LoadAssetBundleAsyncCustom(bundleInfo, false));
                bundleInfo.AddDepended(pack.name);
            }
        }

        for (int i = 0; i < pack.dependencies.Length; i++)
        {
            XAssetBundleFileSystem.AssetBundleInfo bundleInfo = XAssetBundleFileSystem.instance.GetAssetBundleInfo(pack.dependencies[i]);
            if (bundleInfo != null)
            {
                while (bundleInfo.isLoading)
                    yield return null;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="info"></param>
    /// <returns></returns>
    public IEnumerator LoadSceneAsync(XResScene info)
    {
        if (info != null)
        {
            XAssetBundleFileSystem.AssetBundleInfo bundleInfo = XAssetBundleFileSystem.instance.GetAssetBundleInfo(info.bundle);
            if (bundleInfo != null)
                yield return XCoroutine.Run(LoadAssetBundleAsyncCustom(bundleInfo));
            else
                yield return null;
        }
        else 
            yield return null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="datas"></param>
    /// <param name="path"></param>
    /// <param name="onComplete"></param>
    public void WriteToFile(object datas, string path, System.Action onComplete = null)
    {
        System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Create, System.IO.FileAccess.Write);
        if (fs != null)
        {
            StringBuilder sb = new StringBuilder();
            System.IO.StringWriter writer = new System.IO.StringWriter(sb);
            LitJson.JsonMapper.ToJson(datas,
                                          new LitJson.JsonWriter(writer)
                                          {
                                              PrettyPrint = true
                                          });

            byte[] buff = Encoding.UTF8.GetBytes(sb.ToString());
            fs.Write(buff, 0, buff.Length);
            fs.Close();
        }

        if (onComplete != null)
            onComplete();
    }
#endif
}
