using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_GameApp : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetSingleton_s(IntPtr l) {
		try {
			var ret=GameApp.GetSingleton();
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"GameApp");
		addMember(l,GetSingleton_s);
		createTypeMetatable(l,null, typeof(GameApp),typeof(UnityEngine.MonoBehaviour));
	}
}
