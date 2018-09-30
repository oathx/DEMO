using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_NetLuaPacket : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			NetLuaPacket o;
			System.Int32 a1;
			checkType(l,2,out a1);
			o=new NetLuaPacket(a1);
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
	static public int Type(IntPtr l) {
		try {
			NetLuaPacket self=(NetLuaPacket)checkSelf(l);
			var ret=self.Type();
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
	static public int Size(IntPtr l) {
		try {
			NetLuaPacket self=(NetLuaPacket)checkSelf(l);
			var ret=self.Size();
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
	static public int SetOffset(IntPtr l) {
		try {
			NetLuaPacket self=(NetLuaPacket)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			self.SetOffset(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Finish(IntPtr l) {
		try {
			NetLuaPacket self=(NetLuaPacket)checkSelf(l);
			self.Finish();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ReadChar(IntPtr l) {
		try {
			NetLuaPacket self=(NetLuaPacket)checkSelf(l);
			var ret=self.ReadChar();
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
	static public int ReadByte(IntPtr l) {
		try {
			NetLuaPacket self=(NetLuaPacket)checkSelf(l);
			var ret=self.ReadByte();
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
	static public int ReadShort(IntPtr l) {
		try {
			NetLuaPacket self=(NetLuaPacket)checkSelf(l);
			var ret=self.ReadShort();
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
	static public int ReadUShort(IntPtr l) {
		try {
			NetLuaPacket self=(NetLuaPacket)checkSelf(l);
			var ret=self.ReadUShort();
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
	static public int ReadInt(IntPtr l) {
		try {
			NetLuaPacket self=(NetLuaPacket)checkSelf(l);
			var ret=self.ReadInt();
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
	static public int ReadUInt(IntPtr l) {
		try {
			NetLuaPacket self=(NetLuaPacket)checkSelf(l);
			var ret=self.ReadUInt();
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
	static public int ReadString(IntPtr l) {
		try {
			NetLuaPacket self=(NetLuaPacket)checkSelf(l);
			var ret=self.ReadString();
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
	static public int ReadBlock(IntPtr l) {
		try {
			NetLuaPacket self=(NetLuaPacket)checkSelf(l);
			var ret=self.ReadBlock();
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
	static public int ReadFloat(IntPtr l) {
		try {
			NetLuaPacket self=(NetLuaPacket)checkSelf(l);
			var ret=self.ReadFloat();
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
	static public int WriteChar(IntPtr l) {
		try {
			NetLuaPacket self=(NetLuaPacket)checkSelf(l);
			System.Char a1;
			checkType(l,2,out a1);
			self.WriteChar(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int WriteByte(IntPtr l) {
		try {
			NetLuaPacket self=(NetLuaPacket)checkSelf(l);
			System.Byte a1;
			checkType(l,2,out a1);
			self.WriteByte(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int WriteShort(IntPtr l) {
		try {
			NetLuaPacket self=(NetLuaPacket)checkSelf(l);
			System.Int16 a1;
			checkType(l,2,out a1);
			self.WriteShort(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int WriteUShort(IntPtr l) {
		try {
			NetLuaPacket self=(NetLuaPacket)checkSelf(l);
			System.UInt16 a1;
			checkType(l,2,out a1);
			self.WriteUShort(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int WriteInt(IntPtr l) {
		try {
			NetLuaPacket self=(NetLuaPacket)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			self.WriteInt(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int WriteUInt(IntPtr l) {
		try {
			NetLuaPacket self=(NetLuaPacket)checkSelf(l);
			System.UInt32 a1;
			checkType(l,2,out a1);
			self.WriteUInt(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int WriteString(IntPtr l) {
		try {
			NetLuaPacket self=(NetLuaPacket)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			self.WriteString(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int WriteBlock(IntPtr l) {
		try {
			NetLuaPacket self=(NetLuaPacket)checkSelf(l);
			System.Byte[] a1;
			checkArray(l,2,out a1);
			self.WriteBlock(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int WriteFloat(IntPtr l) {
		try {
			NetLuaPacket self=(NetLuaPacket)checkSelf(l);
			System.Single a1;
			checkType(l,2,out a1);
			self.WriteFloat(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"NetLuaPacket");
		addMember(l,Type);
		addMember(l,Size);
		addMember(l,SetOffset);
		addMember(l,Finish);
		addMember(l,ReadChar);
		addMember(l,ReadByte);
		addMember(l,ReadShort);
		addMember(l,ReadUShort);
		addMember(l,ReadInt);
		addMember(l,ReadUInt);
		addMember(l,ReadString);
		addMember(l,ReadBlock);
		addMember(l,ReadFloat);
		addMember(l,WriteChar);
		addMember(l,WriteByte);
		addMember(l,WriteShort);
		addMember(l,WriteUShort);
		addMember(l,WriteInt);
		addMember(l,WriteUInt);
		addMember(l,WriteString);
		addMember(l,WriteBlock);
		addMember(l,WriteFloat);
		addMember(l,NetLuaPacket.ReadBlock,true);
		addMember(l,NetLuaPacket.WriteBlock,true);
		createTypeMetatable(l,constructor, typeof(NetLuaPacket));
	}
}
