using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_XBox2DComponent : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_FloatCorrection(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,XBox2DComponent.FloatCorrection);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"XBox2DComponent");
		addMember(l,"FloatCorrection",get_FloatCorrection,null,false);
		createTypeMetatable(l,null, typeof(XBox2DComponent),typeof(XActorComponent));
	}
}
