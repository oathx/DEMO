using UnityEngine;
using System.Threading;
using SLua;

/// <summary>
/// terrain chunk setting.
/// </summary>
[CustomLuaClass]
public class XTerrainChunkSetting{
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
	/// Gets the flat texture.
	/// </summary>
	/// <value>The flat texture.</value>
	public Texture2D 	FlatTexture 
	{ get; private set; }

	/// <summary>
	/// Gets the steep texture.
	/// </summary>
	/// <value>The steep texture.</value>
	public Texture2D 	SteepTexture 
	{ get; private set; }

	/// <summary>
	/// Gets the terrain material.
	/// </summary>
	/// <value>The terrain material.</value>
	public Material 	TerrainMaterial 
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
		int length, int height, Texture2D flatTexture, Texture2D steepTexture, Material terrainMaterial)
	{
		HeightmapResolution = heightmapResolution;
		AlphamapResolution 	= alphamapResolution;
		Length 				= length;
		Height 				= height;
		FlatTexture 		= flatTexture;
		SteepTexture 		= steepTexture;
		TerrainMaterial 	= terrainMaterial;
	}
}
