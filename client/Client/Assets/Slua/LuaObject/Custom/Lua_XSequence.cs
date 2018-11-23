using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_XSequence : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			XSequence o;
			o=new XSequence();
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
	static public int Append(IntPtr l) {
		try {
			XSequence self=(XSequence)checkSelf(l);
			XTween a1;
			checkType(l,2,out a1);
			var ret=self.Append(a1);
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
	static public int AppendCallback(IntPtr l) {
		try {
			XSequence self=(XSequence)checkSelf(l);
			SLua.LuaFunction a1;
			checkType(l,2,out a1);
			var ret=self.AppendCallback(a1);
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
	static public int AppendInterval(IntPtr l) {
		try {
			XSequence self=(XSequence)checkSelf(l);
			System.Single a1;
			checkType(l,2,out a1);
			var ret=self.AppendInterval(a1);
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
	static public int Insert(IntPtr l) {
		try {
			XSequence self=(XSequence)checkSelf(l);
			System.Single a1;
			checkType(l,2,out a1);
			XTween a2;
			checkType(l,3,out a2);
			var ret=self.Insert(a1,a2);
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
	static public int InsertCallback(IntPtr l) {
		try {
			XSequence self=(XSequence)checkSelf(l);
			System.Single a1;
			checkType(l,2,out a1);
			SLua.LuaFunction a2;
			checkType(l,3,out a2);
			var ret=self.InsertCallback(a1,a2);
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
	static public int Join(IntPtr l) {
		try {
			XSequence self=(XSequence)checkSelf(l);
			XTween a1;
			checkType(l,2,out a1);
			var ret=self.Join(a1);
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
	static public int Prepend(IntPtr l) {
		try {
			XSequence self=(XSequence)checkSelf(l);
			XTween a1;
			checkType(l,2,out a1);
			var ret=self.Prepend(a1);
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
	static public int PrependCallback(IntPtr l) {
		try {
			XSequence self=(XSequence)checkSelf(l);
			SLua.LuaFunction a1;
			checkType(l,2,out a1);
			var ret=self.PrependCallback(a1);
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
	static public int PrependInterval(IntPtr l) {
		try {
			XSequence self=(XSequence)checkSelf(l);
			System.Single a1;
			checkType(l,2,out a1);
			var ret=self.PrependInterval(a1);
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
		getTypeTable(l,"XSequence");
		addMember(l,Append);
		addMember(l,AppendCallback);
		addMember(l,AppendInterval);
		addMember(l,Insert);
		addMember(l,InsertCallback);
		addMember(l,Join);
		addMember(l,Prepend);
		addMember(l,PrependCallback);
		addMember(l,PrependInterval);
		createTypeMetatable(l,constructor, typeof(XSequence),typeof(XTween));
	}
}
