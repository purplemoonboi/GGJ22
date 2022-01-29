using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicRTT : MonoBehaviour
{

    [SerializeField]
    private Camera m_CamComponent;
    [SerializeField]
    private int m_ScreenWidth;
    [SerializeField]
    private int m_ScreenHeight;
    [SerializeField]
    private RenderTexture m_RenderTexture;
    [SerializeField]
    private bool m_ScreenChange;

    void Awake()
    {
        //Grab components and set attributes.
        m_CamComponent = GetComponent<Camera>();
        m_ScreenWidth = Screen.width;
        m_ScreenHeight = Screen.height;

        //Create render texture
        m_RenderTexture = CreateNewRenderTexture(Screen.width, Screen.height);
        m_CamComponent.targetTexture = m_RenderTexture;
        m_ScreenChange = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Update render texture based on size of viewport.
        if(m_ScreenWidth != Screen.width || m_ScreenHeight != Screen.height)
        {
            m_ScreenChange = true;
            m_ScreenWidth = Screen.width;
            m_ScreenHeight = Screen.height;
            m_RenderTexture = CreateNewRenderTexture(Screen.width, Screen.height);
            m_CamComponent.targetTexture = m_RenderTexture;
        }
    }

    public RenderTexture CreateNewRenderTexture(int width, int height)
    {
        RenderTexture rtt = new RenderTexture(width, height, 24);
        rtt.filterMode = FilterMode.Trilinear;
        rtt.wrapMode = TextureWrapMode.Clamp;
        rtt.dimension = UnityEngine.Rendering.TextureDimension.Tex2D;
        return rtt;
    }

    public RenderTexture GetRenderTexture()
    {
        return m_RenderTexture;
    }

    public bool HasViewportSizeChanged()
    {
        return m_ScreenChange;
    }

    public void SetViewportChanged(bool value)
    {
        m_ScreenChange = value;
    }

}
