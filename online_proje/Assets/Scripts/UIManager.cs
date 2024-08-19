using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class UIManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private GameObject joinedLobbyText;
    [SerializeField] private GameObject readyToPlayText;
    void Start()
    {
        loadingPanel.SetActive(true);
        joinedLobbyText.SetActive(false);
        readyToPlayText.SetActive(false);
    }
    void Update()
    {
        if(PhotonNetwork.CurrentRoom != null)
        {
            if(PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                joinedLobbyText.SetActive(false);
                readyToPlayText.SetActive(true);
            }
            else
            {
                joinedLobbyText.SetActive(true);
                readyToPlayText.SetActive(false);
            }
        }
    }

    public override void OnJoinedLobby()
    {
        loadingPanel.SetActive(false);     
    }

    public void ButtonForMatchmaking()
    {
        LobbyManager.Instance.Matchmaking();
    }
    public void QuitGame()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LeaveLobby();
        Application.Quit();
    }

    
}
