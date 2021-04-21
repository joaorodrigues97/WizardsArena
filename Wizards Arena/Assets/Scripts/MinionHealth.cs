using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class MinionHealth : MonoBehaviourPunCallbacks, IPunObservable
{

    public int health = 100;
    public Slider healthDragon;


    private PhotonView PV;
    private PlayerInfo player1Info;
    private PlayerInfo player2Info;
    private bool hasRunned;
    

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

    public void TakeDamage(int damage)
    {
        health -= damage;
        SetHealth(health);
    }

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        player1Info = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerInfo>();
        player2Info = GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerInfo>();
        hasRunned = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0 && !hasRunned)
        {
            Debug.Log("BOOL: "+hasRunned+" GameObject: "+gameObject+"Health: "+health);
            hasRunned = true;
            MinionDie();
        }
        
    }

    public void SetHealth(int health)
    {
        healthDragon.value = health;
    }

    public void SetMaxHealth(int health)
    {
        healthDragon.maxValue = health;
        healthDragon.value = health;
    }

    public void MinionDie()
    {
        
        if (gameObject.CompareTag("DragonP1"))
        {
            player2Info.addCoins(50);
            player2Info.addExp(1000, "player2");
        }
        else if (gameObject.CompareTag("DragonP2"))
        {
            player1Info.addCoins(50);
            player1Info.addExp(1000, "player1");
        }
        DestroyImmediate(gameObject);
        PhotonNetwork.Destroy(PV);
        
        
    }

}
