﻿using UnityEngine;
using System.Threading;
using SLua;
using System.Collections.Generic;

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
    /// 
    /// </summary>
    private List<Vector2>           TreePoint
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
            Heightmap = NoiseProvider.GetHeightmapData(Setting.HeightmapResolution, Setting.HeightmapResolution, Position.X, Position.Z);

            if (Setting.Trees.Count > 0)
            {
                TreePoint = new List<Vector2>();
                int baseVal = 10;
                int heightV = 30;

                int minHeightmapResolution = Setting.HeightmapResolution < 33 ? 33 : Setting.HeightmapResolution;
                for (var zRes = 0; zRes < Setting.HeightmapResolution; zRes++)
                {
                    for (var xRes = 0; xRes < Setting.HeightmapResolution; xRes++)
                    {
                        float v = Heightmap[zRes, xRes];
                        if (Mathf.FloorToInt(v * 100) > heightV)
                        {
                            if (zRes % baseVal == 0 && xRes % baseVal == 0)
                            {
                                var xCoordinate = (float)xRes / (minHeightmapResolution - 1);
                                var zCoordinate = (float)zRes / (minHeightmapResolution - 1);

                                TreePoint.Add(new Vector2(xCoordinate, zCoordinate));
                            }
                        }
                    }
                }
            }
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

        ApplyTrees(Data);
	}

    /// <summary>
    /// 
    /// </summary>
    /// <param name="terrainData"></param>
    private void ApplyTrees(TerrainData terrainData)
    {
        if (Setting.Trees.Count > 0)
        {
            List<TreePrototype> trees = new List<TreePrototype>();
            for (int i = 0; i < Setting.Trees.Count; i++)
            {
                TreePrototype tree = new TreePrototype();
                tree.prefab = Setting.Trees[i];

                trees.Add(tree);
            }

            terrainData.treePrototypes = trees.ToArray();
            Debug.LogError(TreePoint.Count);
            foreach (var vpos in TreePoint)
            {
                for (int i = 0; i < 5; i++)
                {
                    TreeInstance tmpTreeInstances = new TreeInstance();
                    tmpTreeInstances.prototypeIndex = Random.Range(0, Setting.Trees.Count - 1); // ID of tree prototype
                    tmpTreeInstances.position = new Vector3(Random.Range(vpos.x - 0.1f, vpos.x + 0.1f), -0.1f, Random.Range(vpos.y - 0.1f, vpos.y + 0.1f));   // not regular pos,  [0,1]
                    tmpTreeInstances.color = new Color(1, 1, 1, 1);
                    tmpTreeInstances.lightmapColor = new Color(1, 1, 1, 1);//must add
                    float ss = Random.Range(0.8f, 1f);
                    tmpTreeInstances.heightScale = ss; //same size as prototype
                    tmpTreeInstances.widthScale = ss;
                    Terrain.AddTreeInstance(tmpTreeInstances);
                }
            }

            TerrainCollider tc = Terrain.GetComponent<TerrainCollider>();
            tc.enabled = false;
            tc.enabled = true;
        }
    }

	/// <summary>
	/// Applies the textures.
	/// </summary>
	/// <param name="terrainData">Terrain data.</param>
	private void 	ApplyTextures(TerrainData terrainData)
	{
        List<SplatPrototype> splats = new List<SplatPrototype>();
        for (int i = 0; i < Setting.Textures.Count; i++)
        {
            SplatPrototype splat = new SplatPrototype();
            splat.texture = Setting.Textures[i];

            splats.Add(splat);
        }

        terrainData.splatPrototypes = splats.ToArray();
		terrainData.RefreshPrototypes();

        Texture2D alphamap = NoiseProvider.GetAlphamap();
		// proc alphamap
        var splatMap = new float[terrainData.alphamapResolution, terrainData.alphamapResolution, splats.Count];
		for (var zRes = 0; zRes < terrainData.alphamapHeight; zRes++)
		{
			for (var xRes = 0; xRes < terrainData.alphamapWidth; xRes++)
			{
                Color clr   = alphamap.GetPixel(zRes, xRes);
                
                splatMap[zRes, xRes, 0] = clr.r;
                splatMap[zRes, xRes, 1] = clr.g;
                splatMap[zRes, xRes, 2] = clr.b;
                splatMap[zRes, xRes, 3] = clr.a;
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
