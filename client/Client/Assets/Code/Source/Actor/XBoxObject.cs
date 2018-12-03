using System.Collections;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
[System.Serializable]
public class XBoxObject : ScriptableObject
{
    public int OffsetX;
    public int OffsetY;

    public int Width;
    public int Height;
}

/// <summary>
/// 
/// </summary>
public enum XHugType
{
    Normal,
    Super,
    SuperIgnoreReaction,
}

/// <summary>
/// 
/// </summary>
[System.Serializable]
public class XHugBoxObject : XBoxObject
{
    public XHugType HugType;
}

/// <summary>
/// 
/// </summary>
public class XBodyBoxIndex
{
    public XBodyBoxComponent hc;
    public int index;
}

/// <summary>
/// 
/// </summary>
#region RectBox
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

    /// <summary>
    /// 
    /// </summary>
    private static XRectBox rectBoxOverlap = null;

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return string.Format("[XRectBox] {0}  {1}  {2}  {3}", 
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
public class ExtraRectBox : XRectBox
{
    public XBoxExtraProperty Property;
}

/// <summary>
/// 
/// </summary>
public class XHugRectBox : XRectBox
{
    public XHugType HugType;
}

#endregion          //RectBox

/// <summary>
/// 
/// </summary>
public class XBodyBoxOverlap
{
    public XBodyBoxComponent actor;
    public int Overlap;
}

/// <summary>
/// 
/// </summary>
public class XBodyMeetCache
{
    /// <summary>
    /// 
    /// </summary>
    private List<List<XBodyBoxComponent>> mCache = new List<List<XBodyBoxComponent>>();

    /// <summary>
    /// 
    /// </summary>
    public List<List<XBodyBoxComponent>> CacheList
    {
        get { return mCache; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="src"></param>
    /// <param name="hc"></param>
    public void AddCache(XBodyBoxComponent src, XBodyBoxComponent hc)
    {
        List<XBodyBoxComponent> cacheExis = GetCacheInCache(src);

        if (cacheExis != null)
        {
            if (!cacheExis.Contains(hc))
                cacheExis.Add(hc);
            return;
        }

        List<XBodyBoxComponent> cache = new List<XBodyBoxComponent>();
        cache.Add(src);
        cache.Add(hc);

        mCache.Add(cache);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="hc"></param>
    /// <returns></returns>
    public List<XBodyBoxComponent> GetCacheInCache(XBodyBoxComponent hc)
    {
        if (mCache.Count > 0)
        {
            for (int i = 0; i < mCache.Count; i++)
            {
                if (mCache[i] != null)
                {
                    for (int index = 0; index < mCache[i].Count; index++)
                    {
                        if (hc == mCache[i][index])
                            return mCache[i];
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
        for (int i = 0; i < mCache.Count; i++)
        {
            if (mCache[i] != null)
            {
                mCache[i].Clear();
            }
            mCache.Clear();
        }
    }

}
