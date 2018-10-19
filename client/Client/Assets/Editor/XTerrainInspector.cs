//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.IO;
//using UnityEngine;
//using UnityEditor;

//[CustomEditor(typeof(XTerrain))]
//class XTerrainInspector : Editor
//{
//    private XTerrain instance;
//    private UnityEngine.SceneManagement.Scene scene;
//    private string assetRoot = "Assets/Resources/Terrain/ConfigAssetObject";

//    void OnEnable()
//    {
//        instance = target as XTerrain;

//        scene = UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene();
//        if (!Application.isPlaying)
//        {
//            UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(scene);
//        }

//        string fullPath = Path.Combine(Application.dataPath.Substring(0, Application.dataPath.Length - 6), assetRoot);
//        if (!Directory.Exists(fullPath))
//        {
//            Directory.CreateDirectory(fullPath);
//        }

//        instance.heightmapConfig = AssetDatabase.LoadAssetAtPath<XTerrainHeightmapConfig>(
//            Path.Combine(assetRoot, scene.name + typeof(XTerrainHeightmapConfig).Name + ".asset"));
//        instance.meshConfig = AssetDatabase.LoadAssetAtPath<XTerrainMeshConfig>(
//            Path.Combine(assetRoot, scene.name + typeof(XTerrainMeshConfig).Name + ".asset"));
//        instance.layerConfig = AssetDatabase.LoadAssetAtPath<XTerrainTextureLayerConfig>(
//            Path.Combine(assetRoot, scene.name + typeof(XTerrainTextureLayerConfig).Name + ".asset"));
//    }

//    void OnDisable()
//    {
//        if (instance.heightmapConfig)
//            EditorUtility.SetDirty(instance.heightmapConfig);

//        if (instance.meshConfig)
//            EditorUtility.SetDirty(instance.meshConfig);

//        if (instance.layerConfig)
//            EditorUtility.SetDirty(instance.layerConfig);

//        AssetDatabase.SaveAssets();
//        AssetDatabase.Refresh();
//    }

//    public override void OnInspectorGUI()
//    {
//        GUI.contentColor = new Color(1.0f, 1f, 0f);
//        EditorGUILayout.LabelField("AssetRoom", assetRoot);
//        GUI.contentColor = new Color(1, 1, 1);
//        GUI.backgroundColor = new Color(1.0f, 0f, 0f);
//        if (EditorHelper.DrawHeader("HeightmapConfig"))
//        {
//            EditorHelper.BeginContents();

//            if (!instance.heightmapConfig)
//            {
//                // create a new height map config object
//                if (GUILayout.Button("New"))
//                {
//                    instance.heightmapConfig = ScriptableObject.CreateInstance<XTerrainHeightmapConfig>();
//                    instance.heightmapConfig.heightCurve = new AnimationCurve();
//                    AssetDatabase.CreateAsset(instance.heightmapConfig,
//                        Path.Combine(assetRoot, scene.name + typeof(XTerrainHeightmapConfig).Name + ".asset"));

//                    EditorUtility.SetDirty(instance.heightmapConfig);
//                    AssetDatabase.SaveAssets();
//                    AssetDatabase.Refresh();
//                }
//            }
//            else
//            {
//                instance.heightmapConfig.useFalloff = EditorGUILayout.Toggle("useFalloff", instance.heightmapConfig.useFalloff);
//                instance.heightmapConfig.heightMultiplier = EditorGUILayout.FloatField("heightMultiplier", instance.heightmapConfig.heightMultiplier);

//                instance.heightmapConfig.heightCurve = (AnimationCurve)EditorGUILayout.CurveField("heightCurve", instance.heightmapConfig.heightCurve);
//                if (instance.heightmapConfig.heightCurve != null)
//                {
//                    EditorGUILayout.LabelField("minHeight", instance.heightmapConfig.minHeight.ToString());
//                    EditorGUILayout.LabelField("maxHeight", instance.heightmapConfig.maxHeight.ToString());
                    
//                }

//                GUI.backgroundColor = new Color(1.0f, 0f, 0f);
//                if (EditorHelper.DrawHeader("NoiseConfig"))
//                {
//                    EditorHelper.BeginContents();

//                    instance.heightmapConfig.noiseConfig.normalizeMode = (XTerrainNoise.NormalizeMode)EditorGUILayout.EnumPopup("normalizeMode", 
//                        instance.heightmapConfig.noiseConfig.normalizeMode);

//                    instance.heightmapConfig.noiseConfig.scale = EditorGUILayout.FloatField("scale", instance.heightmapConfig.noiseConfig.scale);
//                    instance.heightmapConfig.noiseConfig.octaves = EditorGUILayout.IntField("octaves", instance.heightmapConfig.noiseConfig.octaves);
//                    instance.heightmapConfig.noiseConfig.persistance = EditorGUILayout.Slider("persistance", instance.heightmapConfig.noiseConfig.persistance, 0.0f, 1.0f);
//                    instance.heightmapConfig.noiseConfig.lacunarity = EditorGUILayout.FloatField("lacunarity", instance.heightmapConfig.noiseConfig.lacunarity);
//                    instance.heightmapConfig.noiseConfig.seed = EditorGUILayout.IntField("seed", instance.heightmapConfig.noiseConfig.seed);
//                    instance.heightmapConfig.noiseConfig.offset = EditorGUILayout.Vector2Field("offset", instance.heightmapConfig.noiseConfig.offset);

