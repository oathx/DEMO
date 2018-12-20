using System;
using System.Collections.Generic;
namespace SLua {
	[LuaBinder(3)]
	public class BindCustom {
		public static Action<IntPtr>[] GetBindList() {
			Action<IntPtr>[] list= {
				Lua_XActorComponent.reg,
				Lua_XActorComponent_BoneNogType.reg,
				Lua_XActorComponent_BoneNogPoint.reg,
				Lua_GUtility.reg,
				Lua_GameApp.reg,
				Lua_PacketType.reg,
				Lua_INetSession.reg,
				Lua_XNetLuaPacket.reg,
				Lua_XNetTcpSession.reg,
				Lua_TcpServerConfig.reg,
				Lua_XTcpServer.reg,
				Lua_UIEventTriggerListener.reg,
				Lua_XBox2DComponent.reg,
				Lua_XBox2DFlexibleHurt.reg,
				Lua_XBox2DSystem.reg,
				Lua_LuaBehaviourScript.reg,
				Lua_XPrefs.reg,
				Lua_XRes.reg,
				Lua_DissolveType.reg,
				Lua_XShaderEffect.reg,
				Lua_XShapeAssetObject.reg,
				Lua_XEase.reg,
				Lua_XLoopType.reg,
				Lua_XUpdateType.reg,
				Lua_XRotateMode.reg,
				Lua_XAxisConstraint.reg,
				Lua_XScrambleMode.reg,
				Lua_XTween.reg,
				Lua_XStaticDOTween.reg,
				Lua_XSequence.reg,
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
