using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_LuaBehaviourScript : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetLuaBehaviourScript_s(IntPtr l) {
		try {
			UnityEngine.GameObject a1;
			checkType(l,1,out a1);
			var ret=LuaBehaviourScript.GetLuaBehaviourScript(a1);
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
	static public int AddLuaBehaviourScript_s(IntPtr l) {
		try {
			UnityEngine.GameObject a1;
			checkType(l,1,out a1);
			SLua.LuaTable a2;
			checkType(l,2,out a2);
			var ret=LuaBehaviourScript.AddLuaBehaviourScript(a1,a2);
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
	static public int get_LuaModule(IntPtr l) {
		try {
			LuaBehaviourScript self=(LuaBehaviourScript)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.LuaModule);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"LuaBehaviourScript");
		addMember(l,GetLuaBehaviourScript_s);
		addMember(l,AddLuaBehaviourScript_s);
		addMember(l,"LuaModule",get_LuaModule,null,true);
		createTypeMetatable(l,null, typeof(LuaBehaviourScript),typeof(UnityEngine.MonoBehaviour));
	}
}
