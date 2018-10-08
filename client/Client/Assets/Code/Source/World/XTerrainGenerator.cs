using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using SLua;

[CustomLuaClass]
public class XTerrainGenerator : MonoBehaviour {
	public Material 				TerrainMaterial;
	public Texture2D 				FlatTexture;
	public Texture2D 				SteepTexture;
	public int 						HeightmapResolution;
	public int 						AlphamapResolution;
	public int 						Length;
	public int 						Height;

	/// <summary>
	/// The settings.
	/// </summary>
	private XTerrainChunkSetting 	Settings;
	private XNoisePerlin 			NoiseProvider;

	/// <summary>
	/// The cache.
	/// </summary>
	private XTerrainChunkCache 		Cache;

	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake()
	{
		
	}

	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update()
	{
		Cache.Update();
	}

	/// <summary>
	/// Inits the generate.
	/// </summary>
	public void 			InitGenerate(){
		Settings 		= new XTerrainChunkSetting(HeightmapResolution, AlphamapResolution, Length, Height, FlatTexture, SteepTexture, TerrainMaterial);
		NoiseProvider 	= new XNoisePerlin();
		Cache 			= new XTerrainChunkCache();
	}

	/// <summary>
	/// Generates the chunk.
	/// </summary>
	/// <param name="x">The x coordinate.</param>
	/// <param name="z">The z coordinate.</param>
	private void 			GenerateChunk(int x, int z)
	{
		if (Cache.ChunkCanBeAdded(x, z))
		{
			var chunk = new XTerrainChunk(Settings, NoiseProvider, x, z);
			Cache.AddNewChunk(chunk);
		}
	}

	/// <summary>
	/// Removes the chunk.
	/// </summary>
	/// <param name="x">The x coordinate.</param>
	/// <param name="z">The z coordinate.</param>
	private void 			RemoveChunk(int x, int z)
	{
		if (Cache.ChunkCanBeRemoved(x, z))
			Cache.RemoveChunk(x, z);
	}

	/// <summary>
	/// Gets the chunk positions in radius.
	/// </summary>
	/// <returns>The chunk positions in radius.</returns>
	/// <param name="chunkPosition">Chunk position.</param>
	/// <param name="radius">Radius.</param>
	private List<XVec2I> 	GetChunkPositionsInRadius(XVec2I chunkPosition, int radius)
	{
		var result = new List<XVec2I>();

		for (var zCircle = -radius; zCircle <= radius; zCircle++)
		{
			for (var xCircle = -radius; xCircle <= radius; xCircle++)
			{
				if (xCircle * xCircle + zCircle * zCircle < radius * radius)
					result.Add(new XVec2I(chunkPosition.X + xCircle, chunkPosition.Z + zCircle));
			}
		}

		return result;
	}

	/// <summary>
	/// Updates the terrain.
	/// </summary>
	/// <param name="worldPosition">World position.</param>
	/// <param name="radius">Radius.</param>
	public void 			UpdateTerrain(Vector3 worldPosition, int radius)
	{
		var chunkPosition 	= GetChunkPosition(worldPosition);
		var newPositions 	= GetChunkPositionsInRadius(chunkPosition, radius);

		var loadedChunks 	= Cache.GetGeneratedChunks();
		var chunksRemove 	= loadedChunks.Except(newPositions).ToList();
		var possGenerate 	= newPositions.Except(chunksRemove).ToList();

		for (int i = 0; i < possGenerate.Count; i++) {
			XVec2I v = possGenerate [i];
			GenerateChunk(v.X, v.Z);
		}

		for (int i = 0; i < chunksRemove.Count; i++) {
			XVec2I v = chunksRemove [i];
			RemoveChunk(v.X, v.Z);
		}
	}

	/// <summary>
	/// Gets the chunk position.
	/// </summary>
	/// <returns>The chunk position.</returns>
	/// <param name="worldPosition">World position.</param>
	public XVec2I 			GetChunkPosition(Vector3 worldPosition)
	{
		int x = (int)Mathf.Floor(worldPosition.x / Settings.Length);
		int z = (int)Mathf.Floor(worldPosition.z / Settings.Length);

		return new XVec2I(x, z);
	}

	/// <summary>
	/// Determines whether this instance is terrain available the specified worldPosition.
	/// </summary>
	/// <returns><c>true</c> if this instance is terrain available the specified worldPosition; otherwise, <c>false</c>.</returns>
	/// <param name="worldPosition">World position.</param>
	public bool 			IsTerrainAvailable(Vector3 worldPosition)
	{
		XVec2I chunkPosition = GetChunkPosition(worldPosition);
		return Cache.IsChunkGenerated(chunkPosition);
	}

	/// <summary>
	/// Gets the height of the terrain.
	/// </summary>
	/// <returns>The terrain height.</returns>
	/// <param name="worldPosition">World position.</param>
	public float 			GetTerrainHeight(Vector3 worldPosition)
	{
		XVec2I chunkPosition 	= GetChunkPosition(worldPosition);
		XTerrainChunk chunk 	= Cache.GetGeneratedChunk(chunkPosition);
		if (chunkPosition != null)
			return chunk.GetTerrainHeight(worldPosition);

		return 0;
	}
}
