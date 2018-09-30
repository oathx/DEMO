using UnityEngine;
using SLua;
using System;
using System.Collections;
using System.Collections.Generic;

[CustomLuaClass]
public class ShapeAssetObject : ScriptableObject
{
	public Vector3		LocalPosition = Vector3.zero;
	public Vector3		LocalAngle = Vector3.zero;
	public Vector3 		LocalScale = Vector3.one;
}


