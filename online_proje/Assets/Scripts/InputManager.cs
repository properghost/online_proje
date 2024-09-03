using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public bool jump;
    public bool dash;
    public bool crouch;
    public bool sprint;

    //public static InputManager instance;
    // public static InputManager Instance
    // {
    //     get
    //     {
    //         if (instance == null)
    //         {
    //             instance = FindObjectOfType<InputManager>();
    //         }
    //         return instance;
    //     }
    // }

    public void Jump()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            jump = true;
        }
    }

    public void Dash()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            dash = true;
        }
    }

    public void Crouch()
    {
        if(Input.GetKey(KeyCode.LeftControl))
        {
            crouch = true;
        }
    }
    
    public void Sprint()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Debug.Log("Sprint activated.");
            sprint = true;
        }
        else if (Input.GetKey(KeyCode.LeftShift) && sprint)
        {
            Debug.Log("Sprint de-activated.");
            sprint = false;
        }
    }
    
}
