using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using SLua;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using ICSharpCode.SharpZipLib.Zip;

[CustomLuaClassAttribute]
public static class GUtility {
   	static System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();

    public static int HashString( string str ) {
        const uint InitialFNV = 2166136261U;
        const uint FNVMultiple = 16777619;
        
        uint hash = InitialFNV;
        for( int i = 0; i < str.Length; i++ )
        {
            hash = hash ^ str[i];
            hash = hash * FNVMultiple;
        }
        
        return (int)(hash&0x7FFFFFFF);
    }


    public static string Md5Sum(string input) {
        return Md5Sum(System.Text.Encoding.ASCII.GetBytes(input));
    }
    
    public static string Md5Sum(byte[] bytes) {
        byte[] hash = md5.ComputeHash(bytes);
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        for (int i = 0; i < hash.Length; i++)
        {
            sb.Append(hash[i].ToString("x2"));
        } 
        return sb.ToString(); 
    }
		
	public static DateTime TimeStampToDateTime(int timeStamp){
		DateTime dateTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
		dateTime = dateTime.AddSeconds(timeStamp);
		return dateTime;
	}
	
	public static int DateTimeToTimeStamp(DateTime dateTime){
		DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
		return (int)(dateTime - startTime).TotalSeconds;
	}
	
	public static int GetNaturalDaysCount(DateTime dtStart, DateTime dtEnd){
		TimeSpan tsStart = new TimeSpan(dtStart.Ticks);
		TimeSpan tsEnd = new TimeSpan(dtEnd.Ticks);
		return tsEnd.Days - tsStart.Days;
	}
	
	public static int GetHoursCount(DateTime dtStart, DateTime dtEnd) {
		TimeSpan tsStart = new TimeSpan(dtStart.Ticks);
		TimeSpan tsEnd = new TimeSpan(dtEnd.Ticks);
		TimeSpan n = tsEnd - tsStart;
		return n.Hours + n.Days * 24;
	}

	public static int serverTimeOffset = 0;
	public static int ServerTime {
		get {
			return DateTimeToTimeStamp(DateTime.Now)+serverTimeOffset;
		}
	}
	
	public static DateTime ServerDateTime {
		get {
			return TimeStampToDateTime(ServerTime);
		}
	}
	
	public static void SyncTimeFromServer( int serverTime ) {
		serverTimeOffset = serverTime - DateTimeToTimeStamp(DateTime.Now);
	}
	
	public static string NoCacheUrl( string url ) {
		return string.Format("{0}?r={1}", url, ServerTime);
	}

    public static void UnZipFile(byte[] bytes, string outPath)
    {
        using (var memStream = new MemoryStream(bytes))
        {
            UnZipFile(memStream, outPath);
        }
    }
    
    private static void UnZipFile(Stream stream, string outPath)
    {
        if (!Directory.Exists(outPath))
        {
            Directory.CreateDirectory(outPath);
        }
        
        using (var zipStream = new ZipInputStream(stream))
        {
            ZipEntry theEntry;
            while ((theEntry = zipStream.GetNextEntry()) != null)
            {
                string dirName = Path.GetDirectoryName(theEntry.Name);
                string fileName = Path.GetFileName(theEntry.Name);
                
                if (!string.IsNullOrEmpty(dirName))
                {
                    Directory.CreateDirectory(outPath + dirName);
                }
                
                if (!string.IsNullOrEmpty(fileName))
                {
                    using (var streamWriter = File.Create(outPath + theEntry.Name))
                    {
                        int size = 2048;
                        var data = new byte[size];
                        while (true)
                        {
                            size = zipStream.Read(data, 0, data.Length);
                            if (size > 0)
                            {
                                streamWriter.Write(data, 0, size);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}
