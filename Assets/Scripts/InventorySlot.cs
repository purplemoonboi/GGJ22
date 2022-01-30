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
            texture = null; //Setting the texture back to null so the inventory slot icon isn't regenerated unnecessarily
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
