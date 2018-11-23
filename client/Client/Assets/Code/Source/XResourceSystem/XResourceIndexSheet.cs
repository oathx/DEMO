using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class XResIndexSheetInfo
{
    public List<XResPack>   packs 
    { get; set; }
    
    public List<XResFile>   files 
    { get; set; }
    
    public List<XResScene>  scenes 
    { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static XResIndexSheetInfo Parse(string data)
    {
        return LitJson.JsonMapper.ToObject<XResIndexSheetInfo>(data);
    }

    /// <summary>
    /// 
    /// </summary>
    public void Init()
    {
        packs   = new List<XResPack>();
        files   = new List<XResFile>();
        scenes  = new List<XResScene>();
    }
}

public class XAssetInfo
{
    public Location location 
    { get; set; }

    public string   bundle 
    { get; set; }

    public string   fullName 
    { get; set; }
}

/// <summary>
/// 
/// </summary>
public class XEntryDiff
{
    public EntryType type = EntryType.None;

    /// <summary>
    /// 
    /// </summary>
    public XResEntry local;

    /// <summary>
    /// 
    /// </summary>
    public XResEntry remote;
}

/// <summary>
/// resource index sheet
/// </summary>
public sealed class XResourceIndexSheet
{
    /// <summary>
    /// 
    /// </summary>
    public static XResourceIndexSheet instance { get; internal set; }

    /// <summary>
    /// 
    /// </summary>
    static XResourceIndexSheet()
    {
        instance = new XResourceIndexSheet();
    }

    public const string name = "xris";

    /// <summary>
    /// 
    /// </summary>
    private Dictionary<string, XResPack> 
        xPacks = new Dictionary<string, XResPack>();

    private Dictionary<string, XResFile> 
        xFiles = new Dictionary<string, XResFile>();

    private Dictionary<string, XResScene> 
        xScenes = new Dictionary<string, XResScene>();

    /// <summary>
    /// 
    /// </summary>
    private Dictionary<string, XAssetInfo> xFastIndexs = new Dictionary<string, XAssetInfo>();

#if UNITY_EDITOR
    public static string GetPlatformFolder(BuildTarget target)
    {
        switch (target)
        {
            case BuildTarget.Android:
                return "android";
            case BuildTarget.iOS:
                return "ios";
            case BuildTarget.StandaloneWindows:
            case BuildTarget.StandaloneWindows64:
                return "windows";
            case BuildTarget.StandaloneOSXIntel:
            case BuildTarget.StandaloneOSXIntel64:
            case BuildTarget.StandaloneOSXUniversal:
                return "osx";
            default:
                return null;
        }
    }

    public static string resOutputPath = System.IO.Path.Combine(System.Environment.CurrentDirectory,
        string.Format("build/bundle/{0}", XResourceIndexSheet.GetPlatformFolder(EditorUserBuildSettings.activeBuildTarget))).Replace("\\", "/");

    public static string fatOutputPath = System.IO.Path.Combine(resOutputPath, name).Replace("\\", "/");

    public static string updateResOutputPath = System.IO.Path.Combine(System.Environment.CurrentDirectory,
            string.Format("build/res/{0}", XResourceIndexSheet.GetPlatformFolder(EditorUserBuildSettings.activeBuildTarget))).Replace("\\", "/");

    public static string updateRenameResOutputPath = System.IO.Path.Combine(System.Environment.CurrentDirectory,
        string.Format("build/update/{0}", XResourceIndexSheet.GetPlatformFolder(EditorUserBuildSettings.activeBuildTarget))).Replace("\\", "/");
#endif

    public static string GetPlatformFolder(RuntimePlatform platform)
    {
        switch (platform)
        {
            case RuntimePlatform.Android:
                return "android";
            case RuntimePlatform.IPhonePlayer:
                return "ios";
            case RuntimePlatform.WindowsPlayer:
                return "windows";
            case RuntimePlatform.OSXPlayer:
                return "osx";
            default:
                return null;
        }
    }

    public static string    GetPlatformFolder()
    {
#if UNITY_EDITOR
        return GetPlatformFolder(EditorUserBuildSettings.activeBuildTarget);
#else
        return GetPlatformFolder(Application.platform);
#endif
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public XAssetInfo       Find(string name)
    {
        XAssetInfo index = null;
#if UNITY_EDITOR
        if (isDependentRis)
        {
            return index;
        }
#endif
        xFastIndexs.TryGetValue(name, out index);

        return index;
    }

#if UNITY_EDITOR
    private string risPath = string.Empty;

    /// <summary>
    /// 
    /// </summary>
    public bool             isInit = false;
    public bool             isDependentRis = false;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    public void             InitializeFat(string path)
    {
        risPath = path;
        isInit = true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public Dictionary<string, 
        XAssetInfo> GetFastIndex()
    {
        return xFastIndexs;
    }
#endif

    /// <summary>
    /// 
    /// </summary>
    public bool Initialize()
    {
        Clear();

        string data = string.Empty;
#if UNITY_STANDALONE
        string extPath = System.IO.Path.Combine(System.IO.Path.Combine(Application.streamingAssetsPath, XAssetBundleFileSystem.BundlePrefix), name);
#else
		string extPath = System.IO.Path.Combine(Application.persistentDataPath,name);
#endif
#if UNITY_EDITOR
        if (isInit)
        {
            if (!System.IO.File.Exists(extPath))
            {
                extPath = System.IO.Path.Combine(risPath, name);
                isDependentRis = true;
            }
        }
#endif
        if (System.IO.File.Exists(extPath))
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(extPath);
            data = sr.ReadToEnd();
            sr.Close();

            if (string.IsNullOrEmpty(data))
            {
                System.IO.File.Delete(extPath);
            }
        }

        if (string.IsNullOrEmpty(data))
        {
            TextAsset text = Resources.Load<TextAsset>(name);
            if (text != null)
            {
                data = text.text;
            }
        }

        if (string.IsNullOrEmpty(data))
        {
            return false;
        }

        XResIndexSheetInfo info = XResIndexSheetInfo.Parse(data);
        if (info != null)
        {
            for (int i = 0; i < info.files.Count; i++)
                Add(info.files[i]);

            for (int i = 0; i < info.packs.Count; i++)
                Add(info.packs[i]);

            for (int i = 0; i < info.scenes.Count; i++)
                Add(info.scenes[i]);
        }
        else
        {
            Debug.LogError("Parse RIS info failed");
        }

        return true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entry"></param>
    public void Remove(XResEntry entry)
    {
        switch (entry.Type())
        {
            case EntryType.Pack:
                xPacks.Remove(entry.name);
                break;
            case EntryType.File:
                xFiles.Remove(entry.name);
                break;
            case EntryType.Scene:
                xScenes.Remove(entry.name);
                break;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entry"></param>
    public void Add(XResEntry entry)
    {
        switch (entry.Type())
        {
            case EntryType.Pack:
                xPacks.Add(entry.name, entry as XResPack);
                break;
            case EntryType.File:
                xFiles.Add(entry.name, entry as XResFile);
                break;
            case EntryType.Scene:
                xScenes.Add(entry.name, entry as XResScene);
                break;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pack"></param>
    public void Add(XResPack pack)
    {
        xPacks.Add(pack.name, pack);

        Location local = pack.NoBundle() ? Location.Resource : Location.Bundle;
        foreach (string file in pack.files)
        {
            AddFastIndex(file, new XAssetInfo() { 
                location = local, bundle = pack.name, fullName = file 
            });
        }        
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="file"></param>
    public void Add(XResFile file)
    {
        xFiles.Add(file.name, file);

        AddFastIndex(file.name, new XAssetInfo() { 
            location = file.location, fullName = file.name 
        });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="scene"></param>
    public void Add(XResScene scene)
    {
        xScenes.Add(scene.name, scene);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="index"></param>
    private void AddFastIndex(string name, XAssetInfo index)
    {
        string key = name;
        if (System.IO.Path.HasExtension(name))
            key = System.IO.Path.ChangeExtension(name, null);

        xFastIndexs[key] = index;
    }

    /// <summary>
    /// 
    /// </summary>
    public void Clear()
    {
        xPacks.Clear();
        xFiles.Clear();
        xScenes.Clear();
        xFastIndexs.Clear();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public XResPack GetPack(string name)
    {
        XResPack pack = null;
        xPacks.TryGetValue(name, out pack);

        return pack;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public XResFile GetFile(string name)
    {
        XResFile file = null;
        xFiles.TryGetValue(name, out file);

        return file;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public XResScene GetScene(string name)
    {
        XResScene scene = null;
        xScenes.TryGetValue(name, out scene);

        return scene;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public IEnumerable GetPacks()
    {
        return xPacks;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public IEnumerable GetScenes()
    {
        return xScenes;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public IEnumerable GetFiles()
    {
        return xFiles;
    }

#if UNITY_EDITOR
    public bool Exists(string fullpath)
    {
        string key = fullpath;
        if (System.IO.Path.HasExtension(fullpath))
            key = System.IO.Path.ChangeExtension(fullpath, null);

        return xFastIndexs.ContainsKey(key);
    }
#endif

    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <param name="pretty"></param>
    public void Save(string path, bool pretty = true)
    {
        Save(path, string.Empty, pretty);
    }

    public void Save(string path, string extraPackagePath, bool pretty = true)
    {
        if (string.IsNullOrEmpty(path))
            path = System.IO.Path.Combine(Application.persistentDataPath, name);

        List<XResPack> packs = new List<XResPack>();
        foreach (XResPack pack in xPacks.Values)
        {
            packs.Add(pack);
        }

        List<XResFile> files = new List<XResFile>();
        foreach (XResFile file in xFiles.Values)
        {
            files.Add(file);
        }

        List<XResScene> scenes = new List<XResScene>();
        foreach (XResScene scene in xScenes.Values)
        {
            scenes.Add(scene);
        }

#if UNITY_EDITOR

        #region CACHE
        XBundleExtraCache cacheInfo = null;
        List<string> cacheInScene = null;
        List<string> cacheAllTime = null;

        string infoText = XBundleExtraUtil.GetBundleExtraCacheInfoText();
        if (!string.IsNullOrEmpty(infoText))
        {
            cacheInfo = LitJson.JsonMapper.ToObject<XBundleExtraCache>(infoText);
            if (cacheInfo != null)
            {
                cacheInScene = XBundleExtraUtil.ArrayToList<string>(cacheInfo.CacheInScene);
                if (cacheInScene == null)
                    cacheInScene = new List<string>();
                cacheAllTime = XBundleExtraUtil.ArrayToList<string>(cacheInfo.CacheAllTime);
                if (cacheAllTime == null)
                    cacheAllTime = new List<string>();

                foreach (XResPack pack in packs)
                {
                    SetupBundleCacheType(pack, cacheInScene, cacheAllTime);
                }

                foreach (XResScene scene in scenes)
                {
                    SetupBundleCacheType(scene, cacheInScene, cacheAllTime);
                }
            }
        }

        #endregion      //CACHE


        #region PACKAGE
        if (!string.IsNullOrEmpty(extraPackagePath))
        {
            XBundleExtraPack packageInfo = null;
            List<string> package1 = new List<string>();
            List<string> package2 = new List<string>();
            List<string> package3 = new List<string>();
            string packageInfoText = XBundleExtraUtil.GetBundleExtraText(extraPackagePath);
            if (!string.IsNullOrEmpty(packageInfoText))
            {
                packageInfo = LitJson.JsonMapper.ToObject<XBundleExtraPack>(packageInfoText);
                if (packageInfo != null)
                {
                    package1 = XBundleExtraUtil.ArrayToList<string>(packageInfo.PackgesCategory1);
                    package2 = XBundleExtraUtil.ArrayToList<string>(packageInfo.PackgesCategory2);
                    package3 = XBundleExtraUtil.ArrayToList<string>(packageInfo.PackgesCategory3);

                    foreach (XResPack pack in packs)
                    {
                        SetupBundlePackageType(pack, package1, package2, package3);
                    }

                    foreach (XResScene scene in scenes)
                    {
                        SetupBundlePackageType(scene, package1, package2, package3);
                    }
                }
            }
        }

        #endregion          //PACKAGE

#endif

        XResIndexSheetInfo fat = new XResIndexSheetInfo();
        fat.packs = packs;
        fat.files = files;
        fat.scenes = scenes;

        System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Create, System.IO.FileAccess.Write);
        if (fs != null)
        {
            StringBuilder sb = new StringBuilder();
            System.IO.StringWriter writer = new System.IO.StringWriter(sb);
            LitJson.JsonMapper.ToJson(fat,
                                      new LitJson.JsonWriter(writer)
                                      {
                                          PrettyPrint = pretty
                                      });

            byte[] buff = Encoding.UTF8.GetBytes(sb.ToString());
            fs.Write(buff, 0, buff.Length);
            fs.Close();
        }

    }

    private void SetupBundleCacheType(XResEntry entry, 
        List<string> cacheInScene, List<string> cacheAllTime)
    {
        if (cacheInScene.Contains(entry.name))
        {
            entry.cacheType = CacheType.InScene;
        }
        else if (cacheAllTime.Contains(entry.name))
        {
            entry.cacheType = CacheType.AllTime;
        }
    }


    private void SetupBundlePackageType(XResEntry entry, 
        List<string> packageCatrgory1, List<string> packageCatrgory2, List<string> packageCatrgory3)
    {
        if (packageCatrgory1.Contains(entry.name))
        {
            entry.packageType = PackageType.Package1;
        }
        else if (packageCatrgory2.Contains(entry.name))
        {
            entry.packageType = PackageType.Package2;
        }
        else if (packageCatrgory3.Contains(entry.name))
        {
            entry.packageType = PackageType.Package3;
        }
    }


    public void DeleteLocal()
    {
        string extPath = System.IO.Path.Combine(Application.persistentDataPath, name);

        string cachePath = System.IO.Path.Combine(XProcessPack.OutCachePath, name);
        if (System.IO.File.Exists(extPath))
            System.IO.File.Delete(extPath);

        if (System.IO.File.Exists(cachePath))
            System.IO.File.Delete(cachePath);
    }

    public Queue<XEntryDiff> GenerateFatDiff(XResIndexSheetInfo newFat)
    {
        Queue<XEntryDiff> diffFat = new Queue<XEntryDiff>();

        // pack diff
        for (int i = 0; i < newFat.packs.Count; i++)
        {
            bool diff = true;
            XResPack pack = newFat.packs[i];
            XResPack localPack;
            if (xPacks.TryGetValue(pack.name, out localPack))
            {
                if (localPack.checksum == pack.checksum)
                    diff = false;
            }

            if (diff)
                diffFat.Enqueue(new XEntryDiff() { type = EntryType.File, local = localPack, remote = pack });
        }

        // file diff
        for (int i = 0; i < newFat.files.Count; i++)
        {
            bool diff = true;
            XResFile file = newFat.files[i];
            XResFile localFile;
            if (xFiles.TryGetValue(file.name, out localFile))
            {
                if (localFile.checksum == file.checksum)
                    diff = false;
            }

            if (diff)
                diffFat.Enqueue(new XEntryDiff() { type = EntryType.Pack, local = localFile, remote = file });
        }

        // scene diff
        for (int i = 0; i < newFat.scenes.Count; i++)
        {
            bool diff = true;
            XResScene scene = newFat.scenes[i];
            XResScene localScene;
            if (xScenes.TryGetValue(scene.name, out localScene))
            {
                if (localScene.bundle == scene.bundle)
                    diff = false;
            }

            if (diff)
                diffFat.Enqueue(new XEntryDiff() { type = EntryType.Scene, local = localScene, remote = scene });
        }

        return diffFat;
    }

    public static string GetLocalFileUrl(string path)
    {
        switch (Application.platform)
        {
            case RuntimePlatform.WindowsEditor:
            case RuntimePlatform.WindowsPlayer:
                return "file:///" + path;
            default:
                return "file://" + path;
        }
    }
}
