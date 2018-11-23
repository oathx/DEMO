using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum Location
{
    Resource, Data, Streaming, Bundle
}

public enum EntryType
{
    None,
    Pack,
    File,
    Scene
};

public enum CacheType
{
    None,
    InScene,
    AllTime
};

public enum PackageType
{
    None,
    Package1,
    Package2,
    Package3
};

public enum CompressionType
{
    None,
    Zip
};

public enum RisType {
    None, xris
}

public class XResEntry
{
    protected EntryType     type = EntryType.None;

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public EntryType        Type() {
        return type; 
    }

    public uint             checksum 
    { get; set; }
    
    public string           name 
    { get; set; }
    
    public Location         location 
    { get; set; }

    public CacheType        cacheType 
    { get; set; }

    public PackageType      packageType 
    { get; set; }

    public CompressionType  compressionType 
    { get; set; }

    public uint size        { get; set; }
}

/// <summary>
/// 
/// </summary>
public class XResFile : XResEntry
{
    public XResFile() { 
        type = EntryType.File; 
    }
}

/// <summary>
/// 
/// </summary>
public class XResPack : XResEntry
{
    public string[] files 
    { get; set; }
    
    public string[] dependencies 
    { get; set; }

    public XResPack() 
    { type = EntryType.Pack; }
    
    public bool     NoBundle() { 
        return location == Location.Resource; 
    }
}

public class XResScene : XResEntry
{
    public string bundle 
    { get; set; }

    public XResScene()
    { 
        type = EntryType.Scene; 
    }
}

