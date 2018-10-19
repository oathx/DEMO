using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum XNoiseNormalizeMode { 
    Local, 
    Global 
};

/// <summary>
/// 
/// </summary>
[System.Serializable]
public class XNoiseSetting
{
    public XNoiseNormalizeMode normalizeMode;

    [Range(0, 1)]
    public float persistance = .6f;
    public float lacunarity = 2;
    public float scale = 50;

    public int octaves = 6;
    public int seed;
    public Vector2 offset;

    /// <summary>
    /// 
    /// </summary>
    public void                 ValidateValues()
    {
        scale = Mathf.Max(scale, 0.01f);
        octaves = Mathf.Max(octaves, 1);
        lacunarity = Mathf.Max(lacunarity, 1);
        persistance = Mathf.Clamp01(persistance);
    }
}

public static class XNoise
{
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, XNoiseSetting settings, Vector2 sampleCentre)
    {
        float[,] noiseMap = new float[mapWidth, mapHeight];

        System.Random prng = new System.Random(settings.seed);
        Vector2[] octaveOffsets = new Vector2[settings.octaves];

        float maxPossibleHeight = 0;
        float amplitude = 1;
        float frequency = 1;

        for (int i = 0; i < settings.octaves; i++)
        {
            float offsetX = prng.Next(-100000, 100000) + settings.offset.x + sampleCentre.x;
            float offsetY = prng.Next(-100000, 100000) - settings.offset.y - sampleCentre.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);

            maxPossibleHeight += amplitude;
            amplitude *= settings.persistance;
        }

        float maxLocalNoiseHeight = float.MinValue;
        float minLocalNoiseHeight = float.MaxValue;

        float halfWidth = mapWidth / 2f;
        float halfHeight = mapHeight / 2f;

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                amplitude = 1;
                frequency = 1;
                float noiseHeight = 0;

                for (int i = 0; i < settings.octaves; i++)
                {
                    float sampleX = (x - halfWidth + octaveOffsets[i].x) / settings.scale * frequency;
                    float sampleY = (y - halfHeight + octaveOffsets[i].y) / settings.scale * frequency;

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                    noiseHeight += perlinValue * amplitude;

                    amplitude *= settings.persistance;
                    frequency *= settings.lacunarity;
                }

                if (noiseHeight > maxLocalNoiseHeight)
                {
                    maxLocalNoiseHeight = noiseHeight;
                }
                if (noiseHeight < minLocalNoiseHeight)
                {
                    minLocalNoiseHeight = noiseHeight;
                }
                noiseMap[x, y] = noiseHeight;

                if (settings.normalizeMode == XNoiseNormalizeMode.Global)
                {
                    float normalizedHeight = (noiseMap[x, y] + 1) / (maxPossibleHeight / 0.9f);
                    noiseMap[x, y] = Mathf.Clamp(normalizedHeight, 0, int.MaxValue);
                }
            }
        }

        if (settings.normalizeMode == XNoiseNormalizeMode.Local)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    noiseMap[x, y] = Mathf.InverseLerp(minLocalNoiseHeight, maxLocalNoiseHeight, noiseMap[x, y]);
                }
            }
        }

        return noiseMap;
    }
}

[CreateAssetMenu()]
public class XTerrainAsset : XScriptAssetUpdateObject
{
    public XNoiseSetting noiseSettings;
    public bool useFalloff;
    public float heightMultiplier = 20;
    public AnimationCurve heightCurve;

    public static readonly int[] supChunkSizes = { 48, 72, 96, 120, 144, 168, 192, 216, 240 };
    public const int numSuppLODs = 5;
    public const int numSupChunkSizes = 9;
    public const int numSupFlatshadedChunkSizes = 3;

    public float meshScale = 2.5f;
    public bool useFlatShading;

    [Range(0, numSupChunkSizes - 1)]
    public int chunkSizeIndex;

    [Range(0, numSupFlatshadedChunkSizes - 1)]
    public int flatshadedChunkSizeIndex;

    public float minHeight
    {
        get
        {
            return heightMultiplier * heightCurve.Evaluate(0);
        }
    }

    public float maxHeight
    {
        get
        {
            return heightMultiplier * heightCurve.Evaluate(1);
        }
    }

    public int numVertsPerLine
    {
        get
        {
            return supChunkSizes[(useFlatShading) ? flatshadedChunkSizeIndex : chunkSizeIndex] + 5;
        }
    }

    public float meshWorldSize
    {
        get
        {
            return (numVertsPerLine - 3) * meshScale;
        }
    }

#if UNITY_EDITOR
    protected override void OnValidate()
    {
        noiseSettings.ValidateValues();
        base.OnValidate();
    }
#endif

    [System.Serializable]
    public class XTerrainLayer
    {
        public Texture2D texture;
        public Color tint;
        [Range(0, 1)]
        public float tintStrength;
        [Range(0, 1)]
        public float startHeight;
        [Range(0, 1)]
        public float blendStrength;
        public float textureScale;
    }

    const TextureFormat textureFormat = TextureFormat.RGB565;
    private float savedMinHeight;
    private float savedMaxHeight;

    public int textureSize = 512;
    public XTerrainLayer[] layers;

    public void ApplyToMaterial(Material material)
    {
        material.SetInt("layerCount", layers.Length);
        material.SetColorArray("baseColours", layers.Select(x => x.tint).ToArray());
        material.SetFloatArray("baseStartHeights", layers.Select(x => x.startHeight).ToArray());
        material.SetFloatArray("baseBlends", layers.Select(x => x.blendStrength).ToArray());
        material.SetFloatArray("baseColourStrength", layers.Select(x => x.tintStrength).ToArray());
        material.SetFloatArray("baseTextureScales", layers.Select(x => x.textureScale).ToArray());

        Texture2DArray texturesArray = GenerateTextureArray(
            layers.Select(x => x.texture).ToArray());
        material.SetTexture("baseTextures", texturesArray);

        UpdateMeshHeights(material, savedMinHeight, savedMaxHeight);
    }

    public void UpdateMeshHeights(Material material, float minHeight, float maxHeight)
    {
        savedMinHeight = minHeight;
        savedMaxHeight = maxHeight;

        material.SetFloat("minHeight", minHeight);
        material.SetFloat("maxHeight", maxHeight);
    }

    private Texture2DArray GenerateTextureArray(Texture2D[] textures)
    {
        Texture2DArray textureArray = new Texture2DArray(textureSize, textureSize, textures.Length, textureFormat, true);
        for (int i = 0; i < textures.Length; i++)
        {
            textureArray.SetPixels(textures[i].GetPixels(), i);
        }
        textureArray.Apply();
        return textureArray;
    }

    [System.Serializable]
    public class XTreeLayer
    {
        public GameObject prefab;

        [Range(0, 1)]
        public float strength;
        [Range(0, 1)]
        public float startHeight;
    }

    public XTreeLayer[] trees;
}

