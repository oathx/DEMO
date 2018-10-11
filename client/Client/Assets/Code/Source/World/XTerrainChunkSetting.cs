using UnityEngine;
using System.Threading;
using SLua;
using System.Collections.Generic;

/// <summary>
/// terrain chunk setting.
/// </summary>
[System.Serializable]
[CustomLuaClass]
public class XTerrainChunkSetting : ScriptableObject {
	/// <summary>
	/// Gets the heightmap resolution.
	/// </summary>
	/// <value>The heightmap resolution.</value>
	public int 			    HeightmapResolution = 65;


	/// <summary>
	/// Gets the alphamap resolution.
	/// </summary>
	/// <value>The alphamap resolution.</value>
	public int 			    AlphamapResolution = 65;


	/// <summary>
	/// Gets the length.
	/// </summary>
	/// <value>The length.</value>
	public int 			    Length = 40;
	

	/// <summary>
	/// Gets the height.
	/// </summary>
	/// <value>The height.</value>
	public int 			    Height = 10;

    /// <summary>
    /// 
    /// </summary>
    public int              Raidus = 2;

    /// <summary>
    /// 
    /// </summary>
    public int              Left = 1;
    public int              Right = 5;
    public int              Top = 2;
    public int              Bottom = 6;

    /// <summary>
    /// Gets the terrain material.
    /// </summary>
    /// <value>The terrain material.</value>
    public Material         TerrainMaterial;

    /// <summary>
    /// 
    /// </summary>
    public List<Texture2D>  Textures = new List<Texture2D>();
  

    /// <summary>
    /// 
    /// </summary>
    public List<GameObject> Trees = new List<GameObject>();
 
}
