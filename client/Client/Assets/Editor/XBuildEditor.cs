using UnityEditor;
using System.IO;
using UnityEngine;
using System.Collections.Generic;

public class XBuildEditor : EditorWindow 
{
    public class BuildDirectoryCfg
    {
        public string name;
        public bool single;

        public BuildDirectoryCfg(string key, bool s)
        {
            name = key;
            single = s;
        }
    }

    public static List<BuildDirectoryCfg> aryBuildCfg = new List<BuildDirectoryCfg>();
    /// <summary>
    /// 
    /// </summary>
    [MenuItem ("Build/GenerateResource")]
    public static void Build()
    {
        bool isUseBundle = true;
        BuildTarget target = EditorUserBuildSettings.activeBuildTarget;
        if (target == BuildTarget.StandaloneWindows)
        {
            isUseBundle = false;
        }

        aryBuildCfg.Add(new BuildDirectoryCfg("Resources/UI", true));
  
        
        
        Build(target, isUseBundle);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="buildTarget"></param>
    /// <param name="isUseBundle"></param>
    public static void Build(BuildTarget buildTarget, bool isUseBundle)
    {
        //string outpath = XAssetBundleFileSystem.GetBuildBundleOutpath(buildTarget);
        //if (!string.IsNullOrEmpty(outpath))
        //{
        //    BuildAllAssetBundleFile(buildTarget, outpath);
       // }   
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="buildTarget"></param>
    /// <param name="szOutPath"></param>
    public static void BuildAllAssetBundleFile(BuildTarget buildTarget, string szOutPath)
    {
        if (!Directory.Exists(szOutPath))
            Directory.CreateDirectory(szOutPath);

        AssetDatabase.RemoveUnusedAssetBundleNames();

        // build asset bundles
        BuildPipeline.BuildAssetBundles(szOutPath, 
            BuildAssetBundleOptions.ChunkBasedCompression | BuildAssetBundleOptions.DisableWriteTypeTree, buildTarget);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="buildTarget"></param>
    /// <param name="isResourcesOnly"></param>
    public static void BuildAssetBundleFileSystemTable(BuildTarget buildTarget, bool isResourcesOnly)
    {

    }
}

