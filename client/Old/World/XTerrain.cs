using UnityEngine;
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
	private Terrain 				TerrainChunk 
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
    private List<Vector3>           TreeStump
    { get; set; }

    /// <summary>
    /// 
    /// </summary>
    private List<TreeInstance>      TreeInstances
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
        TreeStump           = new List<Vector3>();
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
            Heightmap = NoiseProvider.GetHeightmapData();

            if (Setting.TreePrefabs.Count > 0)
            {
                for (var zRes = 0; zRes < Setting.HeightmapResolution; zRes++)
                {
                    for (var xRes = 0; xRes < Setting.HeightmapResolution; xRes++)
                    {
                        int height = Mathf.FloorToInt(Heightmap[zRes, xRes] * 100);
                        if (height > Setting.StumpHeigt && 
                            zRes % Setting.StumpDensity == 0 && 
                            xRes % Setting.StumpDensity == 0 && TreeStump.Count < Setting.MaxStempCount)
                        {
                            var xCoordinate = (float)xRes / (Setting.HeightmapResolution - 1);
                            var zCoordinate = (float)zRes / (Setting.HeightmapResolution - 1);

                            Vector3 point = new Vector3(xCoordinate, zCoordinate, height);
                            TreeStump.Add(point);
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
		return (TerrainChunk == null && Heightmap != null);
	}

	/// <summary>
	/// Gets the height of the terrain.
	/// </summary>
	/// <returns>The terrain height.</returns>
	/// <param name="worldPosition">World position.</param>
	public float 	GetTerrainHeight(Vector3 worldPosition)
	{
        return TerrainChunk.SampleHeight(worldPosition);
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
        TerrainChunk = newTerrainGameObject.GetComponent<Terrain>();
        TerrainChunk.heightmapPixelError = 8;
        TerrainChunk.materialType = UnityEngine.Terrain.MaterialType.Custom;
        TerrainChunk.materialTemplate = Setting.TerrainMaterial;
        TerrainChunk.reflectionProbeUsage = UnityEngine.Rendering.ReflectionProbeUsage.Off;
        TerrainChunk.Flush();

        ApplyTrees(Data);
	}

    private int GetTreeIndex(float height)
    {
        if (height > 0 && height < 15)
        {
            return 0;
        }

        if (height > 15 && height < 30)
        {
            return 1;
        }

        if (height > 30 && height < 45)
        {
            return 2;
        }

        return 3;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="terrainData"></param>
    private void ApplyTrees(TerrainData terrainData)
    {
        if (Setting.TreePrefabs.Count > 0)
        {

            List<TreePrototype> trees = new List<TreePrototype>();
            for (int i = 0; i < Setting.TreePrefabs.Count; i++)
            {
                TreePrototype tree = new TreePrototype();
                tree.prefab = Setting.TreePrefabs[i];

                trees.Add(tree);
            }

            terrainData.treePrototypes = trees.ToArray();

            for (int i = 0; i < TreeStump.Count; i++)
            {
                Vector3 vpos = TreeStump[i];

                for (int j = 0; j < Setting.TreeGenerateDensity; j++)
                {
                    TreeInstance tmpTreeInstances = new TreeInstance();
                    tmpTreeInstances.prototypeIndex = GetTreeIndex(vpos.y);
                    tmpTreeInstances.position = new Vector3(Random.Range(vpos.x - 0.1f, vpos.x + 0.1f), -0.1f, Random.Range(vpos.y - 0.1f, vpos.y + 0.1f));
                    tmpTreeInstances.color = new Color(1, 1, 1, 1);
                    tmpTreeInstances.lightmapColor = new Color(1, 1, 1, 1);

                    float scale = Random.Range(0.8f, 1f);
                    tmpTreeInstances.heightScale = scale;
                    tmpTreeInstances.widthScale = scale;
                    TerrainChunk.AddTreeInstance(tmpTreeInstances);
                }
            }

            TerrainCollider tc = TerrainChunk.GetComponent<TerrainCollider>();
            tc.enabled = false;
            tc.enabled = true;
        }
    }

    public void ReplaceTree()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 10.0f))
        {
            List<TreeInstance> allTrees = new List<TreeInstance>(TerrainChunk.terrainData.treeInstances);
            for (int i = 0; i < allTrees.Count; i++)
            {
                allTrees.RemoveAt(i);
            }

            TerrainChunk.terrainData.treeInstances = allTrees.ToArray();

            float[,] heights = TerrainChunk.terrainData.GetHeights(0, 0, 0, 0);
            TerrainChunk.terrainData.SetHeights(0, 0, heights);

            TerrainChunk.Flush();
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

        if (TerrainChunk != null)
            GameObject.Destroy(TerrainChunk.gameObject);
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
        if (TerrainChunk != null)
		{
            var xDown = Neighborhood.XDown == null ? null : Neighborhood.XDown.TerrainChunk;
            var xUp = Neighborhood.XUp == null ? null : Neighborhood.XUp.TerrainChunk;
            var zDown = Neighborhood.ZDown == null ? null : Neighborhood.ZDown.TerrainChunk;
            var zUp = Neighborhood.ZUp == null ? null : Neighborhood.ZUp.TerrainChunk;

            TerrainChunk.SetNeighbors(xDown, zUp, xUp, zDown);
            TerrainChunk.Flush();
		}
	}
}
