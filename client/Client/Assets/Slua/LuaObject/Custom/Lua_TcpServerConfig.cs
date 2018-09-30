using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_TcpServerConfig : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			TcpServerConfig o;
			o=new TcpServerConfig();
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
	static public int get_ipAddress(IntPtr l) {
		try {
			TcpServerConfig self=(TcpServerConfig)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ipAddress);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_ipAddress(IntPtr l) {
		try {
			TcpServerConfig self=(TcpServerConfig)checkSelf(l);
			System.String v;
			checkType(l,2,out v);
			self.ipAddress=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ipPort(IntPtr l) {
		try {
			TcpServerConfig self=(TcpServerConfig)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ipPort);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_ipPort(IntPtr l) {
		try {
			TcpServerConfig self=(TcpServerConfig)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.ipPort=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_timeOut(IntPtr l) {
		try {
			TcpServerConfig self=(TcpServerConfig)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.timeOut);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_timeOut(IntPtr l) {
		try {
			TcpServerConfig self=(TcpServerConfig)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.timeOut=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_netFunc(IntPtr l) {
		try {
			TcpServerConfig self=(TcpServerConfig)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.netFunc);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_netFunc(IntPtr l) {
		try {
			TcpServerConfig self=(TcpServerConfig)checkSelf(l);
			SLua.LuaFunction v;
			checkType(l,2,out v);
			self.netFunc=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"TcpServerConfig");
		addMember(l,"ipAddress",get_ipAddress,set_ipAddress,true);
		addMember(l,"ipPort",get_ipPort,set_ipPort,true);
		addMember(l,"timeOut",get_timeOut,set_timeOut,true);
		addMember(l,"netFunc",get_netFunc,set_netFunc,true);
		createTypeMetatable(l,constructor, typeof(TcpServerConfig));
	}
}
