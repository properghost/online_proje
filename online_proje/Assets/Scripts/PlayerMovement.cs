using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    public float walkSpeed;
    public float maxVelocityChange;

    private Vector2 input;
    private Rigidbody rb;


    PhotonView PV;
    private bool sprint;
    private bool jump;
    private bool dash;
    private bool crouch;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
    }

    void Update()
    {
        if(PV.IsMine)
        {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        input.Normalize();
        SprintInput();

        }
    }

    void FixedUpdate()
    {
        rb.AddForce(CalculateMovement(walkSpeed), ForceMode.VelocityChange);
    }

    Vector3 CalculateMovement(float _speed)
    {
        Vector3 targetVelocity = new Vector3(input.x, 0, input.y);
        targetVelocity = transform.TransformDirection(targetVelocity);

        targetVelocity *= _speed;

        Vector3 velocity = rb.velocity;


        if(input.magnitude > 0.5f)
        {
            Vector3 velocityChange = targetVelocity - velocity;

            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);

            velocityChange.y = 0;
            return(velocityChange);
        }
        else
        {
            return new Vector3();
        }
    }   
    public void SprintInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("Sprint activated.");
            sprint = true;
            if(sprint)
            {
                walkSpeed *= 2f;
            }
            else if(sprint == false)
            {
                walkSpeed = walkSpeed - walkSpeed/2;
            }
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) && sprint)
        {
            Debug.Log("Sprint de-activated.");
            sprint = false;
        }
    }

    // void Sprint()
    // {
    //     if(sprint)
    //     {
    //         walkSpeed *= 2f;
    //     }
    //     else if(sprint == false)
    //     {
    //         walkSpeed = walkSpeed - walkSpeed/2;
    //     }
    // }


}
