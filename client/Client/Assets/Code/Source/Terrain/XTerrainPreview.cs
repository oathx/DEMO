using UnityEngine;
using System.Collections;

public class XTerrainPreview : MonoBehaviour
{
    public Renderer textureRender;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;

    public enum DrawMode { 
        NoiseMap, 
        Mesh, 
        FalloffMap 
    };

    public DrawMode drawMode;

    public Material terrainMaterial;
    public XTerrainAsset asset;

    [Range(0, XTerrainAsset.numSuppLODs - 1)]
    public int editorPreviewLOD;
    public bool autoUpdate;

    public void DrawMapInEditor()
    {
        asset.ApplyToMaterial(terrainMaterial);
        asset.UpdateMeshHeights(terrainMaterial, asset.minHeight, asset.maxHeight);
        XTerrainHeightmap heightMap = XTerrainHeightmapGenerate.GenerateHeightmap(asset.numVertsPerLine, asset.numVertsPerLine, asset, Vector2.zero);

        if (drawMode == DrawMode.NoiseMap)
        {
            DrawTexture(XTerrainTextureGenerator.TextureFromHeightMap(heightMap));
        }
        else if (drawMode == DrawMode.Mesh)
        {
            DrawMesh(XTerrainMeshGenerator.GenerateTerrainMesh(heightMap.noisemap, asset, editorPreviewLOD));
        }
        else if (drawMode == DrawMode.FalloffMap)
        {
            DrawTexture(XTerrainTextureGenerator.TextureFromHeightMap(
                new XTerrainHeightmap(XTerrainFalloffGenerator.GenerateFalloffMap(asset.numVertsPerLine), 0, 1)));
        }
    }

    public void DrawTexture(Texture2D texture)
    {
        textureRender.sharedMaterial.mainTexture = texture;
        textureRender.transform.localScale = new Vector3(texture.width, 1, texture.height) / 10f;

        textureRender.gameObject.SetActive(true);
        meshFilter.gameObject.SetActive(false);
    }

    public void DrawMesh(XTerrainMeshData meshData)
    {
        meshFilter.sharedMesh = meshData.CreateMesh();

        textureRender.gameObject.SetActive(false);
        meshFilter.gameObject.SetActive(true);
    }

    private void OnValuesUpdated()
    {
        if (!Application.isPlaying)
        {
            DrawMapInEditor();
        }

        asset.ApplyToMaterial(terrainMaterial);
    }

    private void OnValidate()
    {
        if (asset != null)
        {
            asset.OnValuesUpdated -= OnValuesUpdated;
            asset.OnValuesUpdated += OnValuesUpdated;
        }
    }

}
