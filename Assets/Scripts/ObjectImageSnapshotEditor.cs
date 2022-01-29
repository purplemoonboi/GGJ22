using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(ObjectImageSnapshot))]
public class ObjectImageEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ObjectImageSnapshot t = (ObjectImageSnapshot)target;
        t.objectImageLayer = EditorGUILayout.LayerField("Object Image Layer", t.objectImageLayer);

        if (GUI.changed)
            EditorUtility.SetDirty(target);

        DrawDefaultInspector();
    }
}
