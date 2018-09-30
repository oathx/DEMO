using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_XScrambleMode : LuaObject {
	static public void reg(IntPtr l) {
		getEnumTable(l,"XScrambleMode");
		addMember(l,0,"None");
		addMember(l,1,"All");
		addMember(l,2,"Uppercase");
		addMember(l,3,"Lowercase");
		addMember(l,4,"Numerals");
		addMember(l,5,"Custom");
		LuaDLL.lua_pop(l, 1);
	}
}
