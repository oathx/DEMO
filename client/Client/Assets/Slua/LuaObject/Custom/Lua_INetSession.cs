using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_INetSession : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Connect(IntPtr l) {
		try {
			INetSession self=(INetSession)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			System.Int32 a2;
			checkType(l,3,out a2);
			var ret=self.Connect(a1,a2);
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SendPacket(IntPtr l) {
		try {
			INetSession self=(INetSession)checkSelf(l);
			INetPacket a1;
			checkType(l,2,out a1);
			self.SendPacket(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int PostPacket(IntPtr l) {
		try {
			INetSession self=(INetSession)checkSelf(l);
			INetPacket a1;
			checkType(l,2,out a1);
			self.PostPacket(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Close(IntPtr l) {
		try {
			INetSession self=(INetSession)checkSelf(l);
			self.Close();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Connected(IntPtr l) {
		try {
			INetSession self=(INetSession)checkSelf(l);
			var ret=self.Connected();
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetPacketQueue(IntPtr l) {
		try {
			INetSession self=(INetSession)checkSelf(l);
			var ret=self.GetPacketQueue();
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
		getTypeTable(l,"INetSession");
		addMember(l,Connect);
		addMember(l,SendPacket);
		addMember(l,PostPacket);
		addMember(l,Close);
		addMember(l,Connected);
		addMember(l,GetPacketQueue);
		createTypeMetatable(l,null, typeof(INetSession));
	}
}
