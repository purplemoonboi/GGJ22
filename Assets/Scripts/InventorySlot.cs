using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public bool occupied;
    public GameObject slotObject;
    public GameObject slotImage;
    public Camera objectImageCamera;
    ObjectImageSnapshot snapshot;
    Texture2D texture;

    void Start()
    {
        snapshot = objectImageCamera.GetComponent<ObjectImageSnapshot>();
    }

    void OnGUI()
    {
        if (texture != null)
        {
            slotImage.SetActive(true);
            slotImage.GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2());
        }
    }

    public void CreateSnapshot()
    {
        if (slotObject != null)
        {
            texture = snapshot.TakeObjectSnapshot(slotObject);
        }
    }

    public void EraseSnapshot()
    {
        texture = null;
        slotImage.SetActive(false);
    }
}
