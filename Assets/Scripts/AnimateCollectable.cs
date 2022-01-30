using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateCollectable : MonoBehaviour
{
    [SerializeField]
    private Material m_Material;
    [SerializeField]
    private Transform m_PickupLocation;

    private bool m_CanInteract;

    // Start is called before the first frame update
    void Start()
    {
        m_CanInteract = false;
        List<Material> materials = new List<Material>();
        GetComponent<MeshRenderer>().GetMaterials(materials);
        m_Material = materials[0];
        m_PickupLocation = GameObject.FindGameObjectWithTag("pickLoc").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Teleport()
    {
        transform.position = m_PickupLocation.position;
        m_CanInteract = true;
    }

    public bool CanInteract()
    {
        return m_CanInteract;
    }
}
