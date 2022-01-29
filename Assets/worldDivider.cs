using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cullGeometry : MonoBehaviour
{

    [SerializeField]
    private Transform m_WorldDivider;
    [SerializeField]
    private List<Material> m_Material;
    [SerializeField]
    private Vector3 m_WorldDividerNormal;
    [SerializeField]
    private Vector3 m_WorldDividerPosition;


    // Start is called before the first frame update
    void Start()
    {
        if(m_WorldDivider == null)
        {
            GameObject gameObject = GameObject.FindGameObjectWithTag("divider");
            m_WorldDivider = gameObject.GetComponent<Transform>();
        }

        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        List<Material> materials = new List<Material>();
        if(meshRenderer != null)
        {
            meshRenderer.GetMaterials(materials);
            foreach(var mat in materials)
            {
                m_Material.Add(mat);
            }
        }

        m_WorldDividerNormal = transform.worldToLocalMatrix.MultiplyVector(m_WorldDivider.transform.up);
        m_WorldDividerPosition = m_WorldDivider.transform.position;

        
    }

    private void Update()
    {
        foreach (var mat in m_Material)
        {
            mat.SetVector("sliceNormal", m_WorldDividerNormal);
            mat.SetVector("sliceCentre", m_WorldDividerPosition);
        }
    }

}
