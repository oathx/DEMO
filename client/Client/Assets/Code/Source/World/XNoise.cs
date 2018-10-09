﻿using UnityEngine;
using SLua;


/// <summary>
/// X noise.
/// </summary>
[CustomLuaClass]
public abstract class XNoise{
    public int left 	= 1;
    public int right 	= 5;
    public int top 		= 2;
    public int bottom 	= 6;

	/// <summary>
	/// Gets the value.
	/// </summary>
	/// <returns>The value.</returns>
	/// <param name="x">The x coordinate.</param>
	/// <param name="z">The z coordinate.</param>
    abstract public float GetValue(float x, float z);

	/// <summary>
	/// Gets the height map data.
	/// </summary>
	/// <returns>The height map data.</returns>
	/// <param name="w">The width.</param>
	/// <param name="h">The height.</param>
    abstract public float[,] GetHeightmapData(int w, int h, int x, int z);

	/// <summary>
	/// Gets the alphamap.
	/// </summary>
	/// <returns>The alphamap.</returns>
    abstract public Texture2D GetAlphamap();
}

/// <summary>
/// 
/// </summary>
public class XNoiseDefault : XNoise
{
    private LibNoise.Generator.Perlin perlinNoiseGenerator;

    /// <summary>
    /// 
    /// </summary>
    public XNoiseDefault()
    {
        perlinNoiseGenerator = new LibNoise.Generator.Perlin();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    public override float GetValue(float x, float z)
    {
        return (float)(perlinNoiseGenerator.GetValue(x, 0, z) / 2f) + 0.5f;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="w"></param>
    /// <param name="h"></param>
    /// <returns></returns>
    public override float[,] GetHeightmapData(int w, int h, int x, int z)
    {
        var heightmap = new float[w, h];

        for (var zRes = 0; zRes < w; zRes++)
        {
            for (var xRes = 0; xRes < h; xRes++)
            {
                var xCoordinate = x + (float)xRes / (h - 1);
                var zCoordinate = z + (float)zRes / (h - 1);

                heightmap[zRes, xRes] = GetValue(xCoordinate, zCoordinate);
            }
        }

        return heightmap;
    }

    /// <summary>
    /// Gets the alphamap texture.
    /// </summary>
    /// <returns>The alphamap texture.</returns>
    public override Texture2D GetAlphamap()
    {
        return null;
    }
}

/// <summary>
/// X noise perlin.
/// </summary>
public class XNoisePerlin : XNoise
{
	private LibNoise.Generator.Perlin 	perlinNoiseGenerator;
	private LibNoise.Noise2D 			heightMapBuilder;

	/// <summary>
	/// Initializes a new instance of the <see cref="XNoisePerlin"/> class.
	/// </summary>
	public XNoisePerlin()
	{
		perlinNoiseGenerator = new LibNoise.Generator.Perlin();
	}

	/// <summary>
	/// Gets the value.
	/// </summary>
	/// <returns>The value.</returns>
	/// <param name="x">The x coordinate.</param>
	/// <param name="z">The z coordinate.</param>
	public override float 		GetValue(float x, float z)
	{
		return (float)(perlinNoiseGenerator.GetValue(x, 0, z) / 2f) + 0.5f;
	}

	/// <summary>
	/// Gets the height map data.
	/// </summary>
	/// <returns>The height map data.</returns>
	/// <param name="w">The width.</param>
	/// <param name="h">The height.</param>
    public override float[,] GetHeightmapData(int w, int h, int x, int z)
	{
		heightMapBuilder = new LibNoise.Noise2D (w, h, perlinNoiseGenerator);
		heightMapBuilder.GeneratePlanar (left, right, top, bottom);

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
	
