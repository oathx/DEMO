using UnityEngine;
using SLua;


/// <summary>
/// X noise.
/// </summary>
[CustomLuaClass]
public class XNoise{
	/// <summary>
	/// Initializes a new instance of the <see cref="XNoise"/> class.
	/// </summary>
	public XNoise(){
	}

	/// <summary>
	/// Gets the value.
	/// </summary>
	/// <returns>The value.</returns>
	/// <param name="x">The x coordinate.</param>
	/// <param name="z">The z coordinate.</param>
	public virtual float	GetValue(float x, float z){
		return 0f;
	}
}

/// <summary>
/// X noise perlin.
/// </summary>
public class XNoisePerlin : XNoise
{
	private LibNoise.Generator.Perlin PerlinNoiseGenerator;

	/// <summary>
	/// Initializes a new instance of the <see cref="XNoisePerlin"/> class.
	/// </summary>
	public XNoisePerlin()
	{
		PerlinNoiseGenerator = new LibNoise.Generator.Perlin();
	}

	/// <summary>
	/// Gets the value.
	/// </summary>
	/// <returns>The value.</returns>
	/// <param name="x">The x coordinate.</param>
	/// <param name="z">The z coordinate.</param>
	public override float GetValue(float x, float z)
	{
		return (float)(PerlinNoiseGenerator.GetValue(x, 0, z) / 2f) + 0.5f;
	}
}
	
