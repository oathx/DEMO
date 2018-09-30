using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_GUtility : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int HashString_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			var ret=GUtility.HashString(a1);
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
	static public int Md5Sum_s(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(matchType(l,argc,1,typeof(System.Byte[]))){
				System.Byte[] a1;
				checkArray(l,1,out a1);
				var ret=GUtility.Md5Sum(a1);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(matchType(l,argc,1,typeof(string))){
				System.String a1;
				checkType(l,1,out a1);
				var ret=GUtility.Md5Sum(a1);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function Md5Sum to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int TimeStampToDateTime_s(IntPtr l) {
		try {
			System.Int32 a1;
			checkType(l,1,out a1);
			var ret=GUtility.TimeStampToDateTime(a1);
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
	static public int DateTimeToTimeStamp_s(IntPtr l) {
		try {
			System.DateTime a1;
			checkValueType(l,1,out a1);
			var ret=GUtility.DateTimeToTimeStamp(a1);
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
	static public int GetNaturalDaysCount_s(IntPtr l) {
		try {
			System.DateTime a1;
			checkValueType(l,1,out a1);
			System.DateTime a2;
			checkValueType(l,2,out a2);
			var ret=GUtility.GetNaturalDaysCount(a1,a2);
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
	static public int GetHoursCount_s(IntPtr l) {
		try {
			System.DateTime a1;
			checkValueType(l,1,out a1);
			System.DateTime a2;
			checkValueType(l,2,out a2);
			var ret=GUtility.GetHoursCount(a1,a2);
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
	static public int SyncTimeFromServer_s(IntPtr l) {
		try {
			System.Int32 a1;
			checkType(l,1,out a1);
			GUtility.SyncTimeFromServer(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int NoCacheUrl_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			var ret=GUtility.NoCacheUrl(a1);
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
	static public int UnZipFile_s(IntPtr l) {
		try {
			System.Byte[] a1;
			checkArray(l,1,out a1);
			System.String a2;
			checkType(l,2,out a2);
			GUtility.UnZipFile(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_serverTimeOffset(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,GUtility.serverTimeOffset);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_serverTimeOffset(IntPtr l) {
		try {
			System.Int32 v;
			checkType(l,2,out v);
			GUtility.serverTimeOffset=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ServerTime(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,GUtility.ServerTime);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ServerDateTime(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,GUtility.ServerDateTime);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"GUtility");
		addMember(l,HashString_s);
		addMember(l,Md5Sum_s);
		addMember(l,TimeStampToDateTime_s);
		addMember(l,DateTimeToTimeStamp_s);
		addMember(l,GetNaturalDaysCount_s);
		addMember(l,GetHoursCount_s);
		addMember(l,SyncTimeFromServer_s);
		addMember(l,NoCacheUrl_s);
		addMember(l,UnZipFile_s);
		addMember(l,"serverTimeOffset",get_serverTimeOffset,set_serverTimeOffset,false);
		addMember(l,"ServerTime",get_ServerTime,null,false);
		addMember(l,"ServerDateTime",get_ServerDateTime,null,false);
		createTypeMetatable(l,null, typeof(GUtility));
	}
}
