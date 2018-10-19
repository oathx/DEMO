using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class XTerrain : MonoBehaviour
{
    const float viewerMoveThresholdForChunkUpdate = 25f;
    const float sqrViewerMoveThresholdForChunkUpdate = viewerMoveThresholdForChunkUpdate * viewerMoveThresholdForChunkUpdate;

    public int colliderLODIndex;
    public XTerrainLODInfo[] detailLevels;

    public XTerrainAsset asset;

    public Transform viewer;
    public Material mapMaterial;

    public Vector2 viewerPosition;
    public Vector2 viewerPositionOld;

    private float meshWorldSize;
    private int chunksVisibleInViewDst;

    private Dictionary<Vector2, XTerrainChunk> terrainChunkDictionary = new Dictionary<Vector2, XTerrainChunk>();
    private List<XTerrainChunk> visibleTerrainChunks = new List<XTerrainChunk>();
    private Queue<ThreadInfo> dataQueue = new Queue<ThreadInfo>();

    public static XTerrain instance;
    
    struct ThreadInfo
    {
        public readonly Action<object> callback;
        public readonly object parameter;

        public ThreadInfo(Action<object> callback, object parameter)
        {
            this.callback = callback;
            this.parameter = parameter;
        }
    }

    void Awake()
    {
        instance = FindObjectOfType<XTerrain>();
    }

    void Start()
    {

        asset.ApplyToMaterial(mapMaterial);
        asset.UpdateMeshHeights(mapMaterial, asset.minHeight, asset.maxHeight);

        float maxViewDst = detailLevels[detailLevels.Length - 1].visibleDstThreshold;
        meshWorldSize = asset.meshWorldSize;
        chunksVisibleInViewDst = Mathf.RoundToInt(maxViewDst / meshWorldSize);

        UpdateVisibleChunks();
    }

    void Update()
    {
        viewerPosition = new Vector2(viewer.position.x, viewer.position.z);

        if (viewerPosition != viewerPositionOld)
        {
            foreach (XTerrainChunk chunk in visibleTerrainChunks)
            {
                chunk.UpdateCollisionMesh();
            }
        }

        if ((viewerPositionOld - viewerPosition).sqrMagnitude > sqrViewerMoveThresholdForChunkUpdate)
        {
            viewerPositionOld = viewerPosition;
            UpdateVisibleChunks();
        }

        UpdateThread();
    }

    public static void RequestData(Func<object> generateData, Action<object> callback)
    {
        ThreadStart threadStart = delegate
        {
            instance.DataThread(generateData, callback);
        };

        new Thread(threadStart).Start();
    }

    private void UpdateVisibleChunks()
    {
        HashSet<Vector2> alreadyUpdatedChunkCoords = new HashSet<Vector2>();
        for (int i = visibleTerrainChunks.Count - 1; i >= 0; i--)
        {
            alreadyUpdatedChunkCoords.Add(visibleTerrainChunks[i].coord);
            visibleTerrainChunks[i].UpdateTerrainChunk();
        }

        int currentChunkCoordX = Mathf.RoundToInt(viewerPosition.x / meshWorldSize);
        int currentChunkCoordY = Mathf.RoundToInt(viewerPosition.y / meshWorldSize);

        for (int yOffset = -chunksVisibleInViewDst; yOffset <= chunksVisibleInViewDst; yOffset++)
        {
            for (int xOffset = -chunksVisibleInViewDst; xOffset <= chunksVisibleInViewDst; xOffset++)
            {
                Vector2 viewedChunkCoord = new Vector2(currentChunkCoordX + xOffset, currentChunkCoordY + yOffset);
                if (!alreadyUpdatedChunkCoords.Contains(viewedChunkCoord))
                {
                    if (terrainChunkDictionary.ContainsKey(viewedChunkCoord))
                    {
                        terrainChunkDictionary[viewedChunkCoord].UpdateTerrainChunk();
                    }
                    else
                    {
                        XTerrainChunk newChunk = new XTerrainChunk(viewedChunkCoord, asset, detailLevels, colliderLODIndex, transform, viewer, mapMaterial);
                        terrainChunkDictionary.Add(viewedChunkCoord, newChunk);
                        newChunk.onVisibilityChanged += OnTerrainChunkVisibilityChanged;
                        newChunk.Load();
                    }
                }

            }
        }
    }

    private void OnTerrainChunkVisibilityChanged(XTerrainChunk chunk, bool isVisible)
    {
        if (isVisible)
        {
            visibleTerrainChunks.Add(chunk);
        }
        else
        {
            visibleTerrainChunks.Remove(chunk);
        }
    }

    private void DataThread(Func<object> generateData, Action<object> callback)
    {
        object data = generateData();
        lock (dataQueue)
        {
            dataQueue.Enqueue(new ThreadInfo(callback, data));
        }
    }

    private void UpdateThread()
    {
        if (dataQueue.Count > 0)
        {
            for (int i = 0; i < dataQueue.Count; i++)
            {
                ThreadInfo threadInfo = dataQueue.Dequeue();
                threadInfo.callback(threadInfo.parameter);
            }
        }
    }
}
