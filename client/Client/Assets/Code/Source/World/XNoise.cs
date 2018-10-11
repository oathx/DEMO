using UnityEngine;
using SLua;
using LibNoise;
using LibNoise.Generator;
using LibNoise.Operator;

/// <summary>
/// X noise.
/// </summary>
[CustomLuaClass]
public abstract class XNoise{
	/// <summary>
	/// Gets the height map data.
	/// </summary>
	/// <returns>The height map data.</returns>
	/// <param name="w">The width.</param>
	/// <param name="h">The height.</param>
    abstract public float[,] GetHeightmapData();

	/// <summary>
	/// Gets the alphamap.
	/// </summary>
	/// <returns>The alphamap.</returns>
    abstract public Texture2D GetAlphamap();
}

/// <summary>
/// X noise perlin.
/// </summary>
public class XNoisePerlin : XNoise
{
    private LibNoise.Generator.Perlin   perlinNoiseGenerator;
    private LibNoise.Noise2D            heightMapBuilder;

    private int                         heightmapResolution;
    private int                         alphamapResolution;
    private int                         chunkX;
    private int                         chunkZ;

    /// <summary>
    /// 
    /// </summary>
    public int left 
    { get; set; }
    public int right 
    { get; set; }
    public int top 
    { get; set; }
    public int bottom 
    { get; set; }

    public XNoisePerlin(int heightResolution, int alphaResolution, int x, int z, int l, int r, int t, int b)
	{
        heightmapResolution = heightResolution;
        alphamapResolution  = alphaResolution;
        left                = l;
        right               = r;
        top                 = t;
        bottom              = b;
        chunkX              = x;
        chunkZ              = z;

		perlinNoiseGenerator= new LibNoise.Generator.Perlin();

	}

	/// <summary>
	/// Gets the height map data.
	/// </summary>
	/// <returns>The height map data.</returns>
	/// <param name="w">The width.</param>
	/// <param name="h">The height.</param>
    public override float[,] GetHeightmapData()
	{
        heightMapBuilder = new LibNoise.Noise2D(heightmapResolution, alphamapResolution, perlinNoiseGenerator);

        heightMapBuilder.GeneratePlanar(left, right, top, bottom);
		return heightMapBuilder.GetData ();
	}

	/// <summary>
	/// Gets the alphamap texture.
	/// </summary>
	/// <returns>The alphamap texture.</returns>
    public override Texture2D GetAlphamap()
    {
		return heightMapBuilder.GetTexture (LibNoise.GradientPresets.RGBA);
	}
}