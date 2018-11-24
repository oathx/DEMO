using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using SevenZip.Compression.LZMA;

public class XProcessPack
{
    public static string OutCachePath;

    /// <summary>
    /// 
    /// </summary>
    public static XProcessPack instance;
    
	/// <summary>
	/// Initializes the <see cref="XProcessPack"/> class.
	/// </summary>
    static XProcessPack()
    {
        instance = new XProcessPack();

        #if UNITY_ANDROID && !UNITY_EDITOR
			OutCachePath = Path.Combine(Application.persistentDataPath, "act_cache");
        #else
			OutCachePath = Path.Combine(Application.temporaryCachePath, "act_cache");
        #endif

        try
        {
            InitPaths();

            if (!Directory.Exists(OutCachePath))
                Directory.CreateDirectory(OutCachePath);
        }
		catch(System.Exception e){
			Debug.LogException (e);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    static string packPathPrefix = string.Empty;

    /// <summary>
    /// 
    /// </summary>
    static void InitPaths()
    {
        string streamPath = Application.streamingAssetsPath;
        string[] splits = streamPath.Split('!');
        if (splits != null && splits.Length == 2)
        {
            packPathPrefix = splits[1];
            while (packPathPrefix.StartsWith("\\") || packPathPrefix.StartsWith("/") || packPathPrefix.StartsWith("!"))
            {
                packPathPrefix = packPathPrefix.Substring(1);
            }
        }
    }
}
