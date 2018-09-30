using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_PacketType : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			PacketType o;
			o=new PacketType();
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
	static public int get_SOCKET_CONNECT_FAILURE(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,PacketType.SOCKET_CONNECT_FAILURE);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SOCKET_CONNECT_SUCCESS(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,PacketType.SOCKET_CONNECT_SUCCESS);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SOCKET_DISCONNECT(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,PacketType.SOCKET_DISCONNECT);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"PacketType");
		addMember(l,"SOCKET_CONNECT_FAILURE",get_SOCKET_CONNECT_FAILURE,null,false);
		addMember(l,"SOCKET_CONNECT_SUCCESS",get_SOCKET_CONNECT_SUCCESS,null,false);
		addMember(l,"SOCKET_DISCONNECT",get_SOCKET_DISCONNECT,null,false);
		createTypeMetatable(l,constructor, typeof(PacketType));
	}
}
