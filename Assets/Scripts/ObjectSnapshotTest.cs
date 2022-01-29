using UnityEngine;
using System.Collections;

public class ObjectSnapshotTest : MonoBehaviour
{
    public Transform objectImageCamera;
    public GameObject objectToSnapshot;
    ObjectImageSnapshot snapshot;
    Texture2D texture;

	// Use this for initialization
	void Start ()
    {
        snapshot = objectImageCamera.GetComponent<ObjectImageSnapshot>();
	}


    void OnGUI()
    {
        GUI.TextField(new Rect(10, 5, 275, 21), "Press \"Spacebar\" to take object snapshot");

        if (texture != null)
        {
            GUI.Box(new Rect(10, 32, texture.width, texture.height), texture);
        }
    }
	
	void Update ()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            texture = snapshot.TakeObjectSnapshot(objectToSnapshot);
        }
	
	}
}
