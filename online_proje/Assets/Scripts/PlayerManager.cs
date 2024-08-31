using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
using System.IO;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    PhotonView PV;
    [SerializeField] private Camera camera;
    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }
    void Start()
    {
        if(!PV.IsMine)
        {
            DisableCamera();
        }
    }

    private void DisableCamera()
    {
        camera.enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
