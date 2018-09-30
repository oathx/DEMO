using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_ResourceManager : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int LoadAsync(IntPtr l) {
		try {
			ResourceManager self=(ResourceManager)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			System.Type a2;
			checkType(l,3,out a2);
			System.Action<UnityEngine.Object> a3;
			checkDelegate(l,4,out a3);
			var ret=self.LoadAsync(a1,a2,a3);
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int LoadMultiAsync(IntPtr l) {
		try {
			ResourceManager self=(ResourceManager)checkSelf(l);
			System.String[] a1;
			checkArray(l,2,out a1);
			System.Type a2;
			checkType(l,3,out a2);
			System.Action<UnityEngine.Object[]> a3;
			checkDelegate(l,4,out a3);
			var ret=self.LoadMultiAsync(a1,a2,a3);
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int LoadScene(IntPtr l) {
		try {
			ResourceManager self=(ResourceManager)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			System.Action<System.String> a2;
			checkDelegate(l,3,out a2);
			var ret=self.LoadScene(a1,a2);
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetSingleton_s(IntPtr l) {
		try {
			var ret=ResourceManager.GetSingleton();
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
		getTypeTable(l,"ResourceManager");
		addMember(l,LoadAsync);
		addMember(l,LoadMultiAsync);
		addMember(l,LoadScene);
		addMember(l,GetSingleton_s);
		createTypeMetatable(l,null, typeof(ResourceManager),typeof(UnityEngine.MonoBehaviour));
	}
}
