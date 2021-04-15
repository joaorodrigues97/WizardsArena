using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Realtime;

public class NetworkController : MonoBehaviourPunCallbacks
{
    public Text txtStatus = null;
    public GameObject btnStart = null;
    public byte MaxPlayers = 2;
    


    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        btnStart.SetActive(false);
        Status("Connecting to server");
    }

    private void Status(string msg)
    {
        Debug.Log(msg);
        txtStatus.text = msg;
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        PhotonNetwork.AutomaticallySyncScene = true;
        Status("Connected to "+PhotonNetwork.ServerAddress);
        btnStart.SetActive(true);
    }

    public void btnStart_Click()
    {
        string roomName = "Room1";
        Photon.Realtime.RoomOptions opts = new Photon.Realtime.RoomOptions();
        opts.IsOpen = true;
        opts.IsVisible = true;
        opts.MaxPlayers = MaxPlayers;

        PhotonNetwork.JoinOrCreateRoom(roomName,opts,Photon.Realtime.TypedLobby.Default);
        btnStart.SetActive(false);
        Status("Joining "+roomName);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        
        
        PhotonNetwork.LoadLevel("WizardsArenaGame");
            
    }







}
