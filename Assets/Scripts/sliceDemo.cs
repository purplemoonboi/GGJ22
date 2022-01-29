using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sliceDemo : MonoBehaviour
{

    [SerializeField]
    GameObject m_Plane;
    [SerializeField]
    private Material m_Material;

    // Start is called before the first frame update
    void Start()
    {
        List<Material> materials = new List<Material>();
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

        meshRenderer.GetMaterials(materials);
        m_Material = materials[0];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 planeNormal = transform.worldToLocalMatrix.MultiplyVector(-m_Plane.transform.up);
        Vector3 planePosition = m_Plane.transform.position;
        m_Material.SetVector("sliceNormal", planeNormal);
        m_Material.SetVector("sliceCentre", planePosition);
    }


    
}
