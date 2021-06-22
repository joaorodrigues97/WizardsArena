using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class TurretHealth : MonoBehaviourPunCallbacks, IPunObservable
{

    private int health = 1000;
    public Slider healthTurret;
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


    public void TakeDamage(int damage)
    {
        health -= damage;
        SetHealth(health);
    }

    public void SetHealth(int newHealth)
    {
        //health = newHealth;
        healthTurret.value = newHealth;
    }

    public void setHealthTurretDeath(int newHealth)
    {
        health = newHealth;
    }

    public void SetMaxHealth(int health)
    {
        healthTurret.maxValue = health;
        healthTurret.value = health;
    }

    public void TurretDie()
    {
        PhotonNetwork.Destroy(transform.parent.gameObject);
    }

    public int getTurretHealth()
    {
        return health;
    }
}
