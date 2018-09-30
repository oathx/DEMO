
using System;
using System.Collections.Generic;
namespace SLua
{
    public partial class LuaDelegation : LuaObject
    {

        static internal void Lua_UIEventTriggerListener_VoidDelegate(LuaFunction ld ,SLua.LuaTable a1,UnityEngine.GameObject a2,UnityEngine.EventSystems.BaseEventData a3) {
            IntPtr l = ld.L;
            int error = pushTry(l);

			pushValue(l,a1);
			pushValue(l,a2);
			pushValue(l,a3);
			ld.pcall(3, error);
			LuaDLL.lua_settop(l, error-1);
		}
	}
}
