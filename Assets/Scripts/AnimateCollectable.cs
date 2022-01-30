using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateCollectable : MonoBehaviour
{
    [SerializeField]
    private Material m_Material;
    [SerializeField]
    private Transform m_PlayerTransform;

    // Start is called before the first frame update
    void Start()
    {

        List<Material> materials = new List<Material>();
        GetComponent<MeshRenderer>().GetMaterials(materials);
        m_PlayerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        m_Material = materials[0];
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void Teleport()
    {
        Vector3 position = new Vector3();
        position = m_PlayerTransform.position + m_PlayerTransform.forward * 5.4f;
        position.y += transform.localScale.x;
        transform.position = position;
    }

}
