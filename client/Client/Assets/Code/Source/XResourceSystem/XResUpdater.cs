using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using ICSharpCode;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

/// <summary>
/// X res updater.
/// </summary>
public class XResUpdater
{
	/// <summary>
	/// Update stage.
	/// </summary>
    public enum UpdateStage
    {
        None, LocalVersion, FetchVersion, FetchFat, CheckChange, DownloadRes, DownloadApk, ExternalRes, FetchNews, CheckDataIntegrity, Finish, DiskFull, LagNet, Unknow
    }
}
