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

    //Master
    [PunRPC]
    void RPC_SendAcceptance(int acceptdecline_index)
    {
        acceptdecline_counter++;
        if(acceptdecline_index == 1) //if declined
        {
            acceptanceSituations = false;
        }

        if(acceptanceSituations == false)
        {
            PV.RPC("RPC_LeaveRoomForEveryone", RpcTarget.AllViaServer);
        }
        else if(acceptdecline_counter == 2 && acceptanceSituations == true)
        {
            PV.RPC("RPC_LoadLevelForEveryone", RpcTarget.AllViaServer);
        }
    }

    [PunRPC]
    void RPC_LeaveRoomForEveryone()
    {
        PhotonNetwork.LeaveRoom();
    }

    [PunRPC]
    void RPC_LoadLevelForEveryone()
    {
        PhotonNetwork.LoadLevel(1);
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
