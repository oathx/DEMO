using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_XActorComponent : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int generateBoneNogPoint(IntPtr l) {
		try {
			XActorComponent self=(XActorComponent)checkSelf(l);
			self.generateBoneNogPoint();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetBoneNogPoint(IntPtr l) {
		try {
			XActorComponent self=(XActorComponent)checkSelf(l);
			XActorComponent.BoneNogType a1;
			checkEnum(l,2,out a1);
			var ret=self.GetBoneNogPoint(a1);
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
	static public int GetAnimator(IntPtr l) {
		try {
			XActorComponent self=(XActorComponent)checkSelf(l);
			var ret=self.GetAnimator();
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
	static public int SetAnimatorOverrideAnimation(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(matchType(l,argc,2,typeof(string),typeof(string))){
				XActorComponent self=(XActorComponent)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				System.String a2;
				checkType(l,3,out a2);
				self.SetAnimatorOverrideAnimation(a1,a2);
				pushValue(l,true);
				return 1;
			}
			else if(matchType(l,argc,2,typeof(string),typeof(UnityEngine.AnimationClip))){
				XActorComponent self=(XActorComponent)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				UnityEngine.AnimationClip a2;
				checkType(l,3,out a2);
				self.SetAnimatorOverrideAnimation(a1,a2);
				pushValue(l,true);
				return 1;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function SetAnimatorOverrideAnimation to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int RevertOverrideAnimation(IntPtr l) {
		try {
			XActorComponent self=(XActorComponent)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			self.RevertOverrideAnimation(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetCurrentAnimatorState(IntPtr l) {
		try {
			XActorComponent self=(XActorComponent)checkSelf(l);
			var ret=self.GetCurrentAnimatorState();
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
	static public int get_nogPoint(IntPtr l) {
		try {
			XActorComponent self=(XActorComponent)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.nogPoint);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_nogPoint(IntPtr l) {
		try {
			XActorComponent self=(XActorComponent)checkSelf(l);
			XActorComponent.BoneNogPoint v;
			checkType(l,2,out v);
			self.nogPoint=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_checkPoint(IntPtr l) {
		try {
			XActorComponent self=(XActorComponent)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.checkPoint);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_checkPoint(IntPtr l) {
		try {
			XActorComponent self=(XActorComponent)checkSelf(l);
			UnityEngine.GameObject v;
			checkType(l,2,out v);
			self.checkPoint=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_overridedAnimation(IntPtr l) {
		try {
			XActorComponent self=(XActorComponent)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.overridedAnimation);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_overridedAnimation(IntPtr l) {
		try {
			XActorComponent self=(XActorComponent)checkSelf(l);
			System.Collections.Generic.Dictionary<System.String,UnityEngine.AnimationClip> v;
			checkType(l,2,out v);
			self.overridedAnimation=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_animator(IntPtr l) {
		try {
			XActorComponent self=(XActorComponent)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.animator);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_animator(IntPtr l) {
		try {
			XActorComponent self=(XActorComponent)checkSelf(l);
			UnityEngine.Animator v;
			checkType(l,2,out v);
			self.animator=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"XActorComponent");
		addMember(l,generateBoneNogPoint);
		addMember(l,GetBoneNogPoint);
		addMember(l,GetAnimator);
		addMember(l,SetAnimatorOverrideAnimation);
		addMember(l,RevertOverrideAnimation);
		addMember(l,GetCurrentAnimatorState);
		addMember(l,"nogPoint",get_nogPoint,set_nogPoint,true);
		addMember(l,"checkPoint",get_checkPoint,set_checkPoint,true);
		addMember(l,"overridedAnimation",get_overridedAnimation,set_overridedAnimation,true);
		addMember(l,"animator",get_animator,set_animator,true);
		createTypeMetatable(l,null, typeof(XActorComponent),typeof(UnityEngine.MonoBehaviour));
	}
}
