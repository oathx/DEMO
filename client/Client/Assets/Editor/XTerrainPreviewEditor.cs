using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(XTerrainPreview))]
public class XTerrainPreviewEditor : Editor
{
    public override void OnInspectorGUI()
    {
        XTerrainPreview preview = (XTerrainPreview)target;

        if (DrawDefaultInspector())
        {
            if (preview.autoUpdate)
            {
                preview.DrawMapInEditor();
            }
        }

        if (GUILayout.Button("Generate"))
        {
            preview.DrawMapInEditor();
        }
    }
}
