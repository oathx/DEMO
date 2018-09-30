using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_NetTcpSession : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			NetTcpSession o;
			o=new NetTcpSession();
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
	static public int Connect(IntPtr l) {
		try {
			NetTcpSession self=(NetTcpSession)checkSelf(l);
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
			NetTcpSession self=(NetTcpSession)checkSelf(l);
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
			NetTcpSession self=(NetTcpSession)checkSelf(l);
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
	static public int GetPacketQueue(IntPtr l) {
		try {
			NetTcpSession self=(NetTcpSession)checkSelf(l);
			var ret=self.GetPacketQueue();
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
	static public int Close(IntPtr l) {
		try {
			NetTcpSession self=(NetTcpSession)checkSelf(l);
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
			NetTcpSession self=(NetTcpSession)checkSelf(l);
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
	static public int OnConnThread(IntPtr l) {
		try {
			NetTcpSession self=(NetTcpSession)checkSelf(l);
			System.Object a1;
			checkType(l,2,out a1);
			self.OnConnThread(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int OnSendThread(IntPtr l) {
		try {
			NetTcpSession self=(NetTcpSession)checkSelf(l);
			self.OnSendThread();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int OnRecvThread(IntPtr l) {
		try {
			NetTcpSession self=(NetTcpSession)checkSelf(l);
			self.OnRecvThread();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"NetTcpSession");
		addMember(l,Connect);
		addMember(l,SendPacket);
		addMember(l,PostPacket);
		addMember(l,GetPacketQueue);
		addMember(l,Close);
		addMember(l,Connected);
		addMember(l,OnConnThread);
		addMember(l,OnSendThread);
		addMember(l,OnRecvThread);
		createTypeMetatable(l,constructor, typeof(NetTcpSession),typeof(INetSession));
	}
}
