using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;
using System.Timers;

public enum SearchFileType{
	png,
	prefab,
	unity,
	fbx,
	mat,
	jpg,
	tag,
	xml,
	bytes,
	pass,
	unity3d,
	zip,
	cs,
	dds,
	DDS,
	asset,
	controller,
	preview,
	txt,
}

public class EditorHelper
{
	/// <summary>
	/// Gets the out path.
	/// </summary>
	/// <value>The out path.</value>
	public static string	OutPath
	{
		get
		{
#if UNITY_STANDALONE && UNITY_EDITOR
			return Application.streamingAssetsPath + "/Win";
#elif UNITY_ANDROID
			return Application.streamingAssetsPath + "/Android";
#elif UNITY_IPHONE
			return Application.streamingAssetsPath + "/iOS";
#endif
		}
	}

	/// <summary>
	/// Gets the middle path.
	/// </summary>
	/// <value>The middle path.</value>
	public static string	MidPath
	{
		get{
#if UNITY_STANDALONE && UNITY_EDITOR
			return Application.streamingAssetsPath + "/Resource/Win";
#elif UNITY_ANDROID
			return Application.streamingAssetsPath + "/Resource/Android";
#elif UNITY_IPHONE
			return Application.streamingAssetsPath + "/Resource/iOS";
#endif
		}
	}

	public static string 	HeroShapeAssetPath {
		get{
			return string.Format ("{0}/{1}", Application.dataPath, "Resources/Hero/ShapeAsset");
		}
	}

	/// <summary>
	/// Searchs the file.
	/// </summary>
	/// <returns>The file.</returns>
	/// <param name="type">Type.</param>
	/// <param name="szInputPath">Size input path.</param>
	public static List<string>
		SearchFile(SearchFileType type, string szInputPath)
	{
		List<string> 
			aryReturn = new List<string>();

		SearchFileType[] aryMask = new SearchFileType[]{
			SearchFileType.png,
			SearchFileType.prefab,
			SearchFileType.unity,
			SearchFileType.fbx,
			SearchFileType.mat,
			SearchFileType.jpg,
			SearchFileType.tag,
			SearchFileType.xml,
			SearchFileType.bytes,
			SearchFileType.pass,
			SearchFileType.unity3d,
			SearchFileType.zip,
			SearchFileType.cs,
			SearchFileType.dds,
			SearchFileType.DDS,
			SearchFileType.asset,
			SearchFileType.controller,
			SearchFileType.preview,
		};

		foreach(SearchFileType mask in aryMask)
		{
			if ((type & mask) != 0)
			{
				// Priority search prefab file
				string pattern = string.Format(
					"{0}.{1}", "*", mask.ToString().ToLower()
					);

				string[] aryFile = System.IO.Directory.GetFiles(szInputPath, pattern, SearchOption.AllDirectories);
				foreach(string path in aryFile)
				{
					string szPath = path.Substring(Application.dataPath.Length - 6);
					aryReturn.Add(
						szPath.Replace("\\", "/")
						);
				}
			}
		}

		return aryReturn;
	}

	/// <summary>
	/// Gets the length of the file.
	/// </summary>
	/// <returns>The file length.</returns>
	/// <param name="szFilePath">Size file path.</param>
	public static int 	GetFileLength(string szFilePath)
	{
		int nLength = 0;
		FileStream stream = File.OpenRead(szFilePath);
		if (stream.CanRead)
			nLength = (int)stream.Length;

		stream.Close ();

		return nLength;
	}

	/// <summary>
	/// Builds the asset bundles.
	/// </summary>
	/// <param name="szPackageName">Size package name.</param>
	/// <param name="outZipFile">If set to <c>true</c> out zip file.</param>
	/// <param name="cleaup">If set to <c>true</c> cleaup.</param>
	/// <param name="szDirectory">Size directory.</param>
	/// <param name="szPattern">Size pattern.</param>
	public static bool 	BuildAssetBundles(string szInputPath, SearchFileType type)
	{
		if (string.IsNullOrEmpty(szInputPath))
			throw new System.NullReferenceException();

		// create mid path
		if (!Directory.Exists(MidPath))
			Directory.CreateDirectory(MidPath);

		// Search all files 
		List<string> aryFile = SearchFile(type, szInputPath);
		if (aryFile.Count <= 0)
			return false;

		// get directory name 
		string[] arySplit = szInputPath.Split('/');
		if (arySplit.Length <= 0)
			return false;

		AssetBundleBuild ab 	= new AssetBundleBuild();
		ab.assetNames			= aryFile.ToArray();
		ab.assetBundleVariant	= SearchFileType.unity3d.ToString();
		ab.assetBundleName		= arySplit[arySplit.Length - 1];
	
		// get the assetbundl file path
		string szOutPath = MidPath.Substring(
			Application.dataPath.Length - 6, MidPath.Length - Application.dataPath.Length + 6
			);

		BuildPipeline.BuildAssetBundles(szOutPath, new AssetBundleBuild[]{ab}, 
			BuildAssetBundleOptions.UncompressedAssetBundle, EditorUserBuildSettings.activeBuildTarget);

		return true;
	}

	/// <summary>
	/// Filter the specified path.
	/// </summary>
	/// <param name="path">Path.</param>
	public static bool	Filter(string path)
	{
		string[] aryFilter = {
			".meta", ".zip", ".manifest"
		};
		
		foreach(string filter in aryFilter)
		{
			if (path.Contains(filter))
				return true;
		}
		
		return false;
	}

