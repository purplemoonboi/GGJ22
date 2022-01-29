using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setRenderTexture : MonoBehaviour
{

    [SerializeField]
    private MeshRenderer m_Renderer;
    [SerializeField]
    private DynamicRTT m_DRTT; 

    // Start is called before the first frame update
    void Start()
    {
        m_Renderer = GetComponent<MeshRenderer>();

        //Get a reference to the dynamic rtt script.
        m_DRTT = GameObject.FindGameObjectWithTag("capture").GetComponent<DynamicRTT>();
        List<Material> material = new List<Material>();
        m_Renderer.GetMaterials(material);
        //Update the render texture.
        material[0].SetTexture("_MainTex", m_DRTT.GetRenderTexture());
    }

    // Update is called once per frame
    void Update()
    {
        if(m_DRTT.HasViewportSizeChanged())
        {
            List<Material> material = new List<Material>();
            m_Renderer.GetMaterials(material);

            //Update the render texture.
            material[0].SetTexture("_MainTex", m_DRTT.GetRenderTexture());

            //We've updated the render texture.
            m_DRTT.SetViewportChanged(false);
        }
    }
}
