using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.EventSystems;
using Photon.Pun.UtilityScripts;
using UnityEngine.UI;

public class UIManager : MonoBehaviourPunCallbacks
{

    private static UIManager instance;

    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
            }
            return instance;
        }
    }

    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private GameObject profilePanel;
    [SerializeField] private GameObject readyPanel;
    [SerializeField] private GameObject roomPanel;
    [SerializeField] private GameObject cancelMatchmakingButton;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TMP_Text _nickname;
    [SerializeField] private GameObject joinedLobbyText;
    [SerializeField] private GameObject readyToPlayText;
    [SerializeField] private GameObject CurrentPlayers;

    public Sprite[] sprites;
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
            readyPanel.SetActive(false);
            cancelMatchmakingButton.SetActive(false);
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

    public void AcceptDeclineMethod()
    {
        int acceptdecline_index = EventSystem.current.currentSelectedGameObject.transform.GetSiblingIndex();
        LobbyManager.Instance.SendAcceptance(acceptdecline_index);
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

    public void OpenRoomPanel()
    {
        readyPanel.SetActive(false);
        profilePanel.SetActive(false);
        roomPanel.SetActive(true);
    }

    public void SaveNickname()
    {
        string nickname = inputField.text;
        PlayerPrefs.SetString("nickname", nickname);
        _nickname.text = PlayerPrefs.GetString("nickname");
        profilePanel.SetActive(false);
    }

    public void ShowCurrentPlayersTabVisible()
    {
        CurrentPlayers.SetActive(true);
    }

    public void SetActivePlayerSlots(int player_count)
    {
        for (int current_player_index = 0; current_player_index < player_count; current_player_index++)
        {
            CurrentPlayers.transform.GetChild(current_player_index).gameObject.SetActive(true);
            string current_player_nickname = (string)PhotonNetwork.CurrentRoom.Players[current_player_index + 1].CustomProperties["nickname"];
            int current_player_image_index = (int)PhotonNetwork.CurrentRoom.Players[current_player_index + 1].CustomProperties["image_index"];
            CurrentPlayers.transform.GetChild(current_player_index).GetChild(0).GetComponent<Image>().sprite = sprites[current_player_image_index];
            CurrentPlayers.transform.GetChild(current_player_index).GetChild(1).GetComponent<TextMeshProUGUI>().text = current_player_nickname;

        }

    }


}
