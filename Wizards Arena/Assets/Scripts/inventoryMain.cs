using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inventoryMain : MonoBehaviour
{

    public Button inventoryButton;
    public int PlayerCoins;
    public bool isOpen = false;
    public Button[] equips;
    private string[] names;
    Dictionary<string, string> equipsDescription;
    Dictionary<string, int> equipsPrice;
    public GameObject selectedImg;
    public GameObject itemName;
    public GameObject itemDescription;
    public GameObject itemPrice;
    private PlayerInfo playerInfo1;
    string[] itemInfor;


    // Start is called before the first frame update
    void Start()
    {
        equipsDescription = new Dictionary<string, string>();
        equipsPrice = new Dictionary<string, int>();
        names = new string[equips.Length];
        for (int i = 0; i < equips.Length; i++)
        {
            names[i] = equips[i].transform.parent.name;
        }
        playerInfo1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerInfo>();
        itemInfor = new string[3] { "0", "0", "0" };
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void onClickBuy()
    {
        if (int.Parse(itemInfor[2]) > 0)
        {
            BuyItem(itemInfor[2]);
        }
    }

    public void BuyItem(string itemPrice)
    {
        if (int.Parse(itemPrice) <= playerInfo1.getPlayerCoins())
        {
            playerInfo1.subCoins(int.Parse(itemPrice));
        }
    }

    public void checkClickInfo(Button button)
    {
        for (int i = 0; i < names.Length; i++)
        {
            if (button.transform.parent.name.Equals(names[i]))
            {
                selectedImg.GetComponent<Image>().sprite = button.GetComponent<Image>().sprite;
                itemInfor = itemInfo(names[i]);
                itemName.GetComponent<Text>().text = itemInfor[0];
                itemDescription.GetComponent<Text>().text = itemInfor[1];
                itemPrice.GetComponent<Text>().text = itemInfor[2];
                selectedImg.SetActive(true);
                itemName.SetActive(true);
                itemDescription.SetActive(true);
                itemPrice.SetActive(true);
            }
        }
    }

    public string[] itemInfo(string name)
    {
        string[] finalArray = new string[3];
        string itemName = "";
        string itemDescription = "";
        string itemPrice = "";

        switch (name)
        {
            case "Skill1":
                itemName = "Skill 1";
                itemDescription = "Description of skill 1";
                itemPrice = "1000";
                break;
            case "Skill2":
                itemName = "Skill 2";
                itemDescription = "Description of skill 2";
                itemPrice = "2000";
                break;
            case "Skill3":
                itemName = "Skill 3";
                itemDescription = "Description of skill 3";
                itemPrice = "3000";
                break;
            case "Equipment1":
                itemName = "Equipment 1";
                itemDescription = "Description of equipment 1";
                itemPrice = "1000";
                break;
            case "Equipment2":
                itemName = "Equipment 2";
                itemDescription = "Description of equipment 2";
                itemPrice = "2000";
                break;
            case "Equipment3":
                itemName = "Equipment 3";
                itemDescription = "Description of equipment 3";
                itemPrice = "3000";
                break;
            case "Equipment4":
                itemName = "Equipment 4";
                itemDescription = "Description of equipment 4";
                itemPrice = "4000";
                break;
            case "Equipment5":
                itemName = "Equipment 5";
                itemDescription = "Description of equipment 5";
                itemPrice = "5000";
                break;
            case "Equipment6":
                itemName = "Equipment 6";
                itemDescription = "Description of equipment 6";
                itemPrice = "6000";
                break;
        }
        finalArray[0] = itemName;
        finalArray[1] = itemDescription;
        finalArray[2] = itemPrice;
        return finalArray;
    }

    public void OpenInventory()
    {
        gameObject.SetActive(true);
        isOpen = true;
        
    }

    public void CloseInventory()
    {
        gameObject.SetActive(false);
        isOpen = false;
    }

    public void controllInventory() {
        if (isOpen)
        {
            CloseInventory();
        }
        else
        {
            OpenInventory();
        }
    }
}
