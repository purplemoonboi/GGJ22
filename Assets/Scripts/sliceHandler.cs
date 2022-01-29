using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// @brief This script is to be added to any collectables in the scene.
public class sliceHandler : MonoBehaviour
{

    [SerializeField]
    GameObject m_MagnifyingGlass;
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
        Vector3 normal = transform.worldToLocalMatrix.MultiplyVector(-m_MagnifyingGlass.transform.up);
        Vector3 centre = m_MagnifyingGlass.transform.position;

        //Upload the data to the shader. 
        m_Material.SetVector("sliceNormal", normal);
        m_Material.SetVector("sliceCentre", centre);
    }

}
