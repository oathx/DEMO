using System;
using System.Collections.Generic;
namespace SLua {
	[LuaBinder(3)]
	public class BindCustom {
		public static Action<IntPtr>[] GetBindList() {
			Action<IntPtr>[] list= {
				Lua_GUtility.reg,
				Lua_GameApp.reg,
				Lua_LuaBehaviourScript.reg,
				Lua_PacketType.reg,
				Lua_INetSession.reg,
				Lua_NetLuaPacket.reg,
				Lua_NetTcpSession.reg,
				Lua_TcpServerConfig.reg,
				Lua_TcpServer.reg,
				Lua_ResourceManager.reg,
				Lua_DissolveType.reg,
				Lua_ShaderEffect.reg,
				Lua_ShapeAssetObject.reg,
				Lua_XEase.reg,
				Lua_XLoopType.reg,
				Lua_XUpdateType.reg,
				Lua_XRotateMode.reg,
				Lua_XAxisConstraint.reg,
				Lua_XScrambleMode.reg,
				Lua_XTween.reg,
				Lua_StaticDOTween.reg,
				Lua_XSequence.reg,
				Lua_UIEventTriggerListener.reg,
				Lua_Custom.reg,
				Lua_Custom_IFoo.reg,
				Lua_Deleg.reg,
				Lua_foostruct.reg,
				Lua_FloatEvent.reg,
				Lua_ListViewEvent.reg,
				Lua_SLuaTest.reg,
				Lua_System_Collections_Generic_List_1_int.reg,
				Lua_XXList.reg,
				Lua_AbsClass.reg,
				Lua_HelloWorld.reg,
				Lua_NewCoroutine.reg,
				Lua_CC_AnalogTV.reg,
				Lua_System_Collections_Generic_Dictionary_2_int_string.reg,
				Lua_System_String.reg,
			};
			return list;
		}
	}
}
