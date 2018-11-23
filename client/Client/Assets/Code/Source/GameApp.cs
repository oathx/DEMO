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
	/// Loads the script.
	/// </summary>
	/// <returns>The script.</returns>
	/// <param name="szPath">Size path.</param>
	[DoNotToLua]
	public static byte[] 		LoadScript(string szPath)
	{
		string szScriptPath = string.Format ("{0}/Code/Script/{1}.lua", Application.dataPath, szPath.Replace (".", "/"));
		if (!File.Exists (szScriptPath))
			throw new System.FieldAccessException (szScriptPath);

		#if UNITY_EDITOR
		Debug.Log(string.Format("GameEngine load script =>> {0}", szPath));
		#endif

		FileStream stream = File.OpenRead (szScriptPath);

		// read lua file
		byte[] byRead = new byte[stream.Length];
		stream.Read (byRead, 0, (int)stream.Length);
		stream.Close ();

		return byRead;
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