	/// <summary>
	/// Gets the name of the asset path.
	/// </summary>
	/// <returns>The asset path name.</returns>
	/// <param name="szAssetPath">Size asset path.</param>
	/// <param name="szReplace">Size replace.</param>
	public static string	GetAssetPathName(string szAssetPath, string szReplace)
	{
		string[] arySplit = szAssetPath.Split('.');
		if (arySplit.Length > 0)
			return szAssetPath.Replace(arySplit[arySplit.Length - 1], szReplace);
		
		return szAssetPath;
	}
		
	/// <summary>
	/// Creates the empty prefab.
	/// </summary>
	/// <returns>The empty prefab.</returns>
	/// <param name="goItem">Go item.</param>
	/// <param name="szPath">Size path.</param>
	public static GameObject	CreateEmptyPrefab(GameObject goItem, string szPath)
	{
		string szFilePath	= szPath.Replace("\\", "/");
		
		// get the assetbundl file path
		string szAssetPath 	= szFilePath.Substring(
			Application.dataPath.Length - 6, szFilePath.Length - Application.dataPath.Length + 6
			);
		
		Object prefab = PrefabUtility.CreateEmptyPrefab(szAssetPath);
		if (!prefab)
			throw new System.NullReferenceException();
		
		prefab = PrefabUtility.ReplacePrefab(goItem, prefab);
		GameObject.DestroyImmediate(goItem);
		
		return prefab as GameObject;
	}

	/// <summary>
	/// Gets the asset path.
	/// </summary>
	/// <returns>The asset path.</returns>
	/// <param name="szPath">Size path.</param>
	public static string		GetAssetPath(string szPath)
	{
		string szFilePath	= szPath.Replace("\\", "/");
		
		// get the assetbundl file path
		return szFilePath.Substring(
			Application.dataPath.Length - 6, szFilePath.Length - Application.dataPath.Length + 6
			);
	}

	/// <summary>
	/// Gets the name of the file.
	/// </summary>
	/// <returns>The file name.</returns>
	/// <param name="szPath">Size path.</param>
	public static string	GetFileName(string szPath)
	{
		string szFilePath	= szPath.Replace("\\", "/");
		string szFileName 	= szPath;

		if (!string.IsNullOrEmpty (szFilePath)) {
			int iStart 	= szFilePath.LastIndexOf ("/");
			int iEnd 	= szFilePath.LastIndexOf (".");

			if (iEnd > iStart) {
				szFileName = szFilePath.Substring (iStart + 1, iEnd - iStart - 1);
			}
		}

		return szFileName;
	}

	/// <summary>
	/// Clears the directory.
	/// </summary>
	/// <param name="szPath">Size path.</param>
	public static void 	ClearDirectory(string szPath)
	{
		// clear old file
		string[] aryDelete = System.IO.Directory.GetFiles(
			szPath, "*.*", SearchOption.AllDirectories
			);
		foreach(string delete in aryDelete)
		{
			File.Delete(delete);
		}
	}

    /// <summary>
    /// Draw a distinctly different looking header label
    /// </summary>

    static public bool DrawHeader(string text) { return DrawHeader(text, text, false); }

    /// <summary>
    /// Draw a distinctly different looking header label
    /// </summary>

    static public bool DrawHeader(string text, string key) { return DrawHeader(text, key, false); }

    /// <summary>
    /// Draw a distinctly different looking header label
    /// </summary>

    static public bool DrawHeader(string text, bool forceOn) { return DrawHeader(text, text, forceOn); }

    /// <summary>
    /// Draw a distinctly different looking header label
    /// </summary>

    static public bool DrawHeader(string text, string key, bool forceOn)
    {
        bool state = EditorPrefs.GetBool(key, true);

        GUILayout.Space(3f);
        if (!forceOn && !state) GUI.backgroundColor = new Color(0.8f, 0.8f, 0.8f);
        GUILayout.BeginHorizontal();
        GUILayout.Space(3f);

        GUI.changed = false;
#if UNITY_3_5
            if (state) text = "\u25B2 " + text;
            else text = "\u25BC " + text;
            if (!GUILayout.Toggle(true, text, "dragtab", GUILayout.MinWidth(20f))) state = !state;
#else
        text = "<b><size=11>" + text + "</size></b>";
        if (state) text = "\u25B2 " + text;
        else text = "\u25BC " + text;
        if (!GUILayout.Toggle(true, text, "dragtab", GUILayout.MinWidth(20f))) state = !state;
#endif
        if (GUI.changed) EditorPrefs.SetBool(key, state);

        GUILayout.Space(2f);
        GUILayout.EndHorizontal();
        GUI.backgroundColor = Color.white;
        if (!forceOn && !state) GUILayout.Space(3f);
        return state;
    }

    /// <summary>
    /// Begin drawing the content area.
    /// </summary>

    static public void BeginContents()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Space(4f);
        EditorGUILayout.BeginHorizontal("AS TextArea", GUILayout.MinHeight(10f));
        GUILayout.BeginVertical();
        GUILayout.Space(2f);
    }

    /// <summary>
    /// End drawing the content area.
    /// </summary>

    static public void EndContents()
    {
        GUILayout.Space(3f);
        GUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();
        GUILayout.Space(3f);
        GUILayout.EndHorizontal();
        GUILayout.Space(3f);
    }
}