using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_CC_AnalogTV : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_autoPhase(IntPtr l) {
		try {
			CC_AnalogTV self=(CC_AnalogTV)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.autoPhase);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_autoPhase(IntPtr l) {
		try {
			CC_AnalogTV self=(CC_AnalogTV)checkSelf(l);
			System.Boolean v;
			checkType(l,2,out v);
			self.autoPhase=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_phase(IntPtr l) {
		try {
			CC_AnalogTV self=(CC_AnalogTV)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.phase);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_phase(IntPtr l) {
		try {
			CC_AnalogTV self=(CC_AnalogTV)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.phase=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_grayscale(IntPtr l) {
		try {
			CC_AnalogTV self=(CC_AnalogTV)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.grayscale);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_grayscale(IntPtr l) {
		try {
			CC_AnalogTV self=(CC_AnalogTV)checkSelf(l);
			System.Boolean v;
			checkType(l,2,out v);
			self.grayscale=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_noiseIntensity(IntPtr l) {
		try {
			CC_AnalogTV self=(CC_AnalogTV)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.noiseIntensity);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_noiseIntensity(IntPtr l) {
		try {
			CC_AnalogTV self=(CC_AnalogTV)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.noiseIntensity=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_scanlinesIntensity(IntPtr l) {
		try {
			CC_AnalogTV self=(CC_AnalogTV)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.scanlinesIntensity);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_scanlinesIntensity(IntPtr l) {
		try {
			CC_AnalogTV self=(CC_AnalogTV)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.scanlinesIntensity=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_scanlinesCount(IntPtr l) {
		try {
			CC_AnalogTV self=(CC_AnalogTV)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.scanlinesCount);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_scanlinesCount(IntPtr l) {
		try {
			CC_AnalogTV self=(CC_AnalogTV)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.scanlinesCount=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_scanlinesOffset(IntPtr l) {
		try {
			CC_AnalogTV self=(CC_AnalogTV)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.scanlinesOffset);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_scanlinesOffset(IntPtr l) {
		try {
			CC_AnalogTV self=(CC_AnalogTV)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.scanlinesOffset=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_distortion(IntPtr l) {
		try {
			CC_AnalogTV self=(CC_AnalogTV)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.distortion);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_distortion(IntPtr l) {
		try {
			CC_AnalogTV self=(CC_AnalogTV)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.distortion=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_cubicDistortion(IntPtr l) {
		try {
			CC_AnalogTV self=(CC_AnalogTV)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.cubicDistortion);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_cubicDistortion(IntPtr l) {
		try {
			CC_AnalogTV self=(CC_AnalogTV)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.cubicDistortion=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_scale(IntPtr l) {
		try {
			CC_AnalogTV self=(CC_AnalogTV)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.scale);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_scale(IntPtr l) {
		try {
			CC_AnalogTV self=(CC_AnalogTV)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.scale=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"CC_AnalogTV");
		addMember(l,"autoPhase",get_autoPhase,set_autoPhase,true);
		addMember(l,"phase",get_phase,set_phase,true);
		addMember(l,"grayscale",get_grayscale,set_grayscale,true);
		addMember(l,"noiseIntensity",get_noiseIntensity,set_noiseIntensity,true);
		addMember(l,"scanlinesIntensity",get_scanlinesIntensity,set_scanlinesIntensity,true);
		addMember(l,"scanlinesCount",get_scanlinesCount,set_scanlinesCount,true);
		addMember(l,"scanlinesOffset",get_scanlinesOffset,set_scanlinesOffset,true);
		addMember(l,"distortion",get_distortion,set_distortion,true);
		addMember(l,"cubicDistortion",get_cubicDistortion,set_cubicDistortion,true);
		addMember(l,"scale",get_scale,set_scale,true);
		createTypeMetatable(l,null, typeof(CC_AnalogTV),typeof(CC_Base));
	}
}
