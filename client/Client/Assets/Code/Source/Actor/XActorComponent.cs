using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SLua;

[CustomLuaClass]
public class XActorComponent : MonoBehaviour
{
    [CustomLuaClass]
    public enum BoneNogType
    {
        None, 
        RHand, 
        LHand, 
        Hit, 
        RFoot, 
        LFoot, 
        Center, 
        Head, 
        Pelvis,
        RElbow, 
        LElbow, 
        RKnee, LKnee, 
        RWeapon, 
        LWeapon, 
        REye, 
        LEye,
        Reserve1, 
        Reserve2, 
        Reserve3, 
        LshoulderArmor, 
        RshoulderArmor,
        LUpperArm, 
        RUpperArm, 
        LForearm, 
        RForearm, 
        Spine, 
        Spine1, 
        Spine2,
        LThigh, 
        RThigh, 
        LCalf, 
        RCalf
    };

}

