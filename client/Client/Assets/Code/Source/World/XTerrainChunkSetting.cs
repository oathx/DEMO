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

    public int              OctaveCount = 6;

    public float            Frequency = 1.0f;
    public float            Lacunarity = 2.0f;
    public float            Persistence = 0.5f;

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
    public List<GameObject> TreePrefabs = new List<GameObject>();

    public int              StumpHeigt = 30;
    public int              MaxStempCount = 10;
    public int              StumpDensity = 20;
    public int              TreeGenerateDensity = 5;
 
}
