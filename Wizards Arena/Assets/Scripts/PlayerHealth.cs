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
    private int playerCooldown;
    private int playerHealth;
    private int playerDamage;
    public Slider healthPlayer;

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
        playerMaxHealth = 1100;
        playerDamage = 30;
        playerCooldown = 30;
        playerResistence = 30;
        playerHealth = playerMaxHealth;
        healthPlayer.maxValue = playerMaxHealth;
        healthPlayer.value = playerMaxHealth;
    }

    

    public int getPlayerDamage()
    {
        return playerDamage;
    }

    public void setPlayerDamage(int damage)
    {
        playerDamage = damage;
    }

    public int getResistence()
    {
        return playerResistence;
    }

    public void subResistence(int resistence)
    {
        playerResistence -= resistence;
    }

    public void addResistence(int resistence)
    {
        playerResistence += resistence;
    }

    public int getPlayerCD()
    {
        return playerCooldown;
    }

    public void setPlayerCD(int cooldown)
    {
        playerCooldown = cooldown;
    }

    public int getMaxHealth()
    {
        return playerMaxHealth;
    }

    public void setMaxHealth(int newMaxHealth)
    {
        healthPlayer.maxValue = newMaxHealth;
        playerMaxHealth = newMaxHealth;
    }

    public int getCurrentHealth()
    {
        return playerHealth;
    }

    public void setCurrentHealth(int healthToAdd)
    {
        playerHealth = healthToAdd;
    }


    public void TakeDamage(int newHealth)
    {
        Debug.Log("Antiga vida= " + playerHealth);
        playerHealth -= newHealth;
        healthPlayer.value = playerHealth;
        Debug.Log("Nova vida= " + playerHealth);
    }
}
