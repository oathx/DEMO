using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_DissolveType : LuaObject {
	static public void reg(IntPtr l) {
		getEnumTable(l,"DissolveType");
		addMember(l,0,"DT_NORMAL");
		LuaDLL.lua_pop(l, 1);
	}
}
