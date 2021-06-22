using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class RoomBtn : MonoBehaviour
{
    public Text roomText;

    public string roomName;

    public void setRoom()
    {
        roomText.text = roomName;
    }

    public void joinRoomOnClick()
    {
        GetComponent<AudioSource>().Play();
        PhotonNetwork.JoinRoom(roomName);
    }
}
