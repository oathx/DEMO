using UnityEngine;

[System.Serializable]
public struct XTerrainLODInfo
{
    [Range(0, XTerrainAsset.numSuppLODs - 1)]
    public int lod;
    public float visibleDstThreshold;

    public float sqrVisibleDstThreshold
    {
        get
        {
            return visibleDstThreshold * visibleDstThreshold;
        }
    }
}

public class XTerrainChunk
{
    const float colliderGenerationDistanceThreshold = 5;
    public event System.Action<XTerrainChunk, bool> onVisibilityChanged;
    public Vector2 coord;

    GameObject meshObject;
    Vector2 sampleCentre;
    Bounds bounds;

    MeshRenderer meshRenderer;
    MeshFilter meshFilter;
    MeshCollider meshCollider;

    XTerrainLODInfo[] detailLevels;
    LODMesh[] lodMeshes;
    int colliderLODIndex;

    XTerrainHeightmap heightMap;
    bool heightMapReceived;
    int previousLODIndex = -1;
    bool hasSetCollider;
    float maxViewDst;

    XTerrainAsset asset;
    Transform viewer;

    public XTerrainChunk(Vector2 coord, XTerrainAsset asset, XTerrainLODInfo[] detailLevels, int colliderLODIndex, Transform parent, Transform viewer, Material material)
    {
        this.coord = coord;
        this.detailLevels = detailLevels;
        this.colliderLODIndex = colliderLODIndex;
        this.asset = asset;
        this.viewer = viewer;

        sampleCentre = coord * asset.meshWorldSize / asset.meshScale;
        Vector2 position = coord * asset.meshWorldSize;
        bounds = new Bounds(position, Vector2.one * asset.meshWorldSize);


        meshObject = new GameObject(string.Format("Chunk({0}.{1})", coord.x, coord.y));
        meshRenderer = meshObject.AddComponent<MeshRenderer>();
        meshFilter = meshObject.AddComponent<MeshFilter>();
        meshCollider = meshObject.AddComponent<MeshCollider>();
        meshRenderer.material = material;

        meshObject.transform.position = new Vector3(position.x, 0, position.y);
        meshObject.transform.parent = parent;
        SetVisible(false);

        lodMeshes = new LODMesh[detailLevels.Length];
        for (int i = 0; i < detailLevels.Length; i++)
        {
            lodMeshes[i] = new LODMesh(detailLevels[i].lod);
            lodMeshes[i].updateCallback += UpdateTerrainChunk;
            if (i == colliderLODIndex)
            {
                lodMeshes[i].updateCallback += UpdateCollisionMesh;
            }
        }

        maxViewDst = detailLevels[detailLevels.Length - 1].visibleDstThreshold;

    }

    public void Load()
    {
        XTerrain.RequestData(() => XTerrainHeightmapGenerate.GenerateHeightmap(asset.numVertsPerLine, asset.numVertsPerLine, asset, sampleCentre), OnHeightMapReceived);
    }

    void OnHeightMapReceived(object heightMapObject)
    {
        this.heightMap = (XTerrainHeightmap)heightMapObject;
        heightMapReceived = true;

        UpdateTerrainChunk();
    }

    Vector2 viewerPosition
    {
        get
        {
            return new Vector2(viewer.position.x, viewer.position.z);
        }
    }


    public void UpdateTerrainChunk()
    {
        if (heightMapReceived)
        {
            float viewerDstFromNearestEdge = Mathf.Sqrt(bounds.SqrDistance(viewerPosition));

            bool wasVisible = IsVisible();
            bool visible = viewerDstFromNearestEdge <= maxViewDst;

            if (visible)
            {
                int lodIndex = 0;

                for (int i = 0; i < detailLevels.Length - 1; i++)
                {
                    if (viewerDstFromNearestEdge > detailLevels[i].visibleDstThreshold)
                    {
                        lodIndex = i + 1;
                    }
                    else
                    {
                        break;
                    }
                }

                if (lodIndex != previousLODIndex)
                {
                    LODMesh lodMesh = lodMeshes[lodIndex];
                    if (lodMesh.hasMesh)
                    {
                        previousLODIndex = lodIndex;
                        meshFilter.mesh = lodMesh.mesh;
                    }
                    else if (!lodMesh.hasRequestedMesh)
                    {
                        lodMesh.RequestMesh(heightMap, asset);
                    }
                }


            }

            if (wasVisible != visible)
            {

                SetVisible(visible);
                if (onVisibilityChanged != null)
                {
                    onVisibilityChanged(this, visible);
                }
            }
        }
    }

    public void UpdateCollisionMesh()
    {
        if (!hasSetCollider)
        {
            float sqrDstFromViewerToEdge = bounds.SqrDistance(viewerPosition);

            if (sqrDstFromViewerToEdge < detailLevels[colliderLODIndex].sqrVisibleDstThreshold)
            {
                if (!lodMeshes[colliderLODIndex].hasRequestedMesh)
                {
                    lodMeshes[colliderLODIndex].RequestMesh(heightMap, asset);
                }
            }

            if (sqrDstFromViewerToEdge < colliderGenerationDistanceThreshold * colliderGenerationDistanceThreshold)
            {
                if (lodMeshes[colliderLODIndex].hasMesh)
                {
                    meshCollider.sharedMesh = lodMeshes[colliderLODIndex].mesh;
                    hasSetCollider = true;
                }
            }
        }
    }

    public void SetVisible(bool visible)
    {
        meshObject.SetActive(visible);
    }

    public bool IsVisible()
    {
        return meshObject.activeSelf;
    }

}

class LODMesh
{
    public Mesh mesh;
    public bool hasRequestedMesh;
    public bool hasMesh;
    int lod;
    public event System.Action updateCallback;

    public LODMesh(int lod)
    {
        this.lod = lod;
    }

    void OnMeshDataReceived(object meshDataObject)
    {
        mesh = ((XTerrainMeshData)meshDataObject).CreateMesh();
        hasMesh = true;

        updateCallback();
    }

    public void RequestMesh(XTerrainHeightmap heightMap, XTerrainAsset asset)
    {
        hasRequestedMesh = true;
        XTerrain.RequestData(() => XTerrainMeshGenerator.GenerateTerrainMesh(heightMap.noisemap, asset, lod), OnMeshDataReceived);
    }

}