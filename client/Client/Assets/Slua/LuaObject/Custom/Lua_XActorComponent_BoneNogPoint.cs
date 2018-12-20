using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_XActorComponent_BoneNogPoint : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			XActorComponent.BoneNogPoint o;
			o=new XActorComponent.BoneNogPoint();
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
	static public int get_head(IntPtr l) {
		try {
			XActorComponent.BoneNogPoint self=(XActorComponent.BoneNogPoint)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.head);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_head(IntPtr l) {
		try {
			XActorComponent.BoneNogPoint self=(XActorComponent.BoneNogPoint)checkSelf(l);
			UnityEngine.Transform v;
			checkType(l,2,out v);
			self.head=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_hit(IntPtr l) {
		try {
			XActorComponent.BoneNogPoint self=(XActorComponent.BoneNogPoint)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.hit);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_hit(IntPtr l) {
		try {
			XActorComponent.BoneNogPoint self=(XActorComponent.BoneNogPoint)checkSelf(l);
			UnityEngine.Transform v;
			checkType(l,2,out v);
			self.hit=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_pelvis(IntPtr l) {
		try {
			XActorComponent.BoneNogPoint self=(XActorComponent.BoneNogPoint)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.pelvis);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_pelvis(IntPtr l) {
		try {
			XActorComponent.BoneNogPoint self=(XActorComponent.BoneNogPoint)checkSelf(l);
			UnityEngine.Transform v;
			checkType(l,2,out v);
			self.pelvis=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_center(IntPtr l) {
		try {
			XActorComponent.BoneNogPoint self=(XActorComponent.BoneNogPoint)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.center);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_center(IntPtr l) {
		try {
			XActorComponent.BoneNogPoint self=(XActorComponent.BoneNogPoint)checkSelf(l);
			UnityEngine.Transform v;
			checkType(l,2,out v);
			self.center=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_leftElbow(IntPtr l) {
		try {
			XActorComponent.BoneNogPoint self=(XActorComponent.BoneNogPoint)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.leftElbow);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_leftElbow(IntPtr l) {
		try {
			XActorComponent.BoneNogPoint self=(XActorComponent.BoneNogPoint)checkSelf(l);
			UnityEngine.Transform v;
			checkType(l,2,out v);
			self.leftElbow=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_rightElbow(IntPtr l) {
		try {
			XActorComponent.BoneNogPoint self=(XActorComponent.BoneNogPoint)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.rightElbow);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_rightElbow(IntPtr l) {
		try {
			XActorComponent.BoneNogPoint self=(XActorComponent.BoneNogPoint)checkSelf(l);
			UnityEngine.Transform v;
			checkType(l,2,out v);
			self.rightElbow=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_leftHand(IntPtr l) {
		try {
			XActorComponent.BoneNogPoint self=(XActorComponent.BoneNogPoint)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.leftHand);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_leftHand(IntPtr l) {
		try {
			XActorComponent.BoneNogPoint self=(XActorComponent.BoneNogPoint)checkSelf(l);
			UnityEngine.Transform v;
			checkType(l,2,out v);
			self.leftHand=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_rightHand(IntPtr l) {
		try {
			XActorComponent.BoneNogPoint self=(XActorComponent.BoneNogPoint)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.rightHand);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_rightHand(IntPtr l) {
		try {
			XActorComponent.BoneNogPoint self=(XActorComponent.BoneNogPoint)checkSelf(l);
			UnityEngine.Transform v;
			checkType(l,2,out v);
			self.rightHand=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_leftKnee(IntPtr l) {
		try {
			XActorComponent.BoneNogPoint self=(XActorComponent.BoneNogPoint)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.leftKnee);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_leftKnee(IntPtr l) {
		try {
			XActorComponent.BoneNogPoint self=(XActorComponent.BoneNogPoint)checkSelf(l);
			UnityEngine.Transform v;
			checkType(l,2,out v);
			self.leftKnee=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_rightKnee(IntPtr l) {
		try {
			XActorComponent.BoneNogPoint self=(XActorComponent.BoneNogPoint)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.rightKnee);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_rightKnee(IntPtr l) {
		try {
			XActorComponent.BoneNogPoint self=(XActorComponent.BoneNogPoint)checkSelf(l);
			UnityEngine.Transform v;
			checkType(l,2,out v);
			self.rightKnee=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_leftFoot(IntPtr l) {
		try {
			XActorComponent.BoneNogPoint self=(XActorComponent.BoneNogPoint)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.leftFoot);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_leftFoot(IntPtr l) {
		try {
			XActorComponent.BoneNogPoint self=(XActorComponent.BoneNogPoint)checkSelf(l);
			UnityEngine.Transform v;
			checkType(l,2,out v);
			self.leftFoot=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_rightFoot(IntPtr l) {
		try {
			XActorComponent.BoneNogPoint self=(XActorComponent.BoneNogPoint)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.rightFoot);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_rightFoot(IntPtr l) {
		try {
			XActorComponent.BoneNogPoint self=(XActorComponent.BoneNogPoint)checkSelf(l);
			UnityEngine.Transform v;
			checkType(l,2,out v);
			self.rightFoot=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_leftWeapon(IntPtr l) {
		try {
			XActorComponent.BoneNogPoint self=(XActorComponent.BoneNogPoint)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.leftWeapon);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_leftWeapon(IntPtr l) {
		try {
			XActorComponent.BoneNogPoint self=(XActorComponent.BoneNogPoint)checkSelf(l);
			UnityEngine.Transform v;
			checkType(l,2,out v);
			self.leftWeapon=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_rightWeapon(IntPtr l) {
		try {
			XActorComponent.BoneNogPoint self=(XActorComponent.BoneNogPoint)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.rightWeapon);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_rightWeapon(IntPtr l) {
		try {
			XActorComponent.BoneNogPoint self=(XActorComponent.BoneNogPoint)checkSelf(l);
			UnityEngine.Transform v;
			checkType(l,2,out v);
			self.rightWeapon=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_leftEye(IntPtr l) {
		try {
			XActorComponent.BoneNogPoint self=(XActorComponent.BoneNogPoint)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.leftEye);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_leftEye(IntPtr l) {
		try {
			XActorComponent.BoneNogPoint self=(XActorComponent.BoneNogPoint)checkSelf(l);
			UnityEngine.Transform v;
			checkType(l,2,out v);
			self.leftEye=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_rightEye(IntPtr l) {
		try {
			XActorComponent.BoneNogPoint self=(XActorComponent.BoneNogPoint)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.rightEye);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_rightEye(IntPtr l) {
		try {
			XActorComponent.BoneNogPoint self=(XActorComponent.BoneNogPoint)checkSelf(l);
			UnityEngine.Transform v;
			checkType(l,2,out v);
			self.rightEye=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_reserve1(IntPtr l) {
		try {
			XActorComponent.BoneNogPoint self=(XActorComponent.BoneNogPoint)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.reserve1);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_reserve1(IntPtr l) {
		try {
			XActorComponent.BoneNogPoint self=(XActorComponent.BoneNogPoint)checkSelf(l);
			UnityEngine.Transform v;
			checkType(l,2,out v);
			self.reserve1=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_reserve2(IntPtr l) {
		try {
			XActorComponent.BoneNogPoint self=(XActorComponent.BoneNogPoint)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.reserve2);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_reserve2(IntPtr l) {
		try {
			XActorComponent.BoneNogPoint self=(XActorComponent.BoneNogPoint)checkSelf(l);
			UnityEngine.Transform v;
			checkType(l,2,out v);
			self.reserve2=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_reserve3(IntPtr l) {
		try {
			XActorComponent.BoneNogPoint self=(XActorComponent.BoneNogPoint)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.reserve3);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_reserve3(IntPtr l) {
		try {
			XActorComponent.BoneNogPoint self=(XActorComponent.BoneNogPoint)checkSelf(l);
			UnityEngine.Transform v;
			checkType(l,2,out v);
			self.reserve3=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_leftshoulderArmor(IntPtr l) {
		try {
			XActorComponent.BoneNogPoint self=(XActorComponent.BoneNogPoint)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.leftshoulderArmor);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_leftshoulderArmor(IntPtr l) {
		try {
			XActorComponent.BoneNogPoint self=(XActorComponent.BoneNogPoint)checkSelf(l);
			UnityEngine.Transform v;
			checkType(l,2,out v);
			self.leftshoulderArmor=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_rightshoulderArmor(IntPtr l) {
		try {
			XActorComponent.BoneNogPoint self=(XActorComponent.BoneNogPoint)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.rightshoulderArmor);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_rightshoulderArmor(IntPtr l) {
		try {
			XActorComponent.BoneNogPoint self=(XActorComponent.BoneNogPoint)checkSelf(l);
			UnityEngine.Transform v;
			checkType(l,2,out v);
			self.rightshoulderArmor=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_leftUpperArm(IntPtr l) {
		try {
			XActorComponent.BoneNogPoint self=(XActorComponent.BoneNogPoint)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.leftUpperArm);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_leftUpperArm(IntPtr l) {
		try {
			XActorComponent.BoneNogPoint self=(XActorComponent.BoneNogPoint)checkSelf(l);
			UnityEngine.Transform v;
			checkType(l,2,out v);
			self.leftUpperArm=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_rightUpperArm(IntPtr l) {
		try {
			XActorComponent.BoneNogPoint self=(XActorComponent.BoneNogPoint)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.rightUpperArm);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_rightUpperArm(IntPtr l) {
		try {
			XActorComponent.BoneNogPoint self=(XActorComponent.BoneNogPoint)checkSelf(l);
			UnityEngine.Transform v;
			checkType(l,2,out v);
			self.rightUpperArm=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_leftForearm(IntPtr l) {
		try {
			XActorComponent.BoneNogPoint self=(XActorComponent.BoneNogPoint)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.leftForearm);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_leftForearm(IntPtr l) {
		try {
			XActorComponent.BoneNogPoint self=(XActorComponent.BoneNogPoint)checkSelf(l);
			UnityEngine.Transform v;
			checkType(l,2,out v);
			self.leftForearm=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_rightForearm(IntPtr l) {
		try {
			XActorComponent.BoneNogPoint self=(XActorComponent.BoneNogPoint)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.rightForearm);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_rightForearm(IntPtr l) {
		try {
			XActorComponent.BoneNogPoint self=(XActorComponent.BoneNogPoint)checkSelf(l);
			UnityEngine.Transform v;
			checkType(l,2,out v);
			self.rightForearm=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_spine(IntPtr l) {
		try {
			XActorComponent.BoneNogPoint self=(XActorComponent.BoneNogPoint)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.spine);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_spine(IntPtr l) {
		try {
			XActorComponent.BoneNogPoint self=(XActorComponent.BoneNogPoint)checkSelf(l);
			UnityEngine.Transform v;
			checkType(l,2,out v);
			self.spine=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_spine1(IntPtr l) {
		try {
			XActorComponent.BoneNogPoint self=(XActorComponent.BoneNogPoint)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.spine1);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_spine1(IntPtr l) {
		try {
			XActorComponent.BoneNogPoint self=(XActorComponent.BoneNogPoint)checkSelf(l);
			UnityEngine.Transform v;
			checkType(l,2,out v);
			self.spine1=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_spine2(IntPtr l) {
		try {
			XActorComponent.BoneNogPoint self=(XActorComponent.BoneNogPoint)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.spine2);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_spine2(IntPtr l) {
		try {
			XActorComponent.BoneNogPoint self=(XActorComponent.BoneNogPoint)checkSelf(l);
			UnityEngine.Transform v;
			checkType(l,2,out v);
			self.spine2=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_leftThigh(IntPtr l) {
		try {
			XActorComponent.BoneNogPoint self=(XActorComponent.BoneNogPoint)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.leftThigh);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_leftThigh(IntPtr l) {
		try {
			XActorComponent.BoneNogPoint self=(XActorComponent.BoneNogPoint)checkSelf(l);
			UnityEngine.Transform v;
			checkType(l,2,out v);
			self.leftThigh=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_rightThigh(IntPtr l) {
		try {
			XActorComponent.BoneNogPoint self=(XActorComponent.BoneNogPoint)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.rightThigh);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_rightThigh(IntPtr l) {
		try {
			XActorComponent.BoneNogPoint self=(XActorComponent.BoneNogPoint)checkSelf(l);
			UnityEngine.Transform v;
			checkType(l,2,out v);
			self.rightThigh=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_leftCalf(IntPtr l) {
		try {
			XActorComponent.BoneNogPoint self=(XActorComponent.BoneNogPoint)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.leftCalf);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_leftCalf(IntPtr l) {
		try {
			XActorComponent.BoneNogPoint self=(XActorComponent.BoneNogPoint)checkSelf(l);
			UnityEngine.Transform v;
			checkType(l,2,out v);
			self.leftCalf=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_rightCalf(IntPtr l) {
		try {
			XActorComponent.BoneNogPoint self=(XActorComponent.BoneNogPoint)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.rightCalf);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_rightCalf(IntPtr l) {
		try {
			XActorComponent.BoneNogPoint self=(XActorComponent.BoneNogPoint)checkSelf(l);
			UnityEngine.Transform v;
			checkType(l,2,out v);
			self.rightCalf=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"XActorComponent.BoneNogPoint");
		addMember(l,"head",get_head,set_head,true);
		addMember(l,"hit",get_hit,set_hit,true);
		addMember(l,"pelvis",get_pelvis,set_pelvis,true);
		addMember(l,"center",get_center,set_center,true);
		addMember(l,"leftElbow",get_leftElbow,set_leftElbow,true);
		addMember(l,"rightElbow",get_rightElbow,set_rightElbow,true);
		addMember(l,"leftHand",get_leftHand,set_leftHand,true);
		addMember(l,"rightHand",get_rightHand,set_rightHand,true);
		addMember(l,"leftKnee",get_leftKnee,set_leftKnee,true);
		addMember(l,"rightKnee",get_rightKnee,set_rightKnee,true);
		addMember(l,"leftFoot",get_leftFoot,set_leftFoot,true);
		addMember(l,"rightFoot",get_rightFoot,set_rightFoot,true);
		addMember(l,"leftWeapon",get_leftWeapon,set_leftWeapon,true);
		addMember(l,"rightWeapon",get_rightWeapon,set_rightWeapon,true);
		addMember(l,"leftEye",get_leftEye,set_leftEye,true);
		addMember(l,"rightEye",get_rightEye,set_rightEye,true);
		addMember(l,"reserve1",get_reserve1,set_reserve1,true);
		addMember(l,"reserve2",get_reserve2,set_reserve2,true);
		addMember(l,"reserve3",get_reserve3,set_reserve3,true);
		addMember(l,"leftshoulderArmor",get_leftshoulderArmor,set_leftshoulderArmor,true);
		addMember(l,"rightshoulderArmor",get_rightshoulderArmor,set_rightshoulderArmor,true);
		addMember(l,"leftUpperArm",get_leftUpperArm,set_leftUpperArm,true);
		addMember(l,"rightUpperArm",get_rightUpperArm,set_rightUpperArm,true);
		addMember(l,"leftForearm",get_leftForearm,set_leftForearm,true);
		addMember(l,"rightForearm",get_rightForearm,set_rightForearm,true);
		addMember(l,"spine",get_spine,set_spine,true);
		addMember(l,"spine1",get_spine1,set_spine1,true);
		addMember(l,"spine2",get_spine2,set_spine2,true);
		addMember(l,"leftThigh",get_leftThigh,set_leftThigh,true);
		addMember(l,"rightThigh",get_rightThigh,set_rightThigh,true);
		addMember(l,"leftCalf",get_leftCalf,set_leftCalf,true);
		addMember(l,"rightCalf",get_rightCalf,set_rightCalf,true);
		createTypeMetatable(l,constructor, typeof(XActorComponent.BoneNogPoint));
	}
}
