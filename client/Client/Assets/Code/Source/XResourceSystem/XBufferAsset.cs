using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// </summary>
public class BufferAsset : ScriptableObject
{
    private byte[] buffer;

    public byte[] bytes
    {
        get { return buffer; }
    }

    public void init(int length)
    {
        buffer = new byte[length];
    }

    public void init(TextAsset text)
    {
        if (text != null)
            buffer = text.bytes;
    }

    public void init(byte[] bytes)
    {
        buffer = bytes;
    }
}
