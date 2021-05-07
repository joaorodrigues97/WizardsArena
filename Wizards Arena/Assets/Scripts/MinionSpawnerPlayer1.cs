using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MinionSpawnerPlayer1 : MonoBehaviourPun
{
    [SerializeField] private GameObject minionPrefab = null;
    public Transform[] spawnPoints = null;

    IEnumerator SpawnDelay()
    {
        
        yield return new WaitForSeconds(5);
        PhotonNetwork.Instantiate(minionPrefab.name, spawnPoints[0].position, spawnPoints[0].rotation);
        PhotonNetwork.Instantiate(minionPrefab.name, spawnPoints[1].position, spawnPoints[1].rotation);
        for (int i = 0; i < 1; i++)
        {
            yield return new WaitForSeconds(3);
            PhotonNetwork.Instantiate(minionPrefab.name, spawnPoints[0].position, spawnPoints[0].rotation);
            PhotonNetwork.Instantiate(minionPrefab.name, spawnPoints[1].position, spawnPoints[1].rotation);
        }
       
        
    }

    IEnumerator Start()
    {


        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            yield return StartCoroutine("SpawnDelay");
        }
        

    }




}
