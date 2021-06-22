using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;

public class CustomLobby : MonoBehaviourPunCallbacks, ILobbyCallbacks
{
    public static CustomLobby lobby;

    public string roomName;
    public GameObject roomListingPrefab;
    public Transform roomsPanel;


    private void Awake()
    {
        lobby = this;
    }

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Player has connected to photon master server");
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.NickName = "Player" + UnityEngine.Random.Range(0,1000);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);
        RemoveRoomListings();
        foreach (RoomInfo info in roomList)
        {
            ListRoom(info);
        }
    }

    private void ListRoom(RoomInfo info)
    {
        if(info.IsOpen && info.IsVisible)
        {
            GameObject tempListing = Instantiate(roomListingPrefab, roomsPanel);
            RoomBtn tempBtn = tempListing.GetComponent<RoomBtn>();
            Debug.Log("NAME INFO: "+info.Name);
            tempBtn.roomName = info.Name;
            tempBtn.setRoom();
        }
    }

    private void RemoveRoomListings()
    {
        for(int i = roomsPanel.childCount-1; i>= 0; i--)
        {
            Destroy(roomsPanel.GetChild(0).gameObject);
        }
    }

    public void CreateRoom()
    {
        Debug.Log("Trying to create a new room");
        RoomOptions options = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 2 };
        PhotonNetwork.CreateRoom(roomName, options);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room creation failed!!");
    }

    public void OnRoomNameChanged(string nameIn)
    {
        roomName = nameIn;
    }

    public void JoinLobbyOnClick()
    {
        if (!PhotonNetwork.InLobby)
        {
            
            PhotonNetwork.JoinLobby();
        }
    }

   
}
