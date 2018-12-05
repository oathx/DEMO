using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SLua;


[CustomLuaClass]
public class XBox2DFlexibleHurt : MonoBehaviour
{
    public XBox2D                   hurtBox;

    /// <summary>
    /// 
    /// </summary>
    private XRectBox                hurtRectBox = new XRectBox();
    private Transform               hurtTrans;

    /// <summary>
    /// 
    /// </summary>
    public enum XFlexibleHurtActiveType
    {
        None = 0,
        Interactive,
    }

    private XFlexibleHurtActiveType activeType;
    private int                     activeID;

    /// <summary>
    /// 
    /// </summary>
    public Transform                Trans
    {
        get { return hurtTrans; }
    }

    /// <summary>
    /// 
    /// </summary>
    void Awake()
    {
        hurtTrans = transform;
        RegisterInBoxes();
    }

    /// <summary>
    /// 
    /// </summary>
    void OnDestroy()
    {
        UnRegisterFromBoxes();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public XRectBox         GetFlexibleHurtBox()
    {
        if (hurtTrans == null)
            return null;

        hurtRectBox.MinX = Mathf.RoundToInt(hurtTrans.position.x * XBox2DComponent.FloatCorrection) + hurtBox.OffsetX - hurtBox.Width;
        hurtRectBox.MinY = Mathf.RoundToInt(hurtTrans.position.y * XBox2DComponent.FloatCorrection) + hurtBox.OffsetY;
        hurtRectBox.Width = hurtBox.Width * 2;
        hurtRectBox.Height = hurtBox.Height;

        return hurtRectBox;
    }

    /// <summary>
    /// 
    /// </summary>
    public void             RegisterInBoxes()
    {
        XBox2DSystem.GetSingleton().RegisterFlexibleHurtBox(this);
    }

    /// <summary>
    /// 
    /// </summary>
    public void             UnRegisterFromBoxes()
    {
        XBox2DSystem.GetSingleton().UnRegisterFlexibleHurtBox(this);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    public void             SetActiveTypeInLua(int type)
    {
        activeType = (XFlexibleHurtActiveType)type;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="activeId"></param>
    public void             SetActiveIdInLua(int activeId)
    {
        activeID = activeId;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public int              GetActiveID()
    {
        return activeID;
    }


#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        if (hurtBox == null)
            return;

        if (hurtTrans == null)
            hurtTrans = transform;

        Gizmos.color = Color.green;
        GetFlexibleHurtBox();

        Gizmos.DrawWireCube(new Vector3((hurtRectBox.MinX + hurtBox.Width) / XBox2DComponent.FloatCorrection,
            (hurtRectBox.MinY + hurtBox.Height / 2f) / XBox2DComponent.FloatCorrection, 0f),
            new Vector3(hurtBox.Width * 2 / XBox2DComponent.FloatCorrection, hurtBox.Height / XBox2DComponent.FloatCorrection, 1.0f));
    }

#endif
}
