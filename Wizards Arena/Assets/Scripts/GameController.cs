using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class GameController : MonoBehaviourPunCallbacks, IPunObservable
{
    private int PlayerHealth1 = 1100;
    private int PlayerHealth2 = 1100;
    private int PlayerCoins1;
    private int PlayerCoins2;
    private GameObject[] dragonPlayer1;
    private GameObject[] dragonPlayer2;
    private string player1Str = "Player1";
    private string player2Str = "Player2";
    private int initialCoins = 3000;
    private Text coins;
    private int player1Exp;
    private int player2Exp;
    private Text expUI;
    private Text expUIPlayer1;
    private Text expUIPlayer2;
    public PhotonView player1PV = null;
    public PhotonView player2PV = null;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(player1Exp);
            stream.SendNext(player2Exp);
            stream.SendNext(PlayerCoins1);
            stream.SendNext(PlayerCoins2);
        }
        else
        {
            player1Exp = (int)stream.ReceiveNext();
            player2Exp = (int)stream.ReceiveNext();
            PlayerCoins1 = (int)stream.ReceiveNext();
            PlayerCoins2 = (int)stream.ReceiveNext();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        coins = GameObject.FindGameObjectWithTag("coins").GetComponent<Text>();
        expUI = GameObject.FindGameObjectWithTag("exp").GetComponent<Text>();
        PlayerCoins1 = initialCoins;
        PlayerCoins2 = initialCoins;
        player1Exp = 0;
        player2Exp = 0;
        coins.text = initialCoins.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(player1PV == null)
        {
            try
            {
                player1PV = GameObject.FindGameObjectWithTag("Player1").GetComponent<PhotonView>();
                expUIPlayer1 = GameObject.FindGameObjectWithTag("PlayerExp1").GetComponent<Text>();
            }
            catch(Exception)
            {
                
            }
            
        }
        if(player2PV == null)
        {
            try
            {
                player2PV = GameObject.FindGameObjectWithTag("Player2").GetComponent<PhotonView>();
                expUIPlayer2 = GameObject.FindGameObjectWithTag("PlayerExp2").GetComponent<Text>();
            }
            catch (Exception)
            {

            }
        }
        dragonPlayer1 = GameObject.FindGameObjectsWithTag("DragonP1");
        dragonPlayer2 = GameObject.FindGameObjectsWithTag("DragonP2");
        checkDragonHealth();
        try
        {
            checkExp1Bar();
        }
        catch (Exception) { }
        try
        {
            checkExp2Bar();
        }
        catch (Exception) { }
    }

    public void addCoins(int value, string player)
    {
        if (player.Equals(player1Str))
        {
            PlayerCoins1 += value;
            if (player1PV.IsMine)
            {
                coins.text = PlayerCoins1.ToString();
            }
        }else if (player.Equals(player2Str))
        {
            PlayerCoins2 += value;
            if (player2PV.IsMine)
            {
                coins.text = PlayerCoins2.ToString();
            }
        }
        
    }

    public void checkDragonHealth()
    {
        int Minion1health;
        for (int i = 0; i < dragonPlayer1.Length; i++)
        {
            Minion1health = dragonPlayer1[i].GetComponent<MinionHealth>().health;
            if (Minion1health <= 0)
            {
                dragonPlayer1[i].GetComponent<MinionHealth>().MinionDie();
                addCoins(50, player2Str);
                addExp(1000, player2Str);
                Debug.Log("EXP1: " + player1Exp);
            }
        }
        int Minion2health;
        for (int i = 0; i < dragonPlayer2.Length; i++)
        {
            Minion2health = dragonPlayer2[i].GetComponent<MinionHealth>().health;
            if (Minion2health <= 0)
            {
                dragonPlayer2[i].GetComponent<MinionHealth>().MinionDie();
                addCoins(50, player1Str);
                addExp(1000, player1Str);
                Debug.Log("EXP2: " + player2Exp);
            }
        }
        
    }

    /*public void subCoins(int value)
    {
        PlayerCoins -= value;
        coins.text = PlayerCoins.ToString();
    }

    public int getPlayerCoins()
    {
        return this.PlayerCoins;
    }*/

    public void addExp(int exp, string Player)
    {
        if (Player.Equals(player1Str))
        {
            player1Exp += exp;
            PlayerLevels(player1Str);
            
        }
        if (Player.Equals(player2Str))
        {
            player2Exp += exp;
            PlayerLevels(player2Str);
            
        }

    }

    public void checkExp1Bar()
    {

        if (player1Exp < 1000)
        {
            expUIPlayer1.text = "1";
        }
        else if (player1Exp >= 1000 && player1Exp < 3000)
        {
            expUIPlayer1.text = "2";
        }
        else if (player1Exp >= 3000 && player1Exp < 6000)
        {
            expUIPlayer1.text = "3";
        }
        else if (player1Exp >= 6000 && player1Exp < 12000)
        {
            expUIPlayer1.text = "4";
        }

    }

    public void checkExp2Bar()
    {

        if (player2Exp < 1000)
        {
            expUIPlayer2.text = "1";
        }
        else if (player2Exp >= 1000 && player2Exp < 3000)
        {
            expUIPlayer2.text = "2";
        }
        else if (player2Exp >= 3000 && player2Exp < 6000)
        {
            expUIPlayer2.text = "3";
        }
        else if (player2Exp >= 6000 && player2Exp < 12000)
        {
            expUIPlayer2.text = "4";
        }
    }

    public void PlayerLevels(string player)
    {

        if (player.Equals(player1Str))
        {
            if (player1PV.IsMine)
            {
                if (player1Exp < 1000)
                {
                    expUI.text = "1";
                }
                else if (player1Exp >= 1000 && player1Exp < 3000)
                {
                    expUI.text = "2";
                }
                else if (player1Exp >= 3000 && player1Exp < 6000)
                {
                    expUI.text = "3";
                }
                else if (player1Exp >= 6000 && player1Exp < 12000)
                {
                    expUI.text = "4";
                }
            }
        }

        if (player.Equals(player2Str))
        {
            if (player2PV.IsMine)
            {
                if (player2Exp < 1000)
                {
                    expUI.text = "1";
                }
                else if (player2Exp >= 1000 && player2Exp < 3000)
                {
                    expUI.text = "2";
                }
                else if (player2Exp >= 3000 && player2Exp < 6000)
                {
                    expUI.text = "3";
                }
                else if (player2Exp >= 6000 && player2Exp < 12000)
                {
                    expUI.text = "4";
                }
            }
        }
    }
}