using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Transform))]
public class lensCamera : MonoBehaviour
{
    [SerializeField]
    private Transform m_OtherCameraTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float offsetX = m_OtherCameraTransform.position.x;
        float offsetY = m_OtherCameraTransform.position.y;
        float offsetZ = m_OtherCameraTransform.position.z;
        transform.position = m_OtherCameraTransform.position + new Vector3(-40, 0, 0);
        transform.rotation = m_OtherCameraTransform.rotation;
    }
}