//                    EditorHelper.EndContents();
//                }
//            }

//            EditorHelper.EndContents();
//        }

//        GUI.backgroundColor = new Color(1.0f, 0f, 0f);
//        if (EditorHelper.DrawHeader("MeshConfig"))
//        {
//            EditorHelper.BeginContents();
//            if (!instance.meshConfig)
//            {
//                if (GUILayout.Button("New"))
//                {
//                    instance.meshConfig = ScriptableObject.CreateInstance<XTerrainMeshConfig>();
//                    AssetDatabase.CreateAsset(instance.meshConfig,
//                        Path.Combine(assetRoot, scene.name + typeof(XTerrainMeshConfig).Name + ".asset"));

//                    EditorUtility.SetDirty(instance.meshConfig);
//                    AssetDatabase.SaveAssets();
//                    AssetDatabase.Refresh();
//                }
//            }
//            else
//            {
//                instance.meshConfig.meshScale = EditorGUILayout.FloatField("meshScale", instance.meshConfig.meshScale);

//                instance.meshConfig.chunkSizeIndex = Mathf.FloorToInt(EditorGUILayout.Slider("chunkSizeIndex",
//                    instance.meshConfig.chunkSizeIndex, 0.0f, 8.0f));
//                instance.meshConfig.flatshadedChunkSizeIndex = Mathf.FloorToInt(EditorGUILayout.Slider("flatshadedChunkSizeIndex",
//                    instance.meshConfig.flatshadedChunkSizeIndex, 0.0f, 2.0f));

//                EditorGUILayout.LabelField("numVertsPerLine", instance.meshConfig.numVertsPerLine.ToString());
//                EditorGUILayout.LabelField("meshWorldSize", instance.meshConfig.meshWorldSize.ToString());
//                EditorGUILayout.LabelField("LODs", XTerrainMeshConfig.numSupportedLODs.ToString());
//                EditorGUILayout.LabelField("ChunkSizes", XTerrainMeshConfig.numSupChunkSizes.ToString());
//                EditorGUILayout.LabelField("FlatshadedChunkSizes", XTerrainMeshConfig.numSupportedFlatshadedChunkSizes.ToString());

//                instance.meshConfig.useFlatShading = EditorGUILayout.Toggle("useFlatShading", instance.meshConfig.useFlatShading);


//                if (instance.heightmapConfig)
//                    EditorUtility.SetDirty(instance.heightmapConfig);

//                if (instance.meshConfig)
//                    EditorUtility.SetDirty(instance.meshConfig);

//                if (instance.layerConfig)
//                    EditorUtility.SetDirty(instance.layerConfig);
//            }
//            EditorHelper.EndContents();
//        }

//         GUI.backgroundColor = new Color(1.0f, 0f, 0f);
//         if (EditorHelper.DrawHeader("TextrueLayerConfig"))
//         {
//             EditorHelper.BeginContents();
//             if (!instance.layerConfig)
//             {
//                 if (GUILayout.Button("New"))
//                 {
//                     instance.layerConfig = ScriptableObject.CreateInstance<XTerrainTextureLayerConfig>();
//                     instance.layerConfig.layers = new XTerrainTextureLayerConfig.TextrueLayer[1];
//                     AssetDatabase.CreateAsset(instance.layerConfig, 
//                         Path.Combine(assetRoot, scene.name + typeof(XTerrainTextureLayerConfig).Name + ".asset"));

//                     EditorUtility.SetDirty(instance.layerConfig);
//                     AssetDatabase.SaveAssets();
//                     AssetDatabase.Refresh();
//                 }
//             }
//             else
//             {
//                 if (GUILayout.Button("Add"))
//                 {
//                     List<XTerrainTextureLayerConfig.TextrueLayer> list = instance.layerConfig.layers.ToList();
//                     list.Add(new XTerrainTextureLayerConfig.TextrueLayer());

//                     instance.layerConfig.layers = list.ToArray();
//                 }

//                 if (GUILayout.Button("Remove"))
//                 {
//                     List<XTerrainTextureLayerConfig.TextrueLayer> list = instance.layerConfig.layers.ToList();
//                     list.RemoveAt(list.Count - 1);

//                     instance.layerConfig.layers = list.ToArray();
//                 }
   
//                 for (int i = 0; i < instance.layerConfig.layers.Length; i++)
//                 {
//                     string layerName = string.Format("Layer {0}", i + 1);
//                     if (EditorHelper.DrawHeader(layerName))
//                     {
//                         XTerrainTextureLayerConfig.TextrueLayer layer = instance.layerConfig.layers[i];
//                         layer.texture = (Texture2D)EditorGUILayout.ObjectField("Texture", layer.texture, typeof(Texture2D));
//                         layer.tint = EditorGUILayout.ColorField("Color", layer.tint);
//                         layer.tintStrength = EditorGUILayout.Slider("tintStrength", layer.tintStrength, 0.0f, 1.0f);
//                         layer.startHeight = EditorGUILayout.Slider("startHeight", layer.startHeight, 0.0f, 1.0f);
//                         layer.blendStrength = EditorGUILayout.Slider("blendStrength", layer.blendStrength, 0.0f, 1.0f);
//                         layer.textureScale = EditorGUILayout.FloatField("textureScale", layer.textureScale);
//                     }
//                 }
                 
//             }

//             EditorHelper.EndContents();
//         }
//    }
//}

