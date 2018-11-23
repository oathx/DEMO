using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UIEventTriggerListener : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int OnPointerClick(IntPtr l) {
		try {
			UIEventTriggerListener self=(UIEventTriggerListener)checkSelf(l);
			UnityEngine.EventSystems.PointerEventData a1;
			checkType(l,2,out a1);
			self.OnPointerClick(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int OnPointerDown(IntPtr l) {
		try {
			UIEventTriggerListener self=(UIEventTriggerListener)checkSelf(l);
			UnityEngine.EventSystems.PointerEventData a1;
			checkType(l,2,out a1);
			self.OnPointerDown(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int OnPointerEnter(IntPtr l) {
		try {
			UIEventTriggerListener self=(UIEventTriggerListener)checkSelf(l);
			UnityEngine.EventSystems.PointerEventData a1;
			checkType(l,2,out a1);
			self.OnPointerEnter(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int OnPointerExit(IntPtr l) {
		try {
			UIEventTriggerListener self=(UIEventTriggerListener)checkSelf(l);
			UnityEngine.EventSystems.PointerEventData a1;
			checkType(l,2,out a1);
			self.OnPointerExit(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int OnPointerUp(IntPtr l) {
		try {
			UIEventTriggerListener self=(UIEventTriggerListener)checkSelf(l);
			UnityEngine.EventSystems.PointerEventData a1;
			checkType(l,2,out a1);
			self.OnPointerUp(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int OnSelect(IntPtr l) {
		try {
			UIEventTriggerListener self=(UIEventTriggerListener)checkSelf(l);
			UnityEngine.EventSystems.BaseEventData a1;
			checkType(l,2,out a1);
			self.OnSelect(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int OnUpdateSelected(IntPtr l) {
		try {
			UIEventTriggerListener self=(UIEventTriggerListener)checkSelf(l);
			UnityEngine.EventSystems.BaseEventData a1;
			checkType(l,2,out a1);
			self.OnUpdateSelected(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int OnBeginDrag(IntPtr l) {
		try {
			UIEventTriggerListener self=(UIEventTriggerListener)checkSelf(l);
			UnityEngine.EventSystems.PointerEventData a1;
			checkType(l,2,out a1);
			self.OnBeginDrag(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int OnDrag(IntPtr l) {
		try {
			UIEventTriggerListener self=(UIEventTriggerListener)checkSelf(l);
			UnityEngine.EventSystems.PointerEventData a1;
			checkType(l,2,out a1);
			self.OnDrag(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int OnEndDrag(IntPtr l) {
		try {
			UIEventTriggerListener self=(UIEventTriggerListener)checkSelf(l);
			UnityEngine.EventSystems.PointerEventData a1;
			checkType(l,2,out a1);
			self.OnEndDrag(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Get_s(IntPtr l) {
		try {
			UnityEngine.GameObject a1;
			checkType(l,1,out a1);
			SLua.LuaTable a2;
			checkType(l,2,out a2);
			var ret=UIEventTriggerListener.Get(a1,a2);
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
	static public int set_onClick(IntPtr l) {
		try {
			UIEventTriggerListener self=(UIEventTriggerListener)checkSelf(l);
			UIEventTriggerListener.VoidDelegate v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.onClick=v;
			else if(op==1) self.onClick+=v;
			else if(op==2) self.onClick-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_onDown(IntPtr l) {
		try {
			UIEventTriggerListener self=(UIEventTriggerListener)checkSelf(l);
			UIEventTriggerListener.VoidDelegate v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.onDown=v;
			else if(op==1) self.onDown+=v;
			else if(op==2) self.onDown-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_onEnter(IntPtr l) {
		try {
			UIEventTriggerListener self=(UIEventTriggerListener)checkSelf(l);
			UIEventTriggerListener.VoidDelegate v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.onEnter=v;
			else if(op==1) self.onEnter+=v;
			else if(op==2) self.onEnter-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_onExit(IntPtr l) {
		try {
			UIEventTriggerListener self=(UIEventTriggerListener)checkSelf(l);
			UIEventTriggerListener.VoidDelegate v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.onExit=v;
			else if(op==1) self.onExit+=v;
			else if(op==2) self.onExit-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_onUp(IntPtr l) {
		try {
			UIEventTriggerListener self=(UIEventTriggerListener)checkSelf(l);
			UIEventTriggerListener.VoidDelegate v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.onUp=v;
			else if(op==1) self.onUp+=v;
			else if(op==2) self.onUp-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_onSelect(IntPtr l) {
		try {
			UIEventTriggerListener self=(UIEventTriggerListener)checkSelf(l);
			UIEventTriggerListener.VoidDelegate v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.onSelect=v;
			else if(op==1) self.onSelect+=v;
			else if(op==2) self.onSelect-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_onUpdateSelect(IntPtr l) {
		try {
			UIEventTriggerListener self=(UIEventTriggerListener)checkSelf(l);
			UIEventTriggerListener.VoidDelegate v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.onUpdateSelect=v;
			else if(op==1) self.onUpdateSelect+=v;
			else if(op==2) self.onUpdateSelect-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_onBeginDrag(IntPtr l) {
		try {
			UIEventTriggerListener self=(UIEventTriggerListener)checkSelf(l);
			UIEventTriggerListener.VoidDelegate v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.onBeginDrag=v;
			else if(op==1) self.onBeginDrag+=v;
			else if(op==2) self.onBeginDrag-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_onDrag(IntPtr l) {
		try {
			UIEventTriggerListener self=(UIEventTriggerListener)checkSelf(l);
			UIEventTriggerListener.VoidDelegate v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.onDrag=v;
			else if(op==1) self.onDrag+=v;
			else if(op==2) self.onDrag-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_onEndDrag(IntPtr l) {
		try {
			UIEventTriggerListener self=(UIEventTriggerListener)checkSelf(l);
			UIEventTriggerListener.VoidDelegate v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.onEndDrag=v;
			else if(op==1) self.onEndDrag+=v;
			else if(op==2) self.onEndDrag-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_luaModule(IntPtr l) {
		try {
			UIEventTriggerListener self=(UIEventTriggerListener)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.luaModule);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_luaModule(IntPtr l) {
		try {
			UIEventTriggerListener self=(UIEventTriggerListener)checkSelf(l);
			SLua.LuaTable v;
			checkType(l,2,out v);
			self.luaModule=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"UIEventTriggerListener");
		addMember(l,OnPointerClick);
		addMember(l,OnPointerDown);
		addMember(l,OnPointerEnter);
		addMember(l,OnPointerExit);
		addMember(l,OnPointerUp);
		addMember(l,OnSelect);
		addMember(l,OnUpdateSelected);
		addMember(l,OnBeginDrag);
		addMember(l,OnDrag);
		addMember(l,OnEndDrag);
		addMember(l,Get_s);
		addMember(l,"onClick",null,set_onClick,true);
		addMember(l,"onDown",null,set_onDown,true);
		addMember(l,"onEnter",null,set_onEnter,true);
		addMember(l,"onExit",null,set_onExit,true);
		addMember(l,"onUp",null,set_onUp,true);
		addMember(l,"onSelect",null,set_onSelect,true);
		addMember(l,"onUpdateSelect",null,set_onUpdateSelect,true);
		addMember(l,"onBeginDrag",null,set_onBeginDrag,true);
		addMember(l,"onDrag",null,set_onDrag,true);
		addMember(l,"onEndDrag",null,set_onEndDrag,true);
		addMember(l,"luaModule",get_luaModule,set_luaModule,true);
		createTypeMetatable(l,null, typeof(UIEventTriggerListener),typeof(UnityEngine.EventSystems.EventTrigger));
	}
}
