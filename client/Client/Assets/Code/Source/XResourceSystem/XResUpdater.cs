using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using ICSharpCode;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

public class XResUpdater
{
    public enum UpdateStage
    {
        None, LocalVersion, FetchVersion, FetchFat, CheckChange, DownloadRes, DownloadApk, ExternalRes, FetchNews, CheckDataIntegrity, Finish,
        DiskFull, LagNet, Unknow
    }
}
