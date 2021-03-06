using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviourPunCallbacks, IPunObservable
{
    private int PlayerCoins1;
    private int PlayerCoins2;
    private GameObject[] dragonPlayer1;
    private GameObject[] dragonPlayer2;
    private GameObject[] turretP1;
    private GameObject[] turretP2;
    private string player1Str = "Player1";
    private string player2Str = "Player2";
    private List<int> dragon1CoinsExp;
    private List<int> dragon2CoinsExp;
    private List<int> turret1CoinsExp;
    private List<int> turret2CoinsExp;
    private int initialCoins = 500;
    private Text coins;
    private PhotonView player1PV = null;
    private PhotonView player2PV = null;
    private bool isPlayer1Dead = false;
    private bool isPlayer2Dead = false;
    public GameObject winCanvas;
    public Image winImg;
    public Image DefeatImg;
    public AudioSource btnClick;
    private int minionPrice;
    private int turretPrice;




    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(isPlayer1Dead);
            stream.SendNext(isPlayer2Dead);
        }
        else
        {
            isPlayer1Dead = (bool)stream.ReceiveNext();
            isPlayer2Dead = (bool)stream.ReceiveNext();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        minionPrice = 1000;
        turretPrice = 4000;
        coins = GameObject.FindGameObjectWithTag("coins").GetComponent<Text>();
        PlayerCoins1 = initialCoins;
        PlayerCoins2 = initialCoins;
        dragon1CoinsExp = new List<int>();
        dragon2CoinsExp = new List<int>();
        turret1CoinsExp = new List<int>();
        turret2CoinsExp = new List<int>();
        coins.text = initialCoins.ToString();
        winCanvas.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player1PV == null)
        {
            try
            {
                player1PV = GameObject.FindGameObjectWithTag("Player1").GetComponent<PhotonView>();
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
            }
            catch (Exception)
            {

            }
        }
        dragonPlayer1 = GameObject.FindGameObjectsWithTag("DragonP1");
        dragonPlayer2 = GameObject.FindGameObjectsWithTag("DragonP2");
        turretP1 = GameObject.FindGameObjectsWithTag("TurretP1");
        turretP2 = GameObject.FindGameObjectsWithTag("TurretP2");

        checkDragonHealth();
        checkTurretHealth();
        addExpAndCoins();

        endGame();
    }

    private void endGame()
    {
        if (isPlayer1Dead)
        {
            winCanvas.SetActive(true);
            if (PhotonNetwork.LocalPlayer == PhotonNetwork.MasterClient)
            {
                DefeatImg.gameObject.SetActive(true);
            }
            else
            {
                winImg.gameObject.SetActive(true);
            }
            
        }
        if (isPlayer2Dead)
        {
            winCanvas.SetActive(true);
            if (PhotonNetwork.LocalPlayer == PhotonNetwork.MasterClient)
            {
                winImg.gameObject.SetActive(true);
            }
            else
            {
                DefeatImg.gameObject.SetActive(true);
            }
        }
    }

    public void playerLeave()
    {
        winCanvas.SetActive(true);
        winImg.gameObject.SetActive(true);
    }

    public void setPlayer1Dead()
    {
        isPlayer1Dead = true;
    }

    public void setPlayer2Dead()
    {
        isPlayer2Dead = true;
    }

    public void addCoins(int value, string player)
    {
        if (player.Equals(player1Str))
        {
            PlayerCoins1 += value;
            coins.text = PlayerCoins1.ToString();
        }else if (player.Equals(player2Str))
        {
            PlayerCoins2 += value;
            coins.text = PlayerCoins2.ToString();
        }
        
    }

    public void addExpAndCoins()
    {
        if (dragon1CoinsExp.Count != 0)
        {
            for (int x1 = 0; x1 < dragon1CoinsExp.Count; x1++)
            {
                if (player2PV.IsMine)
                {
                    addCoins(minionPrice, player2Str);
                }
                dragon1CoinsExp.RemoveAt(x1);
            }
        }
        if (dragon2CoinsExp.Count != 0)
        {
            for (int x2 = 0; x2 < dragon2CoinsExp.Count; x2++)
            {
                if (player1PV.IsMine)
                {
                    addCoins(minionPrice, player1Str);
                }
                dragon2CoinsExp.RemoveAt(x2);
            }
        }
        if (turret1CoinsExp.Count != 0)
        {
            for (int x1 = 0; x1 < turret1CoinsExp.Count; x1++)
            {
                if (player2PV.IsMine)
                {
                    addCoins(turretPrice, player2Str);
                }
                turret1CoinsExp.RemoveAt(x1);
            }
        }
        if (turret2CoinsExp.Count != 0)
        {
            for (int x2 = 0; x2 < turret2CoinsExp.Count; x2++)
            {
                if (player1PV.IsMine)
                {
                    addCoins(turretPrice, player1Str);
                }
                turret2CoinsExp.RemoveAt(x2);
            }
        }
    }

    public void checkDragonHealth()
    {
        int Minion1health;
        for (int i = 0; i < dragonPlayer1.Length; i++)
        {
            Minion1health = dragonPlayer1[i].GetComponent<MinionHealth>().getMinionHealth();
            
            if (Minion1health <= 0)
            {
                dragonPlayer1[i].GetComponent<MinionHealth>().setMinionHealth(100);
                dragonPlayer1[i].GetComponent<MinionHealth>().MinionDie();
                dragon1CoinsExp.Add(1);
            }
        }
        int Minion2health;
        for (int i = 0; i < dragonPlayer2.Length; i++)
        {
            Minion2health = dragonPlayer2[i].GetComponent<MinionHealth>().getMinionHealth();
            
            if (Minion2health <= 0)
            {
                dragonPlayer2[i].GetComponent<MinionHealth>().setMinionHealth(100);
                dragonPlayer2[i].GetComponent<MinionHealth>().MinionDie();
                dragon2CoinsExp.Add(1);
            }
        }
    }

    public void checkTurretHealth()
    {
        int turretHealthP1;
        for (int i = 0; i < turretP1.Length; i++)
        {
            turretHealthP1 = turretP1[i].GetComponent<TurretHealth>().getTurretHealth();

            if (turretHealthP1 <= 0)
            {
                turretP1[i].GetComponent<TurretHealth>().setHealthTurretDeath(500);
                turretP1[i].GetComponent<TurretHealth>().TurretDie();
                GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerHealth>().subResistence(10);
                turret1CoinsExp.Add(1);
                
            }
        }
        int turretHealthP2;
        for (int i = 0; i < turretP2.Length; i++)
        {
            turretHealthP2 = turretP2[i].GetComponent<TurretHealth>().getTurretHealth();

            if (turretHealthP2 <= 0)
            {
                turretP2[i].GetComponent<TurretHealth>().setHealthTurretDeath(500);
                turretP2[i].GetComponent<TurretHealth>().TurretDie();
                GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerHealth>().subResistence(10);
                turret2CoinsExp.Add(1);
                
            }
        }
    }

    public void subCoins(int value, string player)
    {
        
        if (player.Equals(player1Str))
        {
            PlayerCoins1 -= value;
            coins.text = PlayerCoins1.ToString();
        }
        if (player.Equals(player2Str))
        {
            PlayerCoins2 -= value;
            coins.text = PlayerCoins2.ToString();
        }
    }

    public int getPlayerCoins(string player)
    {
        if (player.Equals(player1Str))
        {
            return PlayerCoins1;
        }
        if (player.Equals(player2Str))
        {
            return PlayerCoins2;
        }
        return 0;
        
    }
    
}
