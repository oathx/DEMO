using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_XRotateMode : LuaObject {
	static public void reg(IntPtr l) {
		getEnumTable(l,"XRotateMode");
		addMember(l,0,"Fast");
		addMember(l,1,"FastBeyond360");
		addMember(l,2,"WorldAxisAdd");
		addMember(l,3,"LocalAxisAdd");
		LuaDLL.lua_pop(l, 1);
	}
}
