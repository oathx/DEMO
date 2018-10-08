using UnityEngine;
using System.Threading;
using SLua;

public enum TerrainNeighbor
{
	XUp,
	XDown,
	ZUp,
	ZDown
}

/// <summary>
/// X chunk neighborhood.
/// </summary>
[CustomLuaClass]
public class XChunkNeighborhood
{
	public XTerrainChunk XUp 
	{ get; set; }

	public XTerrainChunk XDown 
	{ get; set; }

	public XTerrainChunk ZUp 
	{ get; set; }

	public XTerrainChunk ZDown 
	{ get; set; }
}

/// <summary>
/// On chunk generated delegate.
/// </summary>
public delegate void OnChunkGeneratedDelegate(int chunksLeftToGenerate);

/// <summary>
/// X chunk.
/// </summary>
[CustomLuaClass]
public class XTerrainChunk {
	/// <summary>
	/// Gets the position.
	/// </summary>
	/// <value>The position.</value>
	public XVec2I 					Position 
	{ get; private set; }

	/// <summary>
	/// Gets or sets the terrain.
	/// </summary>
	/// <value>The terrain.</value>
	private Terrain 				Terrain 
	{ get; set; }

	/// <summary>
	/// Gets or sets the data.
	/// </summary>
	/// <value>The data.</value>
	private TerrainData 			Data 
	{ get; set; }

	/// <summary>
	/// Gets or sets the setting.
	/// </summary>
	/// <value>The setting.</value>
	private XTerrainChunkSetting 	Setting
	{ get; set; }

	/// <summary>
	/// Gets or sets the noise provider.
	/// </summary>
	/// <value>The noise provider.</value>
	private XNoise 					NoiseProvider 
	{ get; set; }

	/// <summary>
	/// Gets or sets the neighborhood.
	/// </summary>
	/// <value>The neighborhood.</value>
	private XChunkNeighborhood 		Neighborhood 
	{get; set; }

	/// <summary>
	/// Gets or sets the heightmap.
	/// </summary>
	/// <value>The heightmap.</value>
	private float[,] 				Heightmap 
	{ get; set; }

	/// <summary>
	/// Gets or sets the heightmap thread.
	/// </summary>
	/// <value>The heightmap thread.</value>
	private object					HeightmapThread
	{ get; set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="XTerrainChunk"/> class.
	/// </summary>
	/// <param name="setting">Setting.</param>
	/// <param name="noise">Noise.</param>
	/// <param name="x">The x coordinate.</param>
	/// <param name="z">The z coordinate.</param>
	public XTerrainChunk(XTerrainChunkSetting setting, XNoise noise, int x, int z)
	{
		HeightmapThread 	= new object();
		Setting 			= setting;
		NoiseProvider 		= noise;
		Neighborhood 		= new XChunkNeighborhood();
		Position 			= new XVec2I(x, z);
	}

	/// <summary>
	/// Generates the heightmap.
	/// </summary>
	public void 	GenerateHeightmap()
	{
		var thread = new Thread(GenerateHeightmapThread);
		thread.Start();
	}

	/// <summary>
	/// Generates the heightmap thread.
	/// </summary>
	private void 	GenerateHeightmapThread()
	{
		lock (HeightmapThread)
		{
			var heightmap = new float[Setting.HeightmapResolution, Setting.HeightmapResolution];

			for (var zRes = 0; zRes < Setting.HeightmapResolution; zRes++)
			{
				for (var xRes = 0; xRes < Setting.HeightmapResolution; xRes++)
				{
					var xCoordinate = Position.X + (float)xRes / (Setting.HeightmapResolution - 1);
					var zCoordinate = Position.Z + (float)zRes / (Setting.HeightmapResolution - 1);

					heightmap[zRes, xRes] = NoiseProvider.GetValue(xCoordinate, zCoordinate);
				}
			}

			Heightmap = heightmap;
		}
	}

	/// <summary>
	/// Determines whether this instance is heightmap ready.
	/// </summary>
	/// <returns><c>true</c> if this instance is heightmap ready; otherwise, <c>false</c>.</returns>
	public bool 	IsHeightmapReady()
	{
		return (Terrain == null && Heightmap != null);
	}

	/// <summary>
	/// Gets the height of the terrain.
	/// </summary>
	/// <returns>The terrain height.</returns>
	/// <param name="worldPosition">World position.</param>
	public float 	GetTerrainHeight(Vector3 worldPosition)
	{
		return Terrain.SampleHeight(worldPosition);
	}

	/// <summary>
	/// Creates the terrain.
	/// </summary>
	public void 	CreateTerrain()
	{
		Data = new TerrainData();

		Data.heightmapResolution 	= Setting.HeightmapResolution;
		Data.alphamapResolution 	= Setting.AlphamapResolution;
		Data.SetHeights(0, 0, Heightmap);

		ApplyTextures(Data);

		Data.size = new Vector3(Setting.Length, Setting.Height, Setting.Length);
		GameObject newTerrainGameObject = Terrain.CreateTerrainGameObject(Data);
		newTerrainGameObject.transform.position = new Vector3(Position.X * Setting.Length, 0, Position.Z * Setting.Length);

		// create a new terrain chunk
		Terrain = newTerrainGameObject.GetComponent<Terrain>();
		Terrain.heightmapPixelError 	= 8;
		Terrain.materialType 			= UnityEngine.Terrain.MaterialType.Custom;
		Terrain.materialTemplate 		= Setting.TerrainMaterial;
		Terrain.reflectionProbeUsage 	= UnityEngine.Rendering.ReflectionProbeUsage.Off;
		Terrain.Flush();
	}

