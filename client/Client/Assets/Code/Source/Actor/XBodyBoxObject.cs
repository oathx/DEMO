using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public class XBodyBoxDataTemple<T>
{
    // body box
    [SerializeField]
    public Vector2 BodyBoxOffset;
    [SerializeField]
    public T BodyBoxWidth;
    [SerializeField]
    public T BodyBoxHeight;

    // body receive damage box
    [SerializeField]
    public List<XBodyReceiveDamageBox> ReceiveDamageBoxes = new List<XBodyReceiveDamageBox>();

    [System.Serializable]
    public class XBodyReceiveDamageBox
    {
        [SerializeField]
        public Vector2 BoxOffset;

        [SerializeField]
        public T BoxWidth;
        [SerializeField]
        public T BoxHeight;
    }
}

/// <summary>
/// 
/// </summary>
public class XAttackBoxData
{
    public Vector2 Offset = Vector2.zero;
    public float Width = 0.5f;
    public float Height = 0.5f;

    public bool IsFollowBone = false;
    public HumanBodyBones FollowBone = HumanBodyBones.LastBone;

    public static XAttackBoxData CreateAttackBoxData(int offsetX, int offsetY, int width, int height, float fFloatCorrection)
    {
        XAttackBoxData data = new XAttackBoxData();
        data.Offset = new Vector2(offsetX / fFloatCorrection, offsetY / fFloatCorrection);
        data.Width = width / fFloatCorrection;
        data.Height = height / fFloatCorrection;

        return data;
    }
}

/// <summary>
/// 
/// </summary>
public class XAttackWarningData
{
    public int CreateState = 0;
    public XBoxObject Box = null;

    public void Clear()
    {
        CreateState = 0;
        Box = null;
    }

    public bool Valid()
    {
        return Box != null;
    }
}

/// <summary>
/// 
/// </summary>
public class XBodyBoxObject : ScriptableObject
{
    // body box
    [SerializeField]
    public int BodyBoxOffsetX;
    [SerializeField]
    public int BodyBoxOffsetY;

    [SerializeField]
    public int BodyBoxWidth;
    [SerializeField]
    public int BodyBoxHeight;

    // body receive damage box
    [SerializeField]
    public List<BodyReceiveDamageBox> ReceiveDamageBoxes = new List<BodyReceiveDamageBox>();

    [System.Serializable]
    public class BodyReceiveDamageBox
    {
        [SerializeField]
        public int BoxOffsetX;
        [SerializeField]
        public int BoxOffsetY;

        [SerializeField]
        public int BoxWidth;
        [SerializeField]
        public int BoxHeight;

        public bool IsEmpty()
        {
            return BoxWidth == 0 || BoxHeight == 0;
        }
    }
}

