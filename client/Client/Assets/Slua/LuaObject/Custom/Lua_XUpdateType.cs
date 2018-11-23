using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_XUpdateType : LuaObject {
	static public void reg(IntPtr l) {
		getEnumTable(l,"XUpdateType");
		addMember(l,0,"Normal");
		addMember(l,1,"Late");
		addMember(l,2,"Fixed");
		LuaDLL.lua_pop(l, 1);
	}
}
