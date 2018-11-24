using UnityEngine;
using SLua;
using System.IO;

/// <summary>
/// Game app.
/// </summary>
[CustomLuaClass]
public class GameApp : MonoBehaviour
{
	/// <summary>
	/// The lua server.
	/// </summary>
	private static LuaSvr 	        luaServer = null;
	/// <summary>
	/// The instance.
	/// </summary>
	private static GameApp			instance;

	/// <summary>
	/// Gets the singleton.
	/// </summary>
	/// <returns>The singleton.</returns>
	public static GameApp			GetSingleton()
	{
		return instance;
	}

	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake() {
		instance = this;
	}

	// Use this for initialization
	void Start () 
	{
		XTcpServer.GetSingleton().Initliaze();

		InitSlua (delegate(int progress) {
			GameObject.DontDestroyOnLoad (gameObject);
		});
	}

	void FixedUpdate()
	{
        XTcpServer.GetSingleton().Update();
	}

	/// <summary>
	/// Raises the destroy event.
	/// </summary>
	void OnDestroy()
	{
        XTcpServer.GetSingleton().Shutdown();
	}

	/// <summary>
	/// Raises the appliaction quit event.
	/// </summary>
	void OnAppliactionQuit()
	{
		// close lua vm
		LuaSvr.mainState.Dispose ();
	}

    /// <summary>
    /// 
    /// </summary>
    private static string platformPath = string.Empty;

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static string GetPlatformLuaBytesFloder()
    {
        switch (Application.platform)
        {
            case RuntimePlatform.IPhonePlayer:
                {
#if ENABLE_MONO
                    return "iosv7";
#else
            string cpu = SystemInfo.processorType.ToLower();
            if (cpu.Contains("arm64"))
                return "ios64";
            else
                return "iosv7";
#endif

                }

            case RuntimePlatform.OSXEditor:
            case RuntimePlatform.OSXPlayer:
                {
                    return "osx";
                }

            default:
                return "jitx86";
        }
    }

	/// <summary>
	/// Loads the bytes script.
	/// </summary>
	/// <returns>The bytes script.</returns>
	/// <param name="fn">Fn.</param>
	public static byte[]    LoadScript(string fn)
    {
        fn = fn.Replace(".", "/");
        
#if UNITY_STANDALONE || UNITY_EDITOR
        BufferAsset asset = ScriptableObject.CreateInstance<BufferAsset>();

        // find config source code
        string filePath = System.IO.Path.Combine(Application.dataPath, string.Format("Config/{0}.lua", fn));
        if (!System.IO.File.Exists(filePath))
        {
			// find logic script
            filePath = System.IO.Path.Combine(Application.dataPath, string.Format("Code/Script/{0}.lua", fn));
            if (!System.IO.File.Exists(filePath))
            {
				// find lua byte code
				filePath = System.IO.Path.Combine(Application.dataPath, 
					string.Format("Slua/jit/{0}/{1}.bytes", platformPath, fn));
            }
        }

		#if !RELEASE
		GLog.Log(string.Format("{0}", filePath));
		#endif

        if (System.IO.File.Exists(filePath))
        {
            System.IO.FileStream fs = System.IO.File.OpenRead(filePath);
            asset.init((int)fs.Length);
            fs.Read(asset.bytes, 0, (int)fs.Length);
            fs.Close();
        }
		else 
		{
			Debug.LogError("can't find file : " + filePath);
		}
#else
        string hash = GUtility.Md5Sum(string.Format("{0}/{1}",
			platformPath, bytefn));

        BufferAsset asset =  LuaFilePicker.LoadLuaAsset(string.Format("files/{0}", hash));
#endif
        if (asset != null)
            return asset.bytes;

        return null;
    }

	/// <summary>
	/// Inits the slua.
	/// </summary>
	/// <returns><c>true</c>, if slua was inited, <c>false</c> otherwise.</returns>
	/// <param name="progress">Progress.</param>
	[DoNotToLua]
	public static bool			InitSlua(System.Action<int> progress)
	{
        luaServer = new LuaSvr();

		platformPath = GetPlatformLuaBytesFloder ();
		if (!string.IsNullOrEmpty(platformPath))
			LuaSvr.mainState.loaderDelegate = LoadScript;

		luaServer.init(progress, () => {
#if !RELEASE
			LuaSvr.mainState["_DEBUG"] = true;
#endif

#if UNITY_EDITOR 
			LuaSvr.mainState["_EDITOR"] = true;
#endif

#if UNITY_ANDROID
			LuaSvr.mainState["_ANDROID"] = true;
#endif

#if UNITY_IPHONE
			luaServer.luaState["_IPHONE"] = true;
#endif

#if _LANGUAGE_CN
            LuaSvr.mainState["_LANGUAGE_CN"] = true;
#endif

#if _LANGUAGE_EN
			LuaSvr.mainState["_LANGUAGE_EN"] = true;
#endif

#if _LOCAL_SERVER
            LuaSvr.mainState["_LOCAL_SERVER"] = true;
#endif

			var success = luaServer.start("main");
			if( success == null || (bool)success != true )
			{
				Debug.LogError("Lua main intialize failed.");
			}
		}, LuaSvrFlag.LSF_BASIC | LuaSvrFlag.LSF_EXTLIB
			#if !RELEASE && LUA_DEBUG
			| LuaSvrFlag.LSF_DEBUG
			#endif
		);	

		return true;
	}
}
