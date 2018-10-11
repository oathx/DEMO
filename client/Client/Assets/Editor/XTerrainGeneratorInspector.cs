﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(XTerrainGenerator))]
class XTerrainGeneratorInspector : Editor
{
    private XTerrainGenerator                   instance;
    private XTerrainChunkSetting                setting;
    private UnityEngine.SceneManagement.Scene   scene;

    void OnEnable()
    {
        scene = UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene();
        if (!Application.isPlaying)
        {
            UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(scene);
        }

        instance = target as XTerrainGenerator;

        if (!instance.Setting)
        {
            instance.Setting = ScriptableObject.CreateInstance<XTerrainChunkSetting>();
        }
    }

    void OnDisable()
    {
		setting = instance.Setting;
    }

    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Reset"))
        {
            
        }

        if (EditorHelper.DrawHeader("Terrain"))
        {
            EditorHelper.BeginContents();

            instance.Setting.HeightmapResolution    = EditorGUILayout.IntField("HeightmapResolution", instance.Setting.HeightmapResolution);
            instance.Setting.AlphamapResolution     = EditorGUILayout.IntField("AlphamapResolution", instance.Setting.AlphamapResolution);
            instance.Setting.Length                 = EditorGUILayout.IntField("Length", instance.Setting.Length);
            instance.Setting.Height                 = EditorGUILayout.IntField("Height", instance.Setting.Height);
            

            EditorHelper.EndContents();
        }

        if (EditorHelper.DrawHeader("Noise"))
        {
            EditorHelper.BeginContents();

            instance.Setting.Left   = EditorGUILayout.IntField("Left", instance.Setting.Left);
            instance.Setting.Right  = EditorGUILayout.IntField("Right", instance.Setting.Right);
            instance.Setting.Top    = EditorGUILayout.IntField("Top", instance.Setting.Top);
            instance.Setting.Bottom = EditorGUILayout.IntField("Bottom", instance.Setting.Bottom);

            EditorHelper.EndContents();
        }

        if (EditorHelper.DrawHeader("Chunk"))
        {
            EditorHelper.BeginContents();
            instance.Setting.TerrainMaterial    = (Material)EditorGUILayout.ObjectField("TerrainMaterial", 
                instance.Setting.TerrainMaterial, typeof(Material));
            instance.Setting.Raidus             = EditorGUILayout.IntField("Raidus", instance.Setting.Raidus);
            instance.Center                     = (Transform)EditorGUILayout.ObjectField("Center",
               instance.Center, typeof(Transform));
            EditorHelper.EndContents();
        }

        if (EditorHelper.DrawHeader("Textures"))
        {
            EditorHelper.BeginContents();

            if (GUILayout.Button ("Add")) {
                instance.Setting.Textures.Add(new Texture2D(512, 512));
            }

            for (int i = 0; i < instance.Setting.Textures.Count; i++)
            {
                string name = string.Format("Layer [{0}] {1}x{1}", i, instance.Setting.Textures[i].width, instance.Setting.Textures[i].height);
                instance.Setting.Textures[i] = (Texture2D)EditorGUILayout.ObjectField(name,
                    instance.Setting.Textures[i], typeof(Texture2D));
            }

            if (GUILayout.Button("Remove"))
            {
                instance.Setting.Textures.RemoveAt(instance.Setting.Textures.Count - 1);
            }
    
            EditorHelper.EndContents();
        }
    }
}

