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
        Application.targetFrameRate = 100;
        cam = FindObjectOfType<HS_CameraController>();
        if (PhotonNetwork.IsMasterClient)
        {
            GameObject player = PhotonNetwork.Instantiate(playerPrefab1.name, spawnPoints[0].position, spawnPoints[0].rotation);
            cam.SetCameraTarget(player.transform);
        }
        else
        {
            GameObject player = PhotonNetwork.Instantiate(playerPrefab2.name, spawnPoints[1].position, spawnPoints[1].localRotation);
            cam.transform.Rotate(14f,180f,0f);
            cam.SetCameraTarget(player.transform);
        }
    }

    
}