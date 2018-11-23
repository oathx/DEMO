using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_XShaderEffect : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			XShaderEffect o;
			o=new XShaderEffect();
			pushValue(l,true);
			pushValue(l,o);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Dissolve_s(IntPtr l) {
		try {
			DissolveType a1;
			checkEnum(l,1,out a1);
			UnityEngine.Material[] a2;
			checkArray(l,2,out a2);
			UnityEngine.GameObject a3;
			checkType(l,3,out a3);
			System.Single a4;
			checkType(l,4,out a4);
			System.Boolean a5;
			checkType(l,5,out a5);
			System.Action a6;
			checkDelegate(l,6,out a6);
			var ret=XShaderEffect.Dissolve(a1,a2,a3,a4,a5,a6);
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
	static public int NormalDissolve_s(IntPtr l) {
		try {
			UnityEngine.Material[] a1;
			checkArray(l,1,out a1);
			UnityEngine.GameObject a2;
			checkType(l,2,out a2);
			System.Single a3;
			checkType(l,3,out a3);
			System.Boolean a4;
			checkType(l,4,out a4);
			System.Action a5;
			checkDelegate(l,5,out a5);
			var ret=XShaderEffect.NormalDissolve(a1,a2,a3,a4,a5);
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
		getTypeTable(l,"XShaderEffect");
		addMember(l,Dissolve_s);
		addMember(l,NormalDissolve_s);
		createTypeMetatable(l,constructor, typeof(XShaderEffect));
	}
}
