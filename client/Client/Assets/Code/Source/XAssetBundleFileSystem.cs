using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// 
/// </summary>
public class XAssetBundleFileSystem
{
    /// <summary>
    /// 
    /// </summary>
    public static XAssetBundleFileSystem instance { 
        get; set; 
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static XAssetBundleFileSystem GetSingleton(){
        if (instance == null){
            instance = new XAssetBundleFileSystem();
        }

        return instance;
    }

	public const string AssetBasePath   = "assets/resources";
    public const string BundlePrefix    = "bundle";
    public const string BuildBundle     = "build/bundle";

#if UNITY_EDITOR
    public static string assetBundleOutputPath = System.IO.Path.Combine(System.Environment.CurrentDirectory,
        string.Format("{0}/{1}", BuildBundle, GetPlatformFolder(EditorUserBuildSettings.activeBuildTarget))).Replace("\\", "/");
#else
	public static string dataBasePath = System.IO.Path.Combine(Application.persistentDataPath, BundlePrefix);
#endif
    private static string streamBasePath = System.IO.Path.Combine(Application.streamingAssetsPath,
        BundlePrefix).Replace("\\", "/");

#if UNITY_EDITOR
    /// <summary>
    /// 
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    public static string GetBuildBundleOutpath(BuildTarget target)
    {
        string folder = GetPlatformFolder(target);
        if (!string.IsNullOrEmpty(folder))
        {
            return System.IO.Path.Combine(System.Environment.CurrentDirectory,
                string.Format("{0}/{1}", BuildBundle, folder.Replace("\\", "/")));
        }

        return null;
    }
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

    public static string GetPlatformFolder()
    {
#if UNITY_EDITOR
        return GetPlatformFolder(EditorUserBuildSettings.activeBuildTarget);
#else
        return GetPlatformFolder(Application.platform);
#endif
    }
}