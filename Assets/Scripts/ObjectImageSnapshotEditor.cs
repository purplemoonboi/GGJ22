using UnityEngine;
using UnityEditor;
using System.Collections;

//Author: Dave Carlile @ crappycoding.com
//Code Source: https://crappycoding.com/2014/12/create-gameobject-image-using-render-textures/

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