	/// <summary>
	/// Applies the textures.
	/// </summary>
	/// <param name="terrainData">Terrain data.</param>
	private void 	ApplyTextures(TerrainData terrainData)
	{
		SplatPrototype flatSplat 	= new SplatPrototype();
		SplatPrototype stepSplat 	= new SplatPrototype();

		flatSplat.texture 	= Setting.FlatTexture;
		stepSplat.texture 	= Setting.SteepTexture;

		terrainData.splatPrototypes = new SplatPrototype[] {
			flatSplat,
			stepSplat
		};

		terrainData.RefreshPrototypes();

		// proc alphamap
		var splatMap = new float[terrainData.alphamapResolution, terrainData.alphamapResolution, 2];
		for (var zRes = 0; zRes < terrainData.alphamapHeight; zRes++)
		{
			for (var xRes = 0; xRes < terrainData.alphamapWidth; xRes++)
			{
				var normalizedX = (float)xRes / (terrainData.alphamapWidth - 1);
				var normalizedZ = (float)zRes / (terrainData.alphamapHeight - 1);

				var steepness 			= terrainData.GetSteepness(normalizedX, normalizedZ);
				var steepnessNormalized = Mathf.Clamp(steepness / 1.5f, 0, 1f);

				splatMap[zRes, xRes, 0] = 1f - steepnessNormalized;
				splatMap[zRes, xRes, 1] = steepnessNormalized;
			}
		}

		terrainData.SetAlphamaps(0, 0, splatMap);
	}

	/// <summary>
	/// Serves as a hash function for a particular type.
	/// </summary>
	/// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
	public override int 	GetHashCode()
	{
		return Position.GetHashCode();
	}

	/// <summary>
	/// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="XTerrainChunk"/>.
	/// </summary>
	/// <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="XTerrainChunk"/>.</param>
	/// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to the current <see cref="XTerrainChunk"/>;
	/// otherwise, <c>false</c>.</returns>
	public override bool 	Equals(object obj)
	{
		var other = obj as XTerrainChunk;
		if (other == null)
			return false;

		return this.Position.Equals(other.Position);
	}

	/// <summary>
	/// Remove this instance.
	/// </summary>
	public void 			Remove()
	{
		Heightmap 	= null;
		Setting 	= null;

		if (Neighborhood.XDown != null)
		{
			Neighborhood.XDown.RemoveFromNeighborhood(this);
			Neighborhood.XDown = null;
		}

		if (Neighborhood.XUp != null)
		{
			Neighborhood.XUp.RemoveFromNeighborhood(this);
			Neighborhood.XUp = null;
		}

		if (Neighborhood.ZDown != null)
		{
			Neighborhood.ZDown.RemoveFromNeighborhood(this);
			Neighborhood.ZDown = null;
		}

		if (Neighborhood.ZUp != null)
		{
			Neighborhood.ZUp.RemoveFromNeighborhood(this);
			Neighborhood.ZUp = null;
		}

		if (Terrain != null)
			GameObject.Destroy(Terrain.gameObject);
	}

	/// <summary>
	/// Removes from neighborhood.
	/// </summary>
	/// <param name="chunk">Chunk.</param>
	public void RemoveFromNeighborhood(XTerrainChunk chunk)
	{
		if (Neighborhood.XDown == chunk)
			Neighborhood.XDown = null;

		if (Neighborhood.XUp == chunk)
			Neighborhood.XUp = null;

		if (Neighborhood.ZDown == chunk)
			Neighborhood.ZDown = null;

		if (Neighborhood.ZUp == chunk)
			Neighborhood.ZUp = null;
	}

	/// <summary>
	/// Sets the neighbors.
	/// </summary>
	/// <param name="chunk">Chunk.</param>
	/// <param name="direction">Direction.</param>
	public void SetNeighbors(XTerrainChunk chunk, TerrainNeighbor direction)
	{
		if (chunk != null)
		{
			switch (direction)
			{
			case TerrainNeighbor.XUp:
				Neighborhood.XUp = chunk;
				break;

			case TerrainNeighbor.XDown:
				Neighborhood.XDown = chunk;
				break;

			case TerrainNeighbor.ZUp:
				Neighborhood.ZUp = chunk;
				break;

			case TerrainNeighbor.ZDown:
				Neighborhood.ZDown = chunk;
				break;
			}
		}
	}

	/// <summary>
	/// Updates the neighbors.
	/// </summary>
	public void UpdateNeighbors()
	{
		if (Terrain != null)
		{
			var xDown 	= Neighborhood.XDown == null ? null : Neighborhood.XDown.Terrain;
			var xUp 	= Neighborhood.XUp == null ? null : Neighborhood.XUp.Terrain;
			var zDown 	= Neighborhood.ZDown == null ? null : Neighborhood.ZDown.Terrain;
			var zUp 	= Neighborhood.ZUp == null ? null : Neighborhood.ZUp.Terrain;

			Terrain.SetNeighbors(xDown, zUp, xUp, zDown);
			Terrain.Flush();
		}
	}
}
