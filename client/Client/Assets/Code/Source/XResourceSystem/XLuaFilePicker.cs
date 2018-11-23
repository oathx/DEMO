using UnityEngine;
using System.Collections;

public class XLuaFilePicker
{
    static LuaBigFileBufferAsset    asset;

    /// <summary>
    /// 
    /// </summary>
    public static string    AssetPath = "files/LuaBigFileBufferAsset";

    /// <summary>
    /// 
    /// </summary>
    public static void      InitPicker()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        _asset = Resources.Load<LuaBigFileBufferAsset>(LuaFilePicker.AssetPath);
        if (_asset == null)
        {
            Debug.LogError("Highest alert!!!  LuaBigFileBufferAsset    NULL");
            return;
        }
        Debug.Log("InitPicker SUCCESS!!!!!");
#endif
    }

    /// <summary>
    /// 
    /// </summary>
    public static void      ReleasePicker()
    {
        asset = null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static byte[]    GetLuaBytesByFileName(string fileName)
    {
        if (asset == null)
            return null;

        return InternalGetLuaBytesByFileName(fileName);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    static byte[]           InternalGetLuaBytesByFileName(string fileName)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        LuaBigFileBufferAsset.FileSign sign = _asset.FileSigns.Find(delegate(LuaBigFileBufferAsset.FileSign obj)
            {
                return obj.FileName == fileName;
            });
        if (sign != null)
        {
            byte[] bytes = new byte[sign.Length];
            System.Array.Copy(_asset.BigBytes, sign.Offset, bytes, 0, sign.Length);
            return bytes;
        }
        else
            Debug.LogError("Highest alert!!!  CANNOT FIND LUA FILE :  " + fileName);
#endif
        return null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static BufferAsset LoadLuaAsset(string fileName)
    {
        BufferAsset bufferAsset = null;

#if UNITY_ANDROID && !UNITY_EDITOR
        string name = fileName.ToLower();
        XFat.AssetInfo info = XFat.Instance.Find(name);
        if (info != null && info.location == XFat.Location.Resource)
        {
            if (_asset != null)
            {
                byte[] bytes = GetLuaBytesByFileName(fileName);
                if (bytes != null)
                {
                    bufferAsset = ScriptableObject.CreateInstance<BufferAsset>();
                    bufferAsset.init(bytes);
                }
            }
            else
            {
                Debug.LogError("Highest alert!!!  LuaBigFileBufferAsset    NULL");
                bufferAsset = XRes.Load<BufferAsset>(fileName);
            }
        }
        else
        {
            bufferAsset = XRes.Load<BufferAsset>(fileName);
        }
#else
        //bufferAsset = XRes.Load<BufferAsset>(fileName);
#endif

        return bufferAsset;
    }
}
