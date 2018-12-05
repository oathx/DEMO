using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SLua;

[CustomLuaClass]
public class XBox2DSystem
{
    /// <summary>
    /// 
    /// </summary>
    private static readonly XBox2DSystem instance = new XBox2DSystem();

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static XBox2DSystem GetSingleton(){
        return instance;
    }

    public void RegisterFlexibleHurtBox(XBox2DFlexibleHurt box)
    {

    }

    public void UnRegisterFlexibleHurtBox(XBox2DFlexibleHurt box)
    {

    }
}