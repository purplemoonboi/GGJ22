using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lensScript : MonoBehaviour
{

    [SerializeField]
    private hand m_Hand;
    [SerializeField]
    private Vector3 m_HandStartPosition;
    [SerializeField]
    private Quaternion m_HandStartRotation;
    [SerializeField]
    private float m_HandSpeed;
    [SerializeField]
    private bool m_LerpHand;
    [SerializeField]
    private Vector3 m_TargetPosition;
    [SerializeField]
    private bool m_ActiveCamera = false;
    [SerializeField]
    private bool m_FireRay;

    [SerializeField]
    private Transform m_OtherCamera;

    // Bit shift the index of the layer (8) to get a bit mask
    private int layerMask = 1 << 8;

    // Start is called before the first frame update
    void Start()
    {

        //Get access to the hand script in the children components.
        m_Hand = GetComponentInChildren<hand>();

        //Check if this script has a hand object.
        m_ActiveCamera = true;

        if(m_Hand == null)
        {
            m_ActiveCamera = false;
        }

        //Lerp the hand of the character.
        m_LerpHand = false;
        m_FireRay = false;
        m_HandSpeed = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_ActiveCamera)
        {
            //Check if the user has moved the hand.
            AnimateHand();
        }

        if (Input.GetMouseButton(0))
        {
            m_FireRay = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            m_FireRay = false;
        }
    }

    public void FixedUpdate()
    {
        if(m_FireRay)
        {
            hasfired = true;
            SimpleRayCast();
        }
        else
        {
            hasfired = false;
        }
    }

    bool hasfired = false;

    private void OnDrawGizmos()
    {
        if (hasfired)
        {
            Gizmos.DrawLine(m_OtherCamera.position, m_OtherCamera.position + m_OtherCamera.forward);
        }
    }

    void SimpleRayCast()
    {

        RaycastHit hit;
        Ray ray = new Ray(m_OtherCamera.position, m_OtherCamera.forward);
        if (Physics.Raycast(ray, out hit, 100.0f, layerMask))
        {
            Debug.Log("Wee hit " + hit.transform.tag);

            GameObject other = hit.transform.gameObject;
            if (hit.transform.tag == "collectable")
            {

                //Start animating collectable.
                var script = other.GetComponent<AnimateCollectable>();
                script.Teleport();
            }
        }
    }


    void AnimateHand()
    {
        if (Input.GetMouseButton(1))
        {
            m_LerpHand = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            m_LerpHand = false;
        }

        //Get the hand position.
        Transform handTransform = m_Hand.GetTransform();
        Vector3 handCurrentLocalPosition = handTransform.localPosition;

        //Transform the world up & right vectors into local space.
        Vector3 localUp = transform.worldToLocalMatrix.MultiplyVector(transform.up);
        Vector3 localRight = transform.worldToLocalMatrix.MultiplyVector(transform.right);

        //Set target based on whether we're holding down the mouse.
        Vector3 target = (m_LerpHand) ?
           m_Hand.GetInitialHandPosition() + ((localUp + localRight) * m_HandSpeed * 0.4f)
           :
           m_Hand.GetInitialHandPosition();

        //The distance to travel.
        float tick = m_HandSpeed * Time.deltaTime;

        handCurrentLocalPosition = Vector3.MoveTowards(handCurrentLocalPosition, target, tick);

        //Check if the distance between current and target positions is below threshold.
        if (Vector3.Distance(handCurrentLocalPosition, target) < 0.001f)
        {
            handCurrentLocalPosition = target;
        }

        m_Hand.SetHandPosition(handCurrentLocalPosition);
    }

}
