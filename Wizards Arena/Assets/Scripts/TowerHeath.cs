using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class TowerHeath : MonoBehaviourPunCallbacks, IPunObservable
{

    private int health = 500;
    public Slider healthTower;
    public bool TowerDead = false;
    private PhotonView PV;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(health);
        }
        else
        {
            health = (int)stream.ReceiveNext();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        //player1Info = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerInfo>();
        //player2Info = GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            TowerDie();
        }

        

    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        SetHealth(health);
    }

    public void SetHealth(int health)
    {
        healthTower.value = health;
    }

    public void SetMaxHealth(int health)
    {
        healthTower.maxValue = health;
        healthTower.value = health;
    }

    public void TowerDie()
    {
        /*if (gameObject.CompareTag("TurretP1"))
        {

            player2Info.addCoins(100);
        }
        else if (gameObject.CompareTag("TurretP2"))
        {

            player1Info.addCoins(100);
        }*/
        PhotonNetwork.Destroy(transform.parent.gameObject);
    }
}
