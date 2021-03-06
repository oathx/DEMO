﻿using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_XShapeAssetObject : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			XShapeAssetObject o;
			o=new XShapeAssetObject();
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
	static public int get_LocalPosition(IntPtr l) {
		try {
			XShapeAssetObject self=(XShapeAssetObject)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.LocalPosition);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_LocalPosition(IntPtr l) {
		try {
			XShapeAssetObject self=(XShapeAssetObject)checkSelf(l);
			UnityEngine.Vector3 v;
			checkType(l,2,out v);
			self.LocalPosition=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LocalAngle(IntPtr l) {
		try {
			XShapeAssetObject self=(XShapeAssetObject)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.LocalAngle);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_LocalAngle(IntPtr l) {
		try {
			XShapeAssetObject self=(XShapeAssetObject)checkSelf(l);
			UnityEngine.Vector3 v;
			checkType(l,2,out v);
			self.LocalAngle=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LocalScale(IntPtr l) {
		try {
			XShapeAssetObject self=(XShapeAssetObject)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.LocalScale);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_LocalScale(IntPtr l) {
		try {
			XShapeAssetObject self=(XShapeAssetObject)checkSelf(l);
			UnityEngine.Vector3 v;
			checkType(l,2,out v);
			self.LocalScale=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"XShapeAssetObject");
		addMember(l,"LocalPosition",get_LocalPosition,set_LocalPosition,true);
		addMember(l,"LocalAngle",get_LocalAngle,set_LocalAngle,true);
		addMember(l,"LocalScale",get_LocalScale,set_LocalScale,true);
		createTypeMetatable(l,constructor, typeof(XShapeAssetObject),typeof(UnityEngine.ScriptableObject));
	}
}
