using UnityEngine;
using System.Threading;
using SLua;
using System.Collections.Generic;

/// <summary>
/// terrain chunk setting.
/// </summary>
[CustomLuaClass]
public class XTerrainChunkSetting : ScriptableObject {
	/// <summary>
	/// Gets the heightmap resolution.
	/// </summary>
	/// <value>The heightmap resolution.</value>
	public int 			HeightmapResolution 
	{ get; private set; }

	/// <summary>
	/// Gets the alphamap resolution.
	/// </summary>
	/// <value>The alphamap resolution.</value>
	public int 			AlphamapResolution 
	{ get; private set; }

	/// <summary>
	/// Gets the length.
	/// </summary>
	/// <value>The length.</value>
	public int 			Length 
	{ get; private set; }

	/// <summary>
	/// Gets the height.
	/// </summary>
	/// <value>The height.</value>
	public int 			Height 
	{ get; private set; }

	/// <summary>
	/// Gets the terrain material.
	/// </summary>
	/// <value>The terrain material.</value>
	public Material 	TerrainMaterial 
	{ get; private set; }

    /// <summary>
    /// 
    /// </summary>
    public List<Texture2D> Textures
    { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    public List<GameObject> Trees
    { get; private set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="XTerrainChunkSetting"/> class.
	/// </summary>
	/// <param name="heightmapResolution">Heightmap resolution.</param>
	/// <param name="alphamapResolution">Alphamap resolution.</param>
	/// <param name="length">Length.</param>
	/// <param name="height">Height.</param>
	/// <param name="flatTexture">Flat texture.</param>
	/// <param name="steepTexture">Steep texture.</param>
	/// <param name="terrainMaterial">Terrain material.</param>
	public XTerrainChunkSetting(int heightmapResolution, int alphamapResolution,
        int length, int height, Material terrainMaterial, List<Texture2D> textures, List<GameObject> trees)
	{
		HeightmapResolution = heightmapResolution;
		AlphamapResolution 	= alphamapResolution;
		Length 				= length;
		Height 				= height;
		TerrainMaterial 	= terrainMaterial;
        Textures            = textures;
        Trees               = trees;
	}
}
