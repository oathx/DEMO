using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_XRes : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int LoadAsync_s(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==2){
				System.String a1;
				checkType(l,1,out a1);
				System.Action<UnityEngine.Object> a2;
				checkDelegate(l,2,out a2);
				var ret=XRes.LoadAsync<UnityEngine.Object>(a1,a2);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(argc==3){
				System.String a1;
				checkType(l,1,out a1);
				System.Type a2;
				checkType(l,2,out a2);
				System.Action<UnityEngine.Object> a3;
				checkDelegate(l,3,out a3);
				var ret=XRes.LoadAsync(a1,a2,a3);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function LoadAsync to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int LoadMultiAsync_s(IntPtr l) {
		try {
			System.String[] a1;
			checkArray(l,1,out a1);
			System.Action<UnityEngine.Object[]> a2;
			checkDelegate(l,2,out a2);
			var ret=XRes.LoadMultiAsync(a1,a2);
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
	static public int LoadSceneAsync_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			System.Action<System.String> a2;
			checkDelegate(l,2,out a2);
			var ret=XRes.LoadSceneAsync(a1,a2);
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
	static public int Unload_s(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(matchType(l,argc,1,typeof(UnityEngine.Object))){
				UnityEngine.Object a1;
				checkType(l,1,out a1);
				var ret=XRes.Unload(a1);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(matchType(l,argc,1,typeof(string))){
				System.String a1;
				checkType(l,1,out a1);
				var ret=XRes.Unload(a1);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function Unload to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"XRes");
		addMember(l,LoadAsync_s);
		addMember(l,LoadMultiAsync_s);
		addMember(l,LoadSceneAsync_s);
		addMember(l,Unload_s);
		createTypeMetatable(l,null, typeof(XRes));
	}
}
