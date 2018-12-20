using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_XBox2DSystem : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			XBox2DSystem o;
			o=new XBox2DSystem();
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
	static public int RegisterFlexibleHurtBox(IntPtr l) {
		try {
			XBox2DSystem self=(XBox2DSystem)checkSelf(l);
			XBox2DFlexibleHurt a1;
			checkType(l,2,out a1);
			self.RegisterFlexibleHurtBox(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int UnRegisterFlexibleHurtBox(IntPtr l) {
		try {
			XBox2DSystem self=(XBox2DSystem)checkSelf(l);
			XBox2DFlexibleHurt a1;
			checkType(l,2,out a1);
			self.UnRegisterFlexibleHurtBox(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetSingleton_s(IntPtr l) {
		try {
			var ret=XBox2DSystem.GetSingleton();
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
		getTypeTable(l,"XBox2DSystem");
		addMember(l,RegisterFlexibleHurtBox);
		addMember(l,UnRegisterFlexibleHurtBox);
		addMember(l,GetSingleton_s);
		createTypeMetatable(l,constructor, typeof(XBox2DSystem));
	}
}
