﻿using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_XAxisConstraint : LuaObject {
	static public void reg(IntPtr l) {
		getEnumTable(l,"XAxisConstraint");
		addMember(l,0,"None");
		addMember(l,2,"X");
		addMember(l,4,"Y");
		addMember(l,8,"Z");
		addMember(l,16,"W");
		LuaDLL.lua_pop(l, 1);
	}
}
