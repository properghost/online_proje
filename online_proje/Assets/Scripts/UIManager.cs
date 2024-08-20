using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class UIManager : MonoBehaviourPunCallbacks
{

    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private GameObject profilePanel;
    [SerializeField] private GameObject readyPanel;
    [SerializeField] private GameObject roomPanel;
    [SerializeField] private GameObject cancelMatchmakingButton;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TMP_Text _nickname;
    [SerializeField] private GameObject joinedLobbyText;
    [SerializeField] private GameObject readyToPlayText;
    void Start()
    {
        if(PlayerPrefs.GetString("nickname") == "")
        {
            PlayerPrefs.SetString("nickname", "Profile");
        }
        _nickname.text = PlayerPrefs.GetString("nickname");
        readyPanel.SetActive(false);
        cancelMatchmakingButton.SetActive(false);
        loadingPanel.SetActive(true);
        joinedLobbyText.SetActive(false);
        readyToPlayText.SetActive(false);
    }
    void Update()
    {
        if(PhotonNetwork.CurrentRoom == null)
        {
            joinedLobbyText.SetActive(false);
            readyToPlayText.SetActive(false);
        }
        else if(PhotonNetwork.CurrentRoom != null)
        {
            if(PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                cancelMatchmakingButton.SetActive(false);
                joinedLobbyText.SetActive(false);
                readyToPlayText.SetActive(true);
                readyPanel.SetActive(true);
            }
            else
            {
                readyPanel.SetActive(false);
                cancelMatchmakingButton.SetActive(true);
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
        if(cancelMatchmakingButton.activeSelf == false)
        {
            cancelMatchmakingButton.SetActive(true);
        }
        LobbyManager.Instance.Matchmaking();
    }
    public void QuitGame()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LeaveLobby();
        Application.Quit();
    }

    public void ButtonForStopMatchmaking()
    {
        cancelMatchmakingButton.SetActive(false);
        LobbyManager.Instance.StopMatchmaking();
    }

    public void AcceptMatchButton()
    {
        roomPanel.SetActive(true);
    }

    public void OpenProfilePanel()
    {
        if(profilePanel.activeSelf == false)
        {
            profilePanel.SetActive(true);
        }
        else if(profilePanel.activeSelf == true)
        {
            profilePanel.SetActive(false);
        }
    }

    public void SaveNickname()
    {
        string nickname = inputField.text;
        PlayerPrefs.SetString("nickname", nickname);
        _nickname.text = PlayerPrefs.GetString("nickname");
    }

    
}
