using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
public class XBundleExtraUtil
{
    public static string BundleExtraCacheInfoPath       = "Assets/Editor/Bundle/BundleExtraInfo.txt";
    public static string BundleExtraPackageInfoPath     = "Assets/Editor/Bundle/BundleExtraPackageInfo";
    public static string BundleExtraInfoName            = "BundleExtraInfo";

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static string GetBundleExtraCacheInfoText()
    {
        return GetBundleExtraText(BundleExtraCacheInfoPath);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string GetBundleExtraText(string path)
    {
        string data     = string.Empty;
        string extPath  = path;
        if (System.IO.File.Exists(extPath))
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(extPath);
            data = sr.ReadToEnd();
            sr.Close();
        }
        else
        {
            TextAsset text = Resources.Load<TextAsset>(BundleExtraInfoName);
            if (text != null)
            {
                data = text.text;
            }
        }

        return data;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="arr"></param>
    /// <returns></returns>
    public static List<T> 
        ArrayToList<T>(T[] arr)
    {
        if (arr != null)
        {
            List<T> generated = new List<T>();
            for (int i = 0; i < arr.Length; i++)
            {
                generated.Add(arr[i]);
            }
            return generated;
        }
        return null;
    }
}

/// <summary>
/// 
/// </summary>
[System.Serializable]
public class XBundleExtraCache
{
    public string[] NotCache;
    public string[] CacheInScene;
    public string[] CacheAllTime;

    public string[] NotCompress;
    public string[] CompressLZMA;
    public string[] CompressLZ4;

    // in default, we decide compress type by cache type
    public void ResetCompressToDefault()
    {
        this.NotCompress = this.CacheAllTime;
        this.CompressLZMA = this.NotCache;
        this.CompressLZ4 = this.CacheInScene;
    }
}

/// <summary>
/// 
/// </summary>
[System.Serializable]
public class XBundleExtraPack
{
    public string[] PackgesCategory1;
    public string[] PackgesCategory2;
    public string[] PackgesCategory3;
}
