using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Unity.VisualScripting;

public class ChatManager : MonoBehaviour
{
    public TMPro.TMP_InputField InputField;
    public GameObject Message;
    public GameObject Content;
    private PhotonView PV;
    private bool cursorLocked = false;

    void Start()
    {
        PV = GetComponent<PhotonView>();
    }

    void Update()
    {
        
        // if(cursorLocked && Input.GetKeyUp(KeyCode.Return))
        // {
        //     Cursor.lockState = CursorLockMode.Locked;
        //     Cursor.visible = false;
        //     cursorLocked = false;
        // }
        // else if(!cursorLocked && Input.GetKeyUp(KeyCode.Return))
        // {
        //     Cursor.lockState = CursorLockMode.Confined;
        //     Cursor.visible = true;
        //     cursorLocked = true;
        // }
    }

    public void SendMessage()
    {
        PV.RPC("GetMessage", RpcTarget.All, InputField.text);
    }

    [PunRPC]
    public void GetMessage(string RecieveMessage)
    {
        Instantiate(Message, Vector3.zero, Quaternion.identity, Content.transform);
        Message.GetComponent<Message>().myMessage.text = RecieveMessage;
    }
}
