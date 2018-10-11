using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using SLua;

public enum XNoiseType {
    NT_NONE = 0,
    NT_PERLIN = 1,
};

[CustomLuaClass]
public class XTerrainGenerator : MonoBehaviour {
	/// <summary>
	/// The settings.
	/// </summary>
    [SerializeField]
	public XTerrainChunkSetting 	Setting;

	/// <summary>
	/// The cache.
	/// </summary>
    public XTerrainChunkCache       Cache;

    /// <summary>
    /// 
    /// </summary>
    public Transform                Center;

    private bool                    canActivateCharacter;
    private XVec2I                  centerChunkPosition;

    /// <summary>
    /// 
    /// </summary>
    void Awake()
    {
        canActivateCharacter = false;
        InitGenerate();
    }

    void Start()
    {
        StartCoroutine(OnLoadWorld(delegate(XVec2I v)
        {

        }));
    }

	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update()
	{
        if (canActivateCharacter && Center.gameObject.activeSelf)
        {
            XVec2I curChunkPosition = GetChunkPosition(Center.position);
            if (!curChunkPosition.Equals(centerChunkPosition))
            {
                UpdateTerrain(Center.position, Setting.Raidus);
                centerChunkPosition = curChunkPosition;
            }
        }

        Cache.Update();
	}

	/// <summary>
	/// Inits the generate.
	/// </summary>
	public void 			InitGenerate(){
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
            XNoisePerlin noise = new XNoisePerlin(Setting.HeightmapResolution,
               Setting.AlphamapResolution, x, z, Setting.Left, Setting.Right, Setting.Top, Setting.Bottom);

            if (x > 0)
            {
                int offset = (noise.bottom - noise.top) * x;
                noise.top = noise.top + offset;
                noise.bottom = noise.bottom + offset;
            }
            else
            {
                int offset = (noise.bottom - noise.top) * x;
                noise.top = noise.top + offset;
                noise.bottom = noise.bottom + offset;
            }

            if (z > 0)
            {
                int offset = (noise.right - noise.left) * z;
                noise.left = noise.left + offset;
                noise.right = noise.right + offset;
            }
            else
            {
                int offset = (noise.right - noise.left) * z;
                noise.left = noise.left + offset;
                noise.right = noise.right + offset;
            }

            var chunk = new XTerrainChunk(Setting, noise, x, z);
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

		for (var zCircle = -radius; zCircle < radius; zCircle++)
		{
			for (var xCircle = -radius; xCircle < radius; xCircle++)
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
        int x = (int)Mathf.Floor(worldPosition.x / Setting.Length);
        int z = (int)Mathf.Floor(worldPosition.z / Setting.Length);

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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="worldPosition"></param>
    /// <returns></returns>
    public XTerrainChunk    GetTerrainChunk(Vector3 worldPosition)
    {
        XVec2I chunkPosition = GetChunkPosition(worldPosition);
        return Cache.GetGeneratedChunk(chunkPosition);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="complate"></param>
    /// <returns></returns>
    IEnumerator             OnLoadWorld(System.Action<XVec2I> complate)
    {
        if (!Center)
        {
            GameObject orgin = new GameObject();
            orgin.transform.position = Vector3.zero;
            Center = orgin.transform;
        }

        InitGenerate();
        UpdateTerrain(Center.position, Setting.Raidus);

        do
        {
            var exists = IsTerrainAvailable(Center.position);
            if (exists)
                canActivateCharacter = true;
            yield return null;

        } while (!canActivateCharacter);

        centerChunkPosition = GetChunkPosition(Center.position);
        Center.position = new Vector3(Center.position.x,
            GetTerrainHeight(Center.position) + 0.5f, Center.position.z);

        Center.gameObject.SetActive(true);

        if (complate != null)
        {
            complate(centerChunkPosition);
        }
    }
}
