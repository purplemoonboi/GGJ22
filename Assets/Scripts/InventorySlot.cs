using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    public bool occupied;
    public GameObject slotObject;
    public Camera previewCamera;
    public IEnumerator CreatePreview()
    {
        yield return new WaitForEndOfFrame();

        RenderTexture saveTexture = RenderTexture.active;
        RenderTexture.active = previewCamera.targetTexture;
        previewCamera.Render();

        Texture2D previewImage = new Texture2D(previewCamera.targetTexture.width, previewCamera.targetTexture.height);
        previewImage.ReadPixels(new Rect(0, 0, previewCamera.targetTexture.width, previewCamera.targetTexture.height), 0, 0);
        previewImage.Apply();

        Renderer renderer = slotObject.GetComponent<Renderer>();
        renderer.material.mainTexture = previewImage;

        RenderTexture.active = saveTexture;
    }
}
