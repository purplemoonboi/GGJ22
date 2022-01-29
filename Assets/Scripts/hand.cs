using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hand : MonoBehaviour
{

    [SerializeField]
    private Vector3 m_InitialPosition;

    public void Start()
    {
        m_InitialPosition = GetComponent<Transform>().localPosition;
    }

    public void SetHandPosition(Vector3 pos)
    {
        transform.localPosition = pos;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public Vector3 GetInitialHandPosition()
    {
        return m_InitialPosition;
    }
}
