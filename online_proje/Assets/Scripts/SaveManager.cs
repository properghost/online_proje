using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class SaveManager
{

    
    public static void SetPlayerNickname(string playerName)
    {
        PlayerPrefs.SetString("playerNickname", playerName);
        
    }

    public static void GetPlayerNickname()
    {
        PlayerPrefs.GetString("playerNickname");
    }

}
