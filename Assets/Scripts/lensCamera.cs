using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Transform))]
public class lensCamera : MonoBehaviour
{
    [SerializeField]
    private Transform m_OtherTransform;

    [SerializeField]
    private Transform m_OtherCamTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = m_OtherTransform.position + new Vector3(-60, 0, 0);
        transform.rotation = m_OtherCamTransform.rotation;
    }
}
