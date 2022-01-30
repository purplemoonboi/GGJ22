using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// @brief This script is to be added to any collectables in the scene.
public class sliceHandler : MonoBehaviour
{

    [SerializeField]
    GameObject         m_MagnifyingGlass;
    [SerializeField]
    private Material   m_Material;
    [SerializeField]
    private GameObject m_CloneBlueprint;
    [SerializeField]
    private GameObject m_CloneGameObject;
    [SerializeField]
    private Transform  m_CloneSpawnTransform;

    private Vector3 m_MagnifyingGlassLocalUp;

    [SerializeField]
    private bool m_CloneAlive;

    // Start is called before the first frame update
    void Start()
    {
        List<Material> materials = new List<Material>();
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.GetMaterials(materials);
        m_Material = materials[0];
        m_CloneAlive = false;
    }

    // Update is called once per frame
    void Update()
    {
        m_MagnifyingGlassLocalUp = transform.worldToLocalMatrix.MultiplyVector(m_MagnifyingGlass.transform.up);
        Vector3 centre = m_MagnifyingGlass.transform.position;

        //Upload the data to the shader. 
        m_Material.SetVector("sliceNormal", m_MagnifyingGlassLocalUp);
        m_Material.SetVector("sliceCentre", centre);

        //Deal with the clone object.
        if(m_CloneGameObject != null)
        {

            //Move towards target.
            Vector3 target = m_CloneSpawnTransform.position + m_MagnifyingGlassLocalUp * 2.0f;

            //The distance to travel.
            float tick = 2.0f * Time.deltaTime;

            Vector3 currentPosition = m_CloneGameObject.transform.position;

            currentPosition = Vector3.MoveTowards(currentPosition, target, tick);

            //Check if the distance between current and target positions is below threshold.
            if (Vector3.Distance(currentPosition, target) < 0.001f)
            {}

            m_CloneGameObject.transform.position = currentPosition;

            List<Material> materials = new List<Material>();
            MeshRenderer meshRenderer = m_CloneGameObject.GetComponent<MeshRenderer>();
            meshRenderer.GetMaterials(materials);

            //Invert clone's dot product so to render the loot
            //coming out of the glass.
            materials[0].SetVector("sliceNormal", -m_MagnifyingGlassLocalUp);
            materials[0].SetVector("sliceCentre", m_CloneSpawnTransform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "lens")
        {
            m_CloneGameObject = null;
        }
        else
        {
            float scale = m_CloneBlueprint.transform.localScale.x;
            Vector3 position = m_CloneSpawnTransform.position - new Vector3(0.0f, m_MagnifyingGlassLocalUp.y * scale, 0.0f);
            Quaternion rotation = m_CloneBlueprint.transform.rotation;
            m_CloneGameObject = Instantiate(m_CloneBlueprint, position, rotation);
            m_CloneAlive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "lens")
        {
            m_CloneGameObject = null;
            m_CloneAlive = false;
        }
    }

}
