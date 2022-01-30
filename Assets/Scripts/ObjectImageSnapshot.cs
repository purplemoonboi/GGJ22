using UnityEditor;
using UnityEngine;
using System;
using System.Collections;

//Author: Dave Carlile @ crappycoding.com
//Code Source: https://crappycoding.com/2014/12/create-gameobject-image-using-render-textures/
public class ObjectImageSnapshot : MonoBehaviour
{
    public Camera objectImageCamera;
    [HideInInspector]
    public int objectImageLayer;

    public int snapshotTextureWidth = 128;
    public int snapshotTextureHeight = 128;
    public Vector3 defaultPosition = new Vector3(0, 0, 1);
    public Vector3 defaultRotation = new Vector3(345.8529f, 313.8297f, 14.28433f);
    public Vector3 defaultScale = new Vector3(1, 1, 1);

    void Start()
    {
    }

    void SetLayerRecursively(GameObject o, int layer)
    {
        foreach (Transform t in o.GetComponentsInChildren<Transform>(true))
            t.gameObject.layer = layer;
    }


    public Texture2D TakeObjectSnapshot(GameObject prefab)
    {
        return TakeObjectSnapshot(prefab, defaultPosition, Quaternion.Euler(defaultRotation), defaultScale);
    }


    public Texture2D TakeObjectSnapshot(GameObject prefab, Vector3 position)
    {
        return TakeObjectSnapshot(prefab, position, Quaternion.Euler(defaultRotation), defaultScale);
    }


    public Texture2D TakeObjectSnapshot(GameObject prefab, Vector3 position, Quaternion rotation, Vector3 scale)
    {
        // validate properties
        if (objectImageCamera == null)
            throw new InvalidOperationException("Object Image Camera must be set");

        if (objectImageLayer < 0 || objectImageLayer > 31)
            throw new InvalidOperationException("Object Image Layer must specify a valid layer between 0 and 31");


        // clone the specified game object so we can change its properties at will, and
        // position the object accordingly
        GameObject gameObject = GameObject.Instantiate(prefab, position, rotation) as GameObject;
        gameObject.transform.localScale = scale;

        // set the layer so the render to texture camera will see the object 
        SetLayerRecursively(gameObject, objectImageLayer);


        // get a temporary render texture and render the camera
        objectImageCamera.targetTexture = RenderTexture.GetTemporary(snapshotTextureWidth, snapshotTextureHeight, 24);
        objectImageCamera.Render();

        // activate the render texture and extract the image into a new texture
        RenderTexture saveActive = RenderTexture.active;
        RenderTexture.active = objectImageCamera.targetTexture;
        Texture2D texture = new Texture2D(objectImageCamera.targetTexture.width, objectImageCamera.targetTexture.height);
        texture.ReadPixels(new Rect(0, 0, objectImageCamera.targetTexture.width, objectImageCamera.targetTexture.height), 0, 0);
        texture.Apply();
        RenderTexture.active = saveActive;

        // clean up after ourselves
        RenderTexture.ReleaseTemporary(objectImageCamera.targetTexture);
        GameObject.DestroyImmediate(gameObject);

        return texture;
    }
}
