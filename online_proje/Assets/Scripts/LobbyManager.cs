using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using Unity.VisualScripting;
using UnityEngine;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public GameObject player;
    [Space]
    public Transform spawnPoint;
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
        PhotonNetwork.Instantiate(player.name, spawnPoint.position, Quaternion.identity);
        //UIManager.Instance.OpenRoomPanel();
    }

    void Start()
    {
        Debug.Log("Connecting to master.");

        
        PhotonNetwork.ConnectUsingSettings();

        ExitGames.Client.Photon.Hashtable local_player_settings = new ExitGames.Client.Photon.Hashtable();

        string nickname = PlayerPrefs.GetString("nickname");
        int image_index = Random.Range(0, 2);

        local_player_settings.Add("nickname", nickname);
        local_player_settings.Add("image_index", image_index);

        PhotonNetwork.LocalPlayer.SetCustomProperties(local_player_settings);
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
        ExitGames.Client.Photon.Hashtable RoomProperties = new ExitGames.Client.Photon.Hashtable();
        RoomProperties.Add("GameMode", 0);
        
        
        PhotonNetwork.JoinRandomRoom(RoomProperties, 2);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        ExitGames.Client.Photon.Hashtable RoomProperties = new ExitGames.Client.Photon.Hashtable();
        RoomProperties.Add("GameMode", 0);
        
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.CustomRoomProperties = RoomProperties;

        roomOptions.CustomRoomPropertiesForLobby = new[] { "GameMode" };
        roomOptions.MaxPlayers = 2;
        
        PhotonNetwork.CreateRoom("room_" + Random.Range(0, 99999), roomOptions);
    }

    public void StopMatchmaking()
    {
        PhotonNetwork.LeaveRoom();
    }
    

    public override void OnJoinedRoom()
    {
        UIManager.Instance.ShowCurrentPlayersTabVisible();
        UIManager.Instance.SetActivePlayerSlots(PhotonNetwork.CurrentRoom.PlayerCount);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UIManager.Instance.SetActivePlayerSlots(PhotonNetwork.CurrentRoom.PlayerCount);
    }

   

}
