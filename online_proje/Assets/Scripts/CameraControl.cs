using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Photon.Pun;

public class CameraControl : MonoBehaviour
{
    private float x;
    private float y;
    public float sensitivity = -1f;
    private Vector3 rotate;
    [SerializeField] private Transform orientation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        y = Input.GetAxis("Mouse X");
        x = Input.GetAxis("Mouse Y");
        rotate = new Vector3(x, y * sensitivity, 0);
        orientation.eulerAngles = orientation.eulerAngles - rotate;
    }
}
