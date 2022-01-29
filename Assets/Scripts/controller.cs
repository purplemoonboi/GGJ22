using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller : MonoBehaviour
{
    //references to the components of this entity.

    [SerializeField]
    [Range(1, 10)]
    private float       m_Acceleration;
    [SerializeField]
    private float       m_JumpAmount;
    [SerializeField]
    [Range(10, 1000)]
    private float       m_JumpRate;
    [SerializeField]
    [Range(100, 10000)]
    private float       m_JumpMax;
    [SerializeField]
    private Camera      m_Camera;
    [SerializeField]
    private Rigidbody   m_RigidBody;
    [SerializeField]
    private bool        m_IsGrounded;
    [SerializeField]
    [Range(2.0f, 100.0f)]
    private float       m_LookSpeed;
    [SerializeField]
    private bool        m_InvertXAxis;
    [SerializeField]
    private float       m_RotationY;
    [SerializeField]
    [Range(0, 360)]
    private float       m_LookLimitsX;
    [SerializeField]
    private bool        m_SelectMode;


    //Const variables.
    const string m_GroundTag = "floor";

    // Start is called before the first frame update
    void Start()
    {
        //Fetch the camera component associated with this entity.
        m_Camera = GetComponentInChildren<Camera>();

        //Grab the rigidbody.
        m_RigidBody = GetComponent<Rigidbody>();

        //Set misc attributes
        m_JumpRate = 1205.0f;
        m_JumpMax = 200.0f;
        m_IsGrounded = false;
        m_LookSpeed = 2.0f;
        m_LookLimitsX = 45.0f;

        //Hide mouse cursor.
        Cursor.lockState      = CursorLockMode.Locked;
        Cursor.visible        = false;
        m_SelectMode = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            m_SelectMode = (m_SelectMode == true) ? false : true;
        }

        if (m_SelectMode)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible   = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible   = false;
            CharacterMovement();
            UpdateCamera();
        }
    }



    // @brief Code for moving the main character.
    private void CharacterMovement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            MoveForward();
        }
        if (Input.GetKey(KeyCode.S))
        {
            MoveBackward();
        }
        if (Input.GetKey(KeyCode.A))
        {
            MoveLeft();
        }
        if (Input.GetKey(KeyCode.D))
        {
            MoveRight();
        }

        if (Input.GetKey(KeyCode.Space) && m_IsGrounded)
        {
            m_JumpAmount += m_JumpRate * Time.deltaTime;
            m_JumpAmount = Mathf.Clamp(m_JumpAmount, 1.0f, m_JumpMax);
        }
        if (Input.GetKeyUp(KeyCode.Space) && m_IsGrounded)
        {
            Jump();
        }
    }

    // @brief Very simple camera controller.
    private void UpdateCamera()
    {
        if(!m_Camera)
        {
            Debug.Log("Invalid reference to camera!");
        }
        else
        {
            m_RotationY += ((m_InvertXAxis) ? Input.GetAxis("Mouse Y") : -Input.GetAxis("Mouse Y"))  * m_LookSpeed;
            m_RotationY = Mathf.Clamp(m_RotationY, -m_LookLimitsX, m_LookLimitsX);

            //*around the x axis*
            m_Camera.transform.localRotation = Quaternion.Euler(m_RotationY, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * m_LookSpeed, 0);


        }
    }

    //Collision callbacks
    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == m_GroundTag)
        {
            m_IsGrounded = true;
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == m_GroundTag)
        {
            m_IsGrounded = false;
            m_JumpAmount = 0.0f;
        }
    }

    //Kinematics
    private void MoveForward()
    {
        Vector3 position = transform.position;
        position += transform.forward * m_Acceleration * Time.deltaTime;
        transform.position = position;
    }

    private void MoveBackward()
    {
        Vector3 position = transform.position;
        position -= transform.forward * m_Acceleration * Time.deltaTime;
        transform.position = position;
    }

    private void MoveRight()
    {
        Vector3 position = transform.position;
        position += transform.right * m_Acceleration * Time.deltaTime;
        transform.position = position;
    }

    private void MoveLeft()
    {
        Vector3 position = transform.position;
        position -= transform.right * m_Acceleration * Time.deltaTime;
        transform.position = position;
    }

    private void Jump()
    {
        if(!m_RigidBody)
        {
            Debug.LogError("Invalid rigidbody reference!");
        }
        else
        {
            Vector3 force = transform.up * m_JumpAmount;
            m_RigidBody.AddForce(force);
        }
    }

}
