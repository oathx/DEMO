using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SLua;

/// <summary>
/// 
/// </summary>
[System.Serializable]
public class XBox2D : XScriptAssetUpdateObject
{
    public int OffsetX;
    public int OffsetY;

    public int Width;
    public int Height;
}

/// <summary>
/// 
/// </summary>
public enum XBox2DHugType
{
    Normal,
    Super,
    SuperIgnoreReaction,
}

/// <summary>
/// 
/// </summary>
[System.Serializable]
public class XHugBox2D : XScriptAssetUpdateObject
{
    public XBox2DHugType HugType;
}

/// <summary>
/// 
/// </summary>
public class XBodyBox2D : XScriptAssetUpdateObject
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
    public List<XBodyReceiveDamageBox> ReceiveDamageBoxes = new List<XBodyReceiveDamageBox>();

    [System.Serializable]
    public class XBodyReceiveDamageBox
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

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public class XBodyBox2DConfigDataTemplate<T>
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
public class XAttackBox2D
{
    public Vector2 Offset = Vector2.zero;
    public float Width = 0.5f;
    public float Height = 0.5f;

    public bool IsFollowBone = false;
    public HumanBodyBones FollowBone = HumanBodyBones.LastBone;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="offsetX"></param>
    /// <param name="offsetY"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="fFloatCorrection"></param>
    /// <returns></returns>
    public static XAttackBox2D CreateAttackBoxData(int offsetX, int offsetY, int width, int height, float fFloatCorrection)
    {
        XAttackBox2D data = new XAttackBox2D();
        data.Offset = new Vector2(offsetX / fFloatCorrection, offsetY / fFloatCorrection);
        data.Width = width / fFloatCorrection;
        data.Height = height / fFloatCorrection;

        return data;
    }
}

/// <summary>
/// 
/// </summary>
public class XAttackWarningBox
{
    public int CreateState = 0;
    public XBodyBox2D Box = null;

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
public class XBox2DOverlap
{
    public XBox2DComponent Hero;
    public int Overlap;
}

/// <summary>
/// 
/// </summary>
public class XBodyBox2DMeetCache
{
    /// <summary>
    /// 
    /// </summary>
    private List<List<XBox2DComponent>> cacheList = new List<List<XBox2DComponent>>();

    /// <summary>
    /// 
    /// </summary>
    public List<List<XBox2DComponent>> CacheList
    {
        get { return cacheList; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="src"></param>
    /// <param name="hc"></param>
    public void AddCache(XBox2DComponent src, XBox2DComponent hc)
    {
        List<XBox2DComponent> cacheExis = GetCacheInCache(src);

        if (cacheExis != null)
        {
            if (!cacheExis.Contains(hc))
                cacheExis.Add(hc);
            return;
        }

        List<XBox2DComponent> cache = new List<XBox2DComponent>();
        cache.Add(src);
        cache.Add(hc);

        cacheList.Add(cache);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="hc"></param>
    /// <returns></returns>
    public List<XBox2DComponent> GetCacheInCache(XBox2DComponent hc)
    {
        if (cacheList.Count > 0)
        {
            for (int i = 0; i < cacheList.Count; i++)
            {
                if (cacheList[i] != null)
                {
                    for (int index = 0; index < cacheList[i].Count; index++)
                    {
                        if (hc == cacheList[i][index])
                            return cacheList[i];
                    }
                }
            }
        }

        return null;
    }

    /// <summary>
    /// 
    /// </summary>
    public void Clear()
    {
        for (int i = 0; i < cacheList.Count; i++)
        {
            if (cacheList[i] != null)
            {
                cacheList[i].Clear();
            }

            cacheList.Clear();
        }
    }
}

/// <summary>
/// 
/// </summary>
public class XBox2DIndex
{
    public XBox2DComponent hc;
    public int index;
}

/// <summary>
/// 
/// </summary>
public class XRectBox
{
    public int MinX;
    public int MinY;
    public int Width;
    public int Height;

    /// <summary>
    /// 
    /// </summary>
    public XRectBox()
    {
        MinX = 0;
        MinY = 0;
        Width = -1;
        Height = -1;
    }

    private static XRectBox rectBoxOverlap = null;

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return string.Format("[RectBox] {0}  {1}  {2}  {3}",
            (MinX / 100f).ToString(), (MinY / 100f).ToString(), (Width / 100f).ToString(), (Height / 100f).ToString());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="box"></param>
    /// <returns></returns>
    public int Overlap(XRectBox box)
    {
        // pretend to create a box, if the box exit, overlap!
        int minX = Mathf.Max(this.MinX, box.MinX);
        int minY = Mathf.Max(this.MinY, box.MinY);
        int maxX = Mathf.Min(this.MinX + Width, box.MinX + box.Width);
        int maxY = Mathf.Min(this.MinY + Height, box.MinY + box.Height);

        if (minX > maxX || minY > maxY)
        {
            return -1;
        }
        else
        {
            if (minX >= maxX)
                return 0;

            return maxX - minX;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public bool IsEmpty()
    {
        return Width == 0 || Height == 0;
    }

    /// <summary>
    /// 
    /// </summary>
    public void Empty()
    {
        Width = 0;
        Height = 0;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="box"></param>
    public void Copy(XRectBox box)
    {
        this.MinX = box.MinX;
        this.MinY = box.MinY;
        this.Width = box.Width;
        this.Height = box.Height;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="box1"></param>
    /// <param name="box2"></param>
    /// <returns></returns>
    public static XRectBox Overlap(XRectBox box1, XRectBox box2)
    {
        if (rectBoxOverlap == null)
            rectBoxOverlap = new XRectBox();
        if (box1 != null && box2 != null)
        {
            // pretend to create a box, if the box exit, overlap!
            int minX = Mathf.Max(box1.MinX, box2.MinX);
            int minY = Mathf.Max(box1.MinY, box2.MinY);
            int maxX = Mathf.Min(box1.MinX + box1.Width, box2.MinX + box2.Width);
            int maxY = Mathf.Min(box1.MinY + box1.Height, box2.MinY + box2.Height);

            if (minX > maxX || minY > maxY)
            {
                rectBoxOverlap.Height = -1;
                rectBoxOverlap.Width = -1;
            }
            else
            {
                rectBoxOverlap.Height = maxY - minY;
                rectBoxOverlap.Width = maxX - minX;
                rectBoxOverlap.MinX = minX;
                rectBoxOverlap.MinY = minY;
            }
        }
        else
        {
            rectBoxOverlap.Width = -1;
        }

        return rectBoxOverlap;
    }
}

/// <summary>
/// 
/// </summary>
public class XBodyMoveRectBox : XRectBox
{
    public int MoveX;
    public int MoveY;
}

/// <summary>
/// 
/// </summary>
public enum XBoxExtraProperty
{
    Wall,
}

/// <summary>
/// 
/// </summary>
public class XExtraRectBox : XRectBox
{
    public XBoxExtraProperty Property;
}

/// <summary>
/// 
/// </summary>
public class XHugRectBox : XRectBox
{
    public XBox2DHugType HugType;
}