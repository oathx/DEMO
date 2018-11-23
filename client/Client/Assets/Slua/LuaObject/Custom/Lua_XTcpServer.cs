using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_XTcpServer : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			XTcpServer o;
			o=new XTcpServer();
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
	static public int Initliaze(IntPtr l) {
		try {
			XTcpServer self=(XTcpServer)checkSelf(l);
			self.Initliaze();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Startup(IntPtr l) {
		try {
			XTcpServer self=(XTcpServer)checkSelf(l);
			TcpServerConfig a1;
			checkType(l,2,out a1);
			self.Startup(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Connect(IntPtr l) {
		try {
			XTcpServer self=(XTcpServer)checkSelf(l);
			var ret=self.Connect();
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
	static public int Connected(IntPtr l) {
		try {
			XTcpServer self=(XTcpServer)checkSelf(l);
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
	static public int Disconnect(IntPtr l) {
		try {
			XTcpServer self=(XTcpServer)checkSelf(l);
			self.Disconnect();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Reconnect(IntPtr l) {
		try {
			XTcpServer self=(XTcpServer)checkSelf(l);
			var ret=self.Reconnect();
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
	static public int Send(IntPtr l) {
		try {
			XTcpServer self=(XTcpServer)checkSelf(l);
			XNetLuaPacket a1;
			checkType(l,2,out a1);
			self.Send(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Post(IntPtr l) {
		try {
			XTcpServer self=(XTcpServer)checkSelf(l);
			XNetLuaPacket a1;
			checkType(l,2,out a1);
			self.Post(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Update(IntPtr l) {
		try {
			XTcpServer self=(XTcpServer)checkSelf(l);
			self.Update();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Shutdown(IntPtr l) {
		try {
			XTcpServer self=(XTcpServer)checkSelf(l);
			self.Shutdown();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetSingleton_s(IntPtr l) {
		try {
			var ret=XTcpServer.GetSingleton();
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
		getTypeTable(l,"XTcpServer");
		addMember(l,Initliaze);
		addMember(l,Startup);
		addMember(l,Connect);
		addMember(l,Connected);
		addMember(l,Disconnect);
		addMember(l,Reconnect);
		addMember(l,Send);
		addMember(l,Post);
		addMember(l,Update);
		addMember(l,Shutdown);
		addMember(l,GetSingleton_s);
		createTypeMetatable(l,constructor, typeof(XTcpServer));
	}
}
