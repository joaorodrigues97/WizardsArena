using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerSpawner : MonoBehaviourPun
{
    [SerializeField] private GameObject playerPrefab1 = null;
    [SerializeField] private GameObject playerPrefab2 = null;
    public Transform[] spawnPoints = null;
    HS_CameraController cam;

    private void Awake()
    {
        cam = FindObjectOfType<HS_CameraController>();

        int i = PhotonNetwork.CurrentRoom.PlayerCount - 1;
        if (i == 0)
        {
            GameObject player = PhotonNetwork.Instantiate(playerPrefab1.name, spawnPoints[i].position, spawnPoints[i].rotation);
            cam.SetCameraTarget(player.transform);
        }else if (i == 1)
        {
            GameObject player = PhotonNetwork.Instantiate(playerPrefab2.name, spawnPoints[i].position, spawnPoints[i].rotation);
            cam.SetCameraTarget(player.transform);
        }
        
    }
}