using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_XLoopType : LuaObject {
	static public void reg(IntPtr l) {
		getEnumTable(l,"XLoopType");
		addMember(l,0,"Restart");
		addMember(l,1,"Yoyo");
		addMember(l,2,"Incremental");
		LuaDLL.lua_pop(l, 1);
	}
}
