using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using SLua;

[CustomLuaClass]
public class XTerrainChunkCache
{
	private readonly int MaxChunkThreads = 3;

	/// <summary>
	/// Gets or sets the requested chunks.
	/// </summary>
	/// <value>The requested chunks.</value>
	private Dictionary<XVec2I, XTerrainChunk> 
		RequestedChunks { get; set; }

	/// <summary>
	/// Gets or sets the chunks being generated.
	/// </summary>
	/// <value>The chunks being generated.</value>
	private Dictionary<XVec2I, XTerrainChunk> 
		ChunksBeingGenerated { get; set; }

	/// <summary>
	/// Gets or sets the loaded chunks.
	/// </summary>
	/// <value>The loaded chunks.</value>
	private Dictionary<XVec2I, XTerrainChunk> 
		LoadedChunks { get; set; }

	/// <summary>
	/// Gets or sets the chunks to remove.
	/// </summary>
	/// <value>The chunks to remove.</value>
	private HashSet<XVec2I> ChunksToRemove 
		{ get; set; }

	/// <summary>
	/// Gets or sets the on chunk generated.
	/// </summary>
	/// <value>The on chunk generated.</value>
	public OnChunkGeneratedDelegate OnChunkGenerated 
		{ get; set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="ChunkCache"/> class.
	/// </summary>
	public XTerrainChunkCache()
	{
		RequestedChunks 		= new Dictionary<XVec2I, XTerrainChunk>();
		ChunksBeingGenerated 	= new Dictionary<XVec2I, XTerrainChunk>();
		LoadedChunks 			= new Dictionary<XVec2I, XTerrainChunk>();
		ChunksToRemove 			= new HashSet<XVec2I>();
	}

	/// <summary>
	/// Update this instance.
	/// </summary>
	public void 			Update()
	{
		TryToDeleteQueuedChunks();

		GenerateHeightmapForAvailableChunks();
		CreateTerrainForReadyChunks();
	}

	/// <summary>
	/// Adds the new chunk.
	/// </summary>
	/// <param name="chunk">Chunk.</param>
	public void 			AddNewChunk(XTerrainChunk chunk)
	{
		RequestedChunks.Add(chunk.Position, chunk);
		GenerateHeightmapForAvailableChunks();
	}

	/// <summary>
	/// Removes the chunk.
	/// </summary>
	/// <param name="x">The x coordinate.</param>
	/// <param name="z">The z coordinate.</param>
	public void 			RemoveChunk(int x, int z)
	{
		ChunksToRemove.Add(new XVec2I(x, z));
		TryToDeleteQueuedChunks();
	}

	/// <summary>
	/// Chunks the can be added.
	/// </summary>
	/// <returns><c>true</c>, if can be added was chunked, <c>false</c> otherwise.</returns>
	/// <param name="x">The x coordinate.</param>
	/// <param name="z">The z coordinate.</param>
	public bool 			ChunkCanBeAdded(int x, int z)
	{
		var key = new XVec2I(x, z);
		return
			!(RequestedChunks.ContainsKey(key)
				|| ChunksBeingGenerated.ContainsKey(key)
				|| LoadedChunks.ContainsKey(key));
	}

	/// <summary>
	/// Chunks the can be removed.
	/// </summary>
	/// <returns><c>true</c>, if can be removed was chunked, <c>false</c> otherwise.</returns>
	/// <param name="x">The x coordinate.</param>
	/// <param name="z">The z coordinate.</param>
	public bool 			ChunkCanBeRemoved(int x, int z)
	{
		var key = new XVec2I(x, z);
		return
			RequestedChunks.ContainsKey(key)
			|| ChunksBeingGenerated.ContainsKey(key)
			|| LoadedChunks.ContainsKey(key);
	}

	/// <summary>
	/// Determines whether this instance is chunk generated the specified chunkPosition.
	/// </summary>
	/// <returns><c>true</c> if this instance is chunk generated the specified chunkPosition; otherwise, <c>false</c>.</returns>
	/// <param name="chunkPosition">Chunk position.</param>
	public bool 			IsChunkGenerated(XVec2I chunkPosition)
	{
		return GetGeneratedChunk(chunkPosition) != null;
	}

	/// <summary>
	/// Gets the generated chunk.
	/// </summary>
	/// <returns>The generated chunk.</returns>
	/// <param name="chunkPosition">Chunk position.</param>
	public XTerrainChunk 	GetGeneratedChunk(XVec2I chunkPosition)
	{
		if (LoadedChunks.ContainsKey(chunkPosition))
			return LoadedChunks[chunkPosition];

		return null;
	}

	/// <summary>
	/// Gets the generated chunks.
	/// </summary>
	/// <returns>The generated chunks.</returns>
	public List<XVec2I> 	GetGeneratedChunks()
	{
		return LoadedChunks.Keys.ToList();
	}

	/// <summary>
	/// Generates the heightmap for available chunks.
	/// </summary>
	private void 			GenerateHeightmapForAvailableChunks()
	{
		var requestedChunks = RequestedChunks.ToList();

		if (requestedChunks.Count > 0 && ChunksBeingGenerated.Count < MaxChunkThreads)
		{
			var chunksToAdd = requestedChunks.Take(MaxChunkThreads - ChunksBeingGenerated.Count);
			foreach (var chunkEntry in chunksToAdd)
			{
				ChunksBeingGenerated.Add(chunkEntry.Key, chunkEntry.Value);
				RequestedChunks.Remove(chunkEntry.Key);

				chunkEntry.Value.GenerateHeightmap();
			}
		}
	}

	/// <summary>
	/// Creates the terrain for ready chunks.
	/// </summary>
	private void 			CreateTerrainForReadyChunks()
	{
		var anyTerrainCreated = false;

		var chunks = ChunksBeingGenerated.ToList();
		foreach (var chunk in chunks)
		{
			if (chunk.Value.IsHeightmapReady())
			{
				ChunksBeingGenerated.Remove(chunk.Key);
				LoadedChunks.Add(chunk.Key, chunk.Value);

				chunk.Value.CreateTerrain();

				anyTerrainCreated = true;
				if (OnChunkGenerated != null)
					OnChunkGenerated.Invoke(ChunksBeingGenerated.Count);

				SetChunkNeighborhood(chunk.Value);
			}
		}

		if (anyTerrainCreated)
			UpdateAllChunkNeighbors();
	}

	/// <summary>
	/// Tries to delete queued chunks.
	/// </summary>
	private void 		TryToDeleteQueuedChunks()
	{
		var chunksToRemove = ChunksToRemove.ToList();
		foreach (var chunkPosition in chunksToRemove)
		{
			if (RequestedChunks.ContainsKey(chunkPosition))
			{
				RequestedChunks.Remove(chunkPosition);
				ChunksToRemove.Remove(chunkPosition);
			}
			else if (LoadedChunks.ContainsKey(chunkPosition))
			{
				var chunk = LoadedChunks[chunkPosition];
				chunk.Remove();

				LoadedChunks.Remove(chunkPosition);
				ChunksToRemove.Remove(chunkPosition);
			}
			else if (!ChunksBeingGenerated.ContainsKey(chunkPosition))
				ChunksToRemove.Remove(chunkPosition);
		}
	}

	/// <summary>
	/// Sets the chunk neighborhood.
	/// </summary>
	/// <param name="chunk">Chunk.</param>
	private void 		SetChunkNeighborhood(XTerrainChunk chunk)
	{
		XTerrainChunk xUp;
		XTerrainChunk xDown;
		XTerrainChunk zUp;
		XTerrainChunk zDown;

		LoadedChunks.TryGetValue(new XVec2I(chunk.Position.X + 1, chunk.Position.Z), out xUp);
		LoadedChunks.TryGetValue(new XVec2I(chunk.Position.X - 1, chunk.Position.Z), out xDown);
		LoadedChunks.TryGetValue(new XVec2I(chunk.Position.X, chunk.Position.Z + 1), out zUp);
		LoadedChunks.TryGetValue(new XVec2I(chunk.Position.X, chunk.Position.Z - 1), out zDown);

		if (xUp != null)
		{
			chunk.SetNeighbors(xUp, TerrainNeighbor.XUp);
			xUp.SetNeighbors(chunk, TerrainNeighbor.XDown);
		}
		if (xDown != null)
		{
			chunk.SetNeighbors(xDown, TerrainNeighbor.XDown);
			xDown.SetNeighbors(chunk, TerrainNeighbor.XUp);
		}
		if (zUp != null)
		{
			chunk.SetNeighbors(zUp, TerrainNeighbor.ZUp);
			zUp.SetNeighbors(chunk, TerrainNeighbor.ZDown);
		}
		if (zDown != null)
		{
			chunk.SetNeighbors(zDown, TerrainNeighbor.ZDown);
			zDown.SetNeighbors(chunk, TerrainNeighbor.ZUp);
		}
	}

	/// <summary>
	/// Updates all chunk neighbors.
	/// </summary>
	private void 		UpdateAllChunkNeighbors()
	{
		foreach (var chunkEntry in LoadedChunks)
			chunkEntry.Value.UpdateNeighbors();
	}
}