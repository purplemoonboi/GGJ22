using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateCollectable : MonoBehaviour
{

    [SerializeField]
    private bool m_ShouldAnimate;
    [SerializeField]
    private Vector3 m_TargetPosition;
    [SerializeField]
    Vector3 currentPosition;
    private Material m_Material;


    // Start is called before the first frame update
    void Start()
    {
        m_ShouldAnimate = false;

        List<Material> materials = new List<Material>();
        GetComponent<MeshRenderer>().GetMaterials(materials);

        m_Material = materials[0];
    }

    // Update is called once per frame
    void Update()
    {
        if(m_ShouldAnimate)
        {
            Animate();
        }
    }

    private void Animate()
    {
        //Debug if we have h
        m_Material.SetColor("_Color", Color.cyan);

        //Move towards target.
        Vector3 target = m_TargetPosition;

        //The distance to travel.
        float tick = 2.0f * Time.deltaTime;

       currentPosition = transform.position;

        currentPosition = Vector3.MoveTowards(currentPosition, target, tick);

        //Check if the distance between current and target positions is below threshold.
        if (Vector3.Distance(currentPosition, target) < 0.001f)
        {
            //Destroy clone.
            m_ShouldAnimate = false;

        }

        transform.position = currentPosition;
    }

    public bool IsAnimating()
    {
        return m_ShouldAnimate;
    }

    public void SetShouldAnimate(bool value)
    {
        m_ShouldAnimate = value;
    }

    public void SetTargetPosition(Vector3 target)
    {
        m_TargetPosition = target;
    }
}
