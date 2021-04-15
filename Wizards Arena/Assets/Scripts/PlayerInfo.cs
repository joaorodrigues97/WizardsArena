using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{

    private int PlayerHealth;
    private int PlayerCoins;
    private Text coins;
   

    // Start is called before the first frame update
    void Start()
    {
        PlayerHealth = 1100;
        PlayerCoins = 3000;
        coins = GameObject.FindGameObjectWithTag("coins").GetComponent<Text>();
        coins.text = PlayerCoins.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
