using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using Unity.VisualScripting;
using UnityEngine;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    
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
    void Start()
    {
        Debug.Log("Connecting to master.");

        
        PhotonNetwork.ConnectUsingSettings();
    }
    void Update()
    {
        
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
