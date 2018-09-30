using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_XTween : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			XTween o;
			if(argc==1){
				o=new XTween();
				pushValue(l,true);
				pushValue(l,o);
				return 2;
			}
			else if(argc==2){
				DG.Tweening.Tween a1;
				checkType(l,2,out a1);
				o=new XTween(a1);
				pushValue(l,true);
				pushValue(l,o);
				return 2;
			}
			return error(l,"New object failed.");
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SetAS(IntPtr l) {
		try {
			XTween self=(XTween)checkSelf(l);
			XTween a1;
			checkType(l,2,out a1);
			var ret=self.SetAS(a1);
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
	static public int SetAutoKill(IntPtr l) {
		try {
			XTween self=(XTween)checkSelf(l);
			System.Boolean a1;
			checkType(l,2,out a1);
			var ret=self.SetAutoKill(a1);
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
	static public int SetEase(IntPtr l) {
		try {
			XTween self=(XTween)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			var ret=self.SetEase(a1);
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
	static public int SetId(IntPtr l) {
		try {
			XTween self=(XTween)checkSelf(l);
			System.Object a1;
			checkType(l,2,out a1);
			var ret=self.SetId(a1);
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
	static public int SetLoops(IntPtr l) {
		try {
			XTween self=(XTween)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			System.Int32 a2;
			checkType(l,3,out a2);
			var ret=self.SetLoops(a1,a2);
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
	static public int SetRecyclable(IntPtr l) {
		try {
			XTween self=(XTween)checkSelf(l);
			System.Boolean a1;
			checkType(l,2,out a1);
			var ret=self.SetRecyclable(a1);
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
	static public int SetRelative(IntPtr l) {
		try {
			XTween self=(XTween)checkSelf(l);
			System.Boolean a1;
			checkType(l,2,out a1);
			var ret=self.SetRelative(a1);
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
	static public int SetUpdate(IntPtr l) {
		try {
			XTween self=(XTween)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			System.Boolean a2;
			checkType(l,3,out a2);
			var ret=self.SetUpdate(a1,a2);
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
	static public int SetDelay(IntPtr l) {
		try {
			XTween self=(XTween)checkSelf(l);
			System.Single a1;
			checkType(l,2,out a1);
			var ret=self.SetDelay(a1);
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
	static public int SetSpeedBased(IntPtr l) {
		try {
			XTween self=(XTween)checkSelf(l);
			System.Boolean a1;
			checkType(l,2,out a1);
			var ret=self.SetSpeedBased(a1);
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
	static public int From(IntPtr l) {
		try {
			XTween self=(XTween)checkSelf(l);
			System.Boolean a1;
			checkType(l,2,out a1);
			var ret=self.From(a1);
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
	static public int ChangeStartValue(IntPtr l) {
		try {
			XTween self=(XTween)checkSelf(l);
			System.Object a1;
			checkType(l,2,out a1);
			var ret=self.ChangeStartValue(a1);
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
	static public int ChangeValues(IntPtr l) {
		try {
			XTween self=(XTween)checkSelf(l);
			System.Object a1;
			checkType(l,2,out a1);
			System.Object a2;
			checkType(l,3,out a2);
			var ret=self.ChangeValues(a1,a2);
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
	static public int ChangeEndValue(IntPtr l) {
		try {
			XTween self=(XTween)checkSelf(l);
			System.Object a1;
			checkType(l,2,out a1);
			System.Single a2;
			checkType(l,3,out a2);
			System.Boolean a3;
			checkType(l,4,out a3);
			var ret=self.ChangeEndValue(a1,a2,a3);
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
	static public int OnComplete(IntPtr l) {
		try {
			XTween self=(XTween)checkSelf(l);
			SLua.LuaFunction a1;
			checkType(l,2,out a1);
			var ret=self.OnComplete(a1);
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
	static public int OnKill(IntPtr l) {
		try {
			XTween self=(XTween)checkSelf(l);
			SLua.LuaFunction a1;
			checkType(l,2,out a1);
			var ret=self.OnKill(a1);
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
	static public int OnPlay(IntPtr l) {
		try {
			XTween self=(XTween)checkSelf(l);
			SLua.LuaFunction a1;
			checkType(l,2,out a1);
			var ret=self.OnPlay(a1);
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
	static public int OnPause(IntPtr l) {
		try {
			XTween self=(XTween)checkSelf(l);
			SLua.LuaFunction a1;
			checkType(l,2,out a1);
			var ret=self.OnPause(a1);
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
	static public int OnRewind(IntPtr l) {
		try {
			XTween self=(XTween)checkSelf(l);
			SLua.LuaFunction a1;
			checkType(l,2,out a1);
			var ret=self.OnRewind(a1);
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
	static public int OnStart(IntPtr l) {
		try {
			XTween self=(XTween)checkSelf(l);
			SLua.LuaFunction a1;
			checkType(l,2,out a1);
			var ret=self.OnStart(a1);
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
	static public int OnStepComplete(IntPtr l) {
		try {
			XTween self=(XTween)checkSelf(l);
			SLua.LuaFunction a1;
			checkType(l,2,out a1);
			var ret=self.OnStepComplete(a1);
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
	static public int OnUpdate(IntPtr l) {
		try {
			XTween self=(XTween)checkSelf(l);
			SLua.LuaFunction a1;
			checkType(l,2,out a1);
			var ret=self.OnUpdate(a1);
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
	static public int OnWaypointChange(IntPtr l) {
		try {
			XTween self=(XTween)checkSelf(l);
			SLua.LuaFunction a1;
			checkType(l,2,out a1);
			var ret=self.OnWaypointChange(a1);
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
	static public int Complete(IntPtr l) {
		try {
			XTween self=(XTween)checkSelf(l);
			self.Complete();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Flip(IntPtr l) {
		try {
			XTween self=(XTween)checkSelf(l);
			self.Flip();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Goto(IntPtr l) {
		try {
			XTween self=(XTween)checkSelf(l);
			System.Single a1;
			checkType(l,2,out a1);
			System.Boolean a2;
			checkType(l,3,out a2);
			self.Goto(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Kill(IntPtr l) {
		try {
			XTween self=(XTween)checkSelf(l);
			System.Boolean a1;
			checkType(l,2,out a1);
			self.Kill(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Pause(IntPtr l) {
		try {
			XTween self=(XTween)checkSelf(l);
			self.Pause();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Play(IntPtr l) {
		try {
			XTween self=(XTween)checkSelf(l);
			self.Play();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int PlayBackwards(IntPtr l) {
		try {
			XTween self=(XTween)checkSelf(l);
			self.PlayBackwards();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int PlayForward(IntPtr l) {
		try {
			XTween self=(XTween)checkSelf(l);
			self.PlayForward();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Restart(IntPtr l) {
		try {
			XTween self=(XTween)checkSelf(l);
			System.Boolean a1;
			checkType(l,2,out a1);
			self.Restart(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Rewind(IntPtr l) {
		try {
			XTween self=(XTween)checkSelf(l);
			System.Boolean a1;
			checkType(l,2,out a1);
			self.Rewind(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SmoothRewind(IntPtr l) {
		try {
			XTween self=(XTween)checkSelf(l);
			self.SmoothRewind();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int TogglePause(IntPtr l) {
		try {
			XTween self=(XTween)checkSelf(l);
			self.TogglePause();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ForceInit(IntPtr l) {
		try {
			XTween self=(XTween)checkSelf(l);
			self.ForceInit();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GotoWaypoint(IntPtr l) {
		try {
			XTween self=(XTween)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			System.Boolean a2;
			checkType(l,3,out a2);
			self.GotoWaypoint(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int fullPosition(IntPtr l) {
		try {
			XTween self=(XTween)checkSelf(l);
			var ret=self.fullPosition();
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
	static public int CompletedLoops(IntPtr l) {
		try {
			XTween self=(XTween)checkSelf(l);
			var ret=self.CompletedLoops();
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
	static public int Delay(IntPtr l) {
		try {
			XTween self=(XTween)checkSelf(l);
			var ret=self.Delay();
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
	static public int Duration(IntPtr l) {
		try {
			XTween self=(XTween)checkSelf(l);
			System.Boolean a1;
			checkType(l,2,out a1);
			var ret=self.Duration(a1);
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
	static public int Elapsed(IntPtr l) {
		try {
			XTween self=(XTween)checkSelf(l);
			System.Boolean a1;
			checkType(l,2,out a1);
			var ret=self.Elapsed(a1);
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
	static public int ElapsedDirectionalPercentage(IntPtr l) {
		try {
			XTween self=(XTween)checkSelf(l);
			var ret=self.ElapsedDirectionalPercentage();
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
	static public int ElapsedPercentage(IntPtr l) {
		try {
			XTween self=(XTween)checkSelf(l);
			System.Boolean a1;
			checkType(l,2,out a1);
			var ret=self.ElapsedPercentage(a1);
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
	static public int IsActive(IntPtr l) {
		try {
			XTween self=(XTween)checkSelf(l);
			var ret=self.IsActive();
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
	static public int IsBackwards(IntPtr l) {
		try {
			XTween self=(XTween)checkSelf(l);
			var ret=self.IsBackwards();
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
	static public int IsComplete(IntPtr l) {
		try {
			XTween self=(XTween)checkSelf(l);
			var ret=self.IsComplete();
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
	static public int IsInitialized(IntPtr l) {
		try {
			XTween self=(XTween)checkSelf(l);
			var ret=self.IsInitialized();
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
	static public int IsPlaying(IntPtr l) {
		try {
			XTween self=(XTween)checkSelf(l);
			var ret=self.IsPlaying();
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
	static public int Loops(IntPtr l) {
		try {
			XTween self=(XTween)checkSelf(l);
			var ret=self.Loops();
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
	static public int get_tween(IntPtr l) {
		try {
			XTween self=(XTween)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.tween);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_tween(IntPtr l) {
		try {
			XTween self=(XTween)checkSelf(l);
			DG.Tweening.Tween v;
			checkType(l,2,out v);
			self.tween=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"XTween");
		addMember(l,SetAS);
		addMember(l,SetAutoKill);
		addMember(l,SetEase);
		addMember(l,SetId);
		addMember(l,SetLoops);
		addMember(l,SetRecyclable);
		addMember(l,SetRelative);
		addMember(l,SetUpdate);
		addMember(l,SetDelay);
		addMember(l,SetSpeedBased);
		addMember(l,From);
		addMember(l,ChangeStartValue);
		addMember(l,ChangeValues);
		addMember(l,ChangeEndValue);
		addMember(l,OnComplete);
		addMember(l,OnKill);
		addMember(l,OnPlay);
		addMember(l,OnPause);
		addMember(l,OnRewind);
		addMember(l,OnStart);
		addMember(l,OnStepComplete);
		addMember(l,OnUpdate);
		addMember(l,OnWaypointChange);
		addMember(l,Complete);
		addMember(l,Flip);
		addMember(l,Goto);
		addMember(l,Kill);
		addMember(l,Pause);
		addMember(l,Play);
		addMember(l,PlayBackwards);
		addMember(l,PlayForward);
		addMember(l,Restart);
		addMember(l,Rewind);
		addMember(l,SmoothRewind);
		addMember(l,TogglePause);
		addMember(l,ForceInit);
		addMember(l,GotoWaypoint);
		addMember(l,fullPosition);
		addMember(l,CompletedLoops);
		addMember(l,Delay);
		addMember(l,Duration);
		addMember(l,Elapsed);
		addMember(l,ElapsedDirectionalPercentage);
		addMember(l,ElapsedPercentage);
		addMember(l,IsActive);
		addMember(l,IsBackwards);
		addMember(l,IsComplete);
		addMember(l,IsInitialized);
		addMember(l,IsPlaying);
		addMember(l,Loops);
		addMember(l,"tween",get_tween,set_tween,true);
		createTypeMetatable(l,constructor, typeof(XTween));
	}
}
