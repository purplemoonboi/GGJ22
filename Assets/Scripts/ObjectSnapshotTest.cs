using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ObjectSnapshotTest : MonoBehaviour
{
    public Transform objectImageCamera;
    public GameObject objectToSnapshot;
    public GameObject inventorySlotImage;

    Image icon;
    ObjectImageSnapshot snapshot;
    Texture2D texture;

    // Use this for initialization
    void Start()
    {
        snapshot = objectImageCamera.GetComponent<ObjectImageSnapshot>();
        icon = inventorySlotImage.GetComponent<Image>();
    }


    void OnGUI()
    {
        GUI.TextField(new Rect(10, 5, 275, 21), "Press \"Spacebar\" to take object snapshot");

        if (texture != null)
        {
            //GUI.Box(new Rect(10, 32, texture.width, texture.height), texture);
            inventorySlotImage.SetActive(true);
            icon.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2());
        }
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            texture = snapshot.TakeObjectSnapshot(objectToSnapshot);
        }

    }
}
