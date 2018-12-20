using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_XBox2DFlexibleHurt : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetFlexibleHurtBox(IntPtr l) {
		try {
			XBox2DFlexibleHurt self=(XBox2DFlexibleHurt)checkSelf(l);
			var ret=self.GetFlexibleHurtBox();
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
	static public int RegisterInBoxes(IntPtr l) {
		try {
			XBox2DFlexibleHurt self=(XBox2DFlexibleHurt)checkSelf(l);
			self.RegisterInBoxes();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int UnRegisterFromBoxes(IntPtr l) {
		try {
			XBox2DFlexibleHurt self=(XBox2DFlexibleHurt)checkSelf(l);
			self.UnRegisterFromBoxes();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SetActiveTypeInLua(IntPtr l) {
		try {
			XBox2DFlexibleHurt self=(XBox2DFlexibleHurt)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			self.SetActiveTypeInLua(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SetActiveIdInLua(IntPtr l) {
		try {
			XBox2DFlexibleHurt self=(XBox2DFlexibleHurt)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			self.SetActiveIdInLua(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetActiveID(IntPtr l) {
		try {
			XBox2DFlexibleHurt self=(XBox2DFlexibleHurt)checkSelf(l);
			var ret=self.GetActiveID();
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
	static public int get_hurtBox(IntPtr l) {
		try {
			XBox2DFlexibleHurt self=(XBox2DFlexibleHurt)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.hurtBox);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_hurtBox(IntPtr l) {
		try {
			XBox2DFlexibleHurt self=(XBox2DFlexibleHurt)checkSelf(l);
			XBox2D v;
			checkType(l,2,out v);
			self.hurtBox=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Trans(IntPtr l) {
		try {
			XBox2DFlexibleHurt self=(XBox2DFlexibleHurt)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Trans);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"XBox2DFlexibleHurt");
		addMember(l,GetFlexibleHurtBox);
		addMember(l,RegisterInBoxes);
		addMember(l,UnRegisterFromBoxes);
		addMember(l,SetActiveTypeInLua);
		addMember(l,SetActiveIdInLua);
		addMember(l,GetActiveID);
		addMember(l,"hurtBox",get_hurtBox,set_hurtBox,true);
		addMember(l,"Trans",get_Trans,null,true);
		createTypeMetatable(l,null, typeof(XBox2DFlexibleHurt),typeof(UnityEngine.MonoBehaviour));
	}
}
