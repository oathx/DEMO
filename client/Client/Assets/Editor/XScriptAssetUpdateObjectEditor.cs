using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(XScriptAssetUpdateObject), true)]
public class XScriptAssetUpdateObjectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        XScriptAssetUpdateObject data = (XScriptAssetUpdateObject)target;
        if (GUILayout.Button("Update"))
        {
            data.NotifyOfUpdatedValues();
            EditorUtility.SetDirty(target);
        }
    }
}
