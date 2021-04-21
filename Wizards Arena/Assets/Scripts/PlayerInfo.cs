using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerInfo : MonoBehaviourPunCallbacks, IPunObservable
{

    private int PlayerHealth= 1100;
    private int PlayerCoins = 3000;
    private Text coins;
    private int player1Exp = 0;
    private int player2Exp = 0;
    private Text expUI;
    private Text expUIPlayer1;
    private Text expUIPlayer2;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(player1Exp);
            stream.SendNext(player2Exp);

        }
        else
        {
            player1Exp = (int)stream.ReceiveNext();
            player2Exp = (int)stream.ReceiveNext();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        coins = GameObject.FindGameObjectWithTag("coins").GetComponent<Text>();
        expUI = GameObject.FindGameObjectWithTag("exp").GetComponent<Text>();
        coins.text = PlayerCoins.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.CompareTag("Player1"))
        {
            expUIPlayer2 = GameObject.FindGameObjectWithTag("PlayerExp2").GetComponent<Text>();
        }
        if (gameObject.CompareTag("Player2"))
        {
            expUIPlayer1 = GameObject.FindGameObjectWithTag("PlayerExp1").GetComponent<Text>();
        }

        PlayerLevels();
        
    }

    public void addCoins(int value)
    {
        PlayerCoins += value;
        coins.text = PlayerCoins.ToString();
    }

    public void subCoins(int value)
    {
        PlayerCoins -= value;
        coins.text = PlayerCoins.ToString();
    }

    public int getPlayerCoins()
    {
        return this.PlayerCoins;
    }

    public void addExp(int exp, string Player)
    {
        if (Player.Equals("player1"))
        {
            player1Exp += exp;
            Debug.Log("EXP: " + player1Exp);
        }
        if (Player.Equals("player2"))
        {
            player2Exp += exp;
            Debug.Log("EXP: " + player2Exp);
        }
    }

    public void PlayerLevels()
    {
        Debug.Log("ENTREI NO PLAYER LEVELS");
        if (gameObject.CompareTag("Player1"))
        {
            if (player1Exp < 1000)
            {
                expUI.text = "1";
                expUIPlayer1.text = "1";
                Debug.Log("Player1: Level1");
            }
            else if (player1Exp >= 1000 && player1Exp < 3000)
            {
                expUI.text = "2";
                expUIPlayer1.text = "2";
                Debug.Log("Player1: Level2");
            }
            else if (player1Exp >= 3000 && player1Exp < 6000)
            {
                expUI.text = "3";
                expUIPlayer1.text = "3";
                Debug.Log("Player1: Level3");
            }
            else if (player1Exp >= 6000 && player1Exp < 12000)
            {
                expUI.text = "4";
                expUIPlayer1.text = "4";
                Debug.Log("Player1: Level4");
            }

            if (player2Exp < 1000)
            {
                expUIPlayer2.text = "1";
                Debug.Log("Player2: Level1");
            }
            else if (player2Exp >= 1000 && player2Exp < 3000)
            {
                expUIPlayer2.text = "2";
                Debug.Log("Player2: Level2");
            }
            else if (player2Exp >= 3000 && player2Exp < 6000)
            {
                expUIPlayer2.text = "3";
                Debug.Log("Player2: Level3");
            }
            else if (player1Exp >= 6000 && player1Exp < 12000)
            {
                expUIPlayer2.text = "4";
                Debug.Log("Player2: Level4");
            }
        }

        if (gameObject.CompareTag("Player2"))
        {
            if (player2Exp < 1000)
            {
                expUI.text = "1";
                expUIPlayer2.text = "1";
            }
            else if (player2Exp >= 1000 && player2Exp < 3000)
            {
                expUI.text = "2";
                expUIPlayer2.text = "2";
            }
            else if (player2Exp >= 3000 && player2Exp < 6000)
            {
                expUI.text = "3";
                expUIPlayer2.text = "3";
            }
            else if (player1Exp >= 6000 && player1Exp < 12000)
            {
                expUI.text = "4";
                expUIPlayer2.text = "4";
            }

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
    }
}
