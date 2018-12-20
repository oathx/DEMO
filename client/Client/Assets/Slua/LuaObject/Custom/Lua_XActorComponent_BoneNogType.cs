using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_XActorComponent_BoneNogType : LuaObject {
	static public void reg(IntPtr l) {
		getEnumTable(l,"XActorComponent.BoneNogType");
		addMember(l,0,"None");
		addMember(l,1,"RHand");
		addMember(l,2,"LHand");
		addMember(l,3,"Hit");
		addMember(l,4,"RFoot");
		addMember(l,5,"LFoot");
		addMember(l,6,"Center");
		addMember(l,7,"Head");
		addMember(l,8,"Pelvis");
		addMember(l,9,"RElbow");
		addMember(l,10,"LElbow");
		addMember(l,11,"RKnee");
		addMember(l,12,"LKnee");
		addMember(l,13,"RWeapon");
		addMember(l,14,"LWeapon");
		addMember(l,15,"REye");
		addMember(l,16,"LEye");
		addMember(l,17,"Reserve1");
		addMember(l,18,"Reserve2");
		addMember(l,19,"Reserve3");
		addMember(l,20,"LshoulderArmor");
		addMember(l,21,"RshoulderArmor");
		addMember(l,22,"LUpperArm");
		addMember(l,23,"RUpperArm");
		addMember(l,24,"LForearm");
		addMember(l,25,"RForearm");
		addMember(l,26,"Spine");
		addMember(l,27,"Spine1");
		addMember(l,28,"Spine2");
		addMember(l,29,"LThigh");
		addMember(l,30,"RThigh");
		addMember(l,31,"LCalf");
		addMember(l,32,"RCalf");
		LuaDLL.lua_pop(l, 1);
	}
}
