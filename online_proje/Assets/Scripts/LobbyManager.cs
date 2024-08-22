using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using Unity.VisualScripting;
using UnityEngine;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public PhotonView PV;
    public static LobbyManager instance;

    public static LobbyManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<LobbyManager>();
            }
            return instance;
        }
    }

    private bool acceptanceSituations = true;
    int acceptdecline_counter = 0;

    public void SendAcceptance(int acceptdecline_index)
    {
        PV.RPC("RPC_SendAcceptance", RpcTarget.MasterClient, acceptdecline_index);
    }
    [PunRPC]
    void RPC_SendAcceptance(int acceptdecline_index)
    {
        acceptdecline_counter++;
        if(acceptdecline_index == 1) //if declined
        {
            acceptanceSituations = false;
        }

        if(acceptdecline_index == 2 && acceptanceSituations == false)
        {
            //close room
        }
        else if(acceptdecline_index == 2 && acceptanceSituations == true)
        {
            //initiate game screen for everyone
        }
    }
    void Start()
    {
        Debug.Log("Connecting to master.");

        
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Joined to master.");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined to lobby.");
    }

    public void Matchmaking()
    {
        PhotonNetwork.JoinRandomOrCreateRoom();
    }

    public void StopMatchmaking()
    {
        PhotonNetwork.LeaveRoom();
    }
    

    public override void OnJoinedRoom()
    {
        
    }


}
