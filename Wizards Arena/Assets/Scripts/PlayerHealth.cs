using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviourPunCallbacks, IPunObservable
{

    //PLAYER1 STATS
    private int playerMaxHealth;
    private int playerResistence;
    private int playerHealth;
    private int playerDamage;
    public Slider healthPlayer;
    private Slider mySliderHealth;
    private PhotonView PV;


    //PLAYER RESISTENCE = COUNT TORRES * 10
    //QUANDO SAO DESRUIDAS AS TORRES -10 RESISTENCE

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(playerHealth);
        }
        else
        {
            playerHealth = (int)stream.ReceiveNext();
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        mySliderHealth = GameObject.FindGameObjectWithTag("CanvasHealth").GetComponent<Slider>();
        playerMaxHealth = 1100;
        playerDamage = 30;
        playerResistence = 30;
        playerHealth = playerMaxHealth;
        healthPlayer.maxValue = playerMaxHealth;
        healthPlayer.value = playerMaxHealth;
        mySliderHealth.maxValue = playerMaxHealth;
        mySliderHealth.value = playerMaxHealth;
    }

    

    public int getPlayerDamage()
    {
        return playerDamage;
    }

    public void setPlayerDamage(int damage)
    {
        //if (PV.IsMine)
        //{
        playerDamage = damage;
        //}
    }

    public int getResistence()
    {
        return playerResistence;
    }

    public void subResistence(int resistence)
    {
        //if (PV.IsMine)
        //{
        playerResistence -= resistence;
        //}
    }

    public void addResistence(int resistence)
    {
        //if (PV.IsMine)
        //{
        playerResistence += resistence;
        //}
    }

    public int getMaxHealth()
    {
        return playerMaxHealth;
    }

    public void setMaxHealth(int newMaxHealth)
    {
        
        healthPlayer.maxValue = newMaxHealth;
        if (PV.IsMine)
        {
            mySliderHealth.maxValue = newMaxHealth;
        }
        
        playerMaxHealth = newMaxHealth;
        
        

    }

    public int getCurrentHealth()
    {
        return playerHealth;
    }

    /*public void setCurrentHealth(int healthToAdd)
    {
        
        playerHealth = healthToAdd;
        
        Debug.Log("SLIDER BEFORE: "+healthPlayer.value);
        healthPlayer.value = playerHealth;
        Debug.Log("SLIDER AFTER: " + healthPlayer.value);
        if (PV.IsMine)
        {
            mySliderHealth.value = playerHealth;
        }
        
    }*/



    public void addHealth(int healthToAdd)
    {
        
        if (playerHealth + healthToAdd <= playerMaxHealth)
        {
            //setCurrentHealth(playerHealth + healthToAdd);
            PV.RPC("healthadd", RpcTarget.All, playerHealth+healthToAdd);
        }
        else
        {
            //setCurrentHealth(playerMaxHealth);
            PV.RPC("healthadd", RpcTarget.All, playerMaxHealth);
        }
    }


    public void TakeDamage(int newHealth)
    {

        /*playerHealth -= newHealth;
        healthPlayer.value = playerHealth;
        if (PV.IsMine)
        {
            mySliderHealth.value = playerHealth;
        }*/
        PV.RPC("takeDamage", RpcTarget.All, newHealth);

    }


    [PunRPC]
    void healthadd(int healthToAdd)
    {
        playerHealth = healthToAdd;

        Debug.Log("SLIDER BEFORE: " + healthPlayer.value);
        healthPlayer.value = playerHealth;
        Debug.Log("SLIDER AFTER: " + healthPlayer.value);
        if (PV.IsMine)
        {
            mySliderHealth.value = playerHealth;
        }
    }

    [PunRPC]
    void takeDamage(int newHealth)
    {
        playerHealth -= newHealth;
        healthPlayer.value = playerHealth;
        if (PV.IsMine)
        {
            mySliderHealth.value = playerHealth;
        }
    }
}
