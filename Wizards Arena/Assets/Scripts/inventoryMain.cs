using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

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
    private GameController GM;
    private PlayerPowers PP;
    public Sprite[] SkillV2;
    public Sprite[] SkillV3;
    string[] itemInfor;
    private Button skillsButton;
    public enum skill1Version { V1, V2, V3, VF };
    public enum skill2Version { V1, V2, V3, VF };
    public enum skill3Version { V1, V2, V3, VF };
    public skill1Version Sk1V = skill1Version.V1;
    public skill2Version Sk2V = skill2Version.V1;
    public skill3Version Sk3V = skill3Version.V1;

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
        GM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameController>();
        PP = GameObject.FindGameObjectWithTag("GM").GetComponent<PlayerPowers>();
        itemInfor = new string[5] { "0", "0", "0", "", ""};
        
    }


    public void onClickBuy()
    {
        if (int.Parse(itemInfor[2]) > 0)
        {
            BuyItem(itemInfor[2]);
            
        }
    }

    public void changeSkillIcon(string btnName)
    {
        
        if (btnName.Equals("Skill1"))
        {
            if (Sk1V == skill1Version.V1)
            {
                skillsButton.GetComponent<Image>().sprite = SkillV2[0];
                selectedImg.GetComponent<Image>().sprite = SkillV2[0];
                Sk1V = skill1Version.V2;
            }else if (Sk1V == skill1Version.V2)
            {
                skillsButton.GetComponent<Image>().sprite = SkillV3[0];
                selectedImg.GetComponent<Image>().sprite = SkillV3[0];
                Sk1V = skill1Version.V3;
            }
            else if (Sk1V == skill1Version.V3)
            {
                skillsButton.GetComponent<Image>().sprite = SkillV3[0];
                selectedImg.GetComponent<Image>().sprite = SkillV3[0];
                Sk1V = skill1Version.VF;
            }
        }
        else if (btnName.Equals("Skill2"))
        {
            if (Sk2V == skill2Version.V1)
            {
                skillsButton.GetComponent<Image>().sprite = SkillV2[1];
                selectedImg.GetComponent<Image>().sprite = SkillV2[1];
                Sk2V = skill2Version.V2;
            }
            else if (Sk2V == skill2Version.V2)
            {
                skillsButton.GetComponent<Image>().sprite = SkillV3[1];
                selectedImg.GetComponent<Image>().sprite = SkillV3[1];
                Sk2V = skill2Version.V3;
            }
            else if (Sk2V == skill2Version.V3)
            {
                skillsButton.GetComponent<Image>().sprite = SkillV3[1];
                selectedImg.GetComponent<Image>().sprite = SkillV3[1];
                Sk2V = skill2Version.VF;
            }
        }
        else if (btnName.Equals("Skill3"))
        {
            if (Sk3V == skill3Version.V1)
            {
                skillsButton.GetComponent<Image>().sprite = SkillV2[2];
                selectedImg.GetComponent<Image>().sprite = SkillV2[2];
                Sk3V = skill3Version.V2;
            }
            else if (Sk3V == skill3Version.V2)
            {
                skillsButton.GetComponent<Image>().sprite = SkillV3[2];
                selectedImg.GetComponent<Image>().sprite = SkillV3[2];
                Sk3V = skill3Version.V3;
            }
            else if (Sk3V == skill3Version.V3)
            {
                skillsButton.GetComponent<Image>().sprite = SkillV3[2];
                selectedImg.GetComponent<Image>().sprite = SkillV3[2];
                Sk3V = skill3Version.VF;
            }
        }
        itemInfor = itemInfo(btnName);
        itemPrice.GetComponent<Text>().text = itemInfor[2];
        itemDescription.GetComponent<Text>().text = itemInfor[1];
    }

    public void BuyItem(string itemPrice)
    {
        int i = 3;
        try
        {
            i = int.Parse(itemInfor[3]); 
        }
        catch
        {
        }

        
        if (PhotonNetwork.MasterClient == PhotonNetwork.LocalPlayer)
        {
            
            if (int.Parse(itemPrice) <= GM.getPlayerCoins("Player1") && i != 4)
            {
                GM.subCoins(int.Parse(itemPrice), "Player1");
                PP.addExtras(itemInfor[0], "V"+itemInfor[3], "Player1");
                changeSkillIcon(itemInfor[0]);
                if (int.Parse(itemInfor[4]) == 0)
                {
                    itemInfor[3] = "4";
                }
                if (i == 3)
                {
                    skillsButton.interactable = false;
                }
            }
            
        }
        else
        {
            if (int.Parse(itemPrice) <= GM.getPlayerCoins("Player2") && i != 4)
            {
                GM.subCoins(int.Parse(itemPrice), "Player2");
                PP.addExtras(itemInfor[0], "V" + itemInfor[3], "Player2");
                changeSkillIcon(itemInfor[0]);
                if (int.Parse(itemInfor[4]) == 0)
                {
                    itemInfor[3] = "4";
                }
                if (i == 3)
                {
                    skillsButton.interactable = false;
                }
            }
        }
        
    }

    public void checkClickInfo(Button button)
    {
        for (int i = 0; i < names.Length; i++)
        {
            if (button.transform.parent.name.Equals(names[i]))
            {
                skillsButton = button;
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
        string[] finalArray = new string[5];
        string itemName = "";
        string itemDescription = "";
        string itemPrice = "";
        string skillLevel = "";
        string invType = "";

        switch (name)
        {
            case "Skill1":
                itemName = name;
                if (Sk1V == skill1Version.V1)
                {
                    itemDescription = "Description of skill 1";
                    itemPrice = "1000";
                    skillLevel = "1";
                    invType = "1";
                }else if (Sk1V == skill1Version.V2)
                {
                    itemDescription = "First Evolution of skill 1";
                    itemPrice = "1500";
                    skillLevel = "2";
                    invType = "1";
                }
                else if (Sk1V == skill1Version.V3)
                {
                    itemDescription = "Final Evolution of skill 1";
                    itemPrice = "2000";
                    skillLevel = "3";
                    invType = "1";
                }
                else if (Sk1V == skill1Version.VF)
                {
                    itemDescription = "Final Evolution of skill 1";
                    itemPrice = "2000";
                    skillLevel = "4";
                    invType = "1";
                }
                break;
            case "Skill2":
                itemName = name;
                if (Sk2V == skill2Version.V1)
                {
                    itemDescription = "Description of skill 2";
                    itemPrice = "2000";
                    skillLevel = "1";
                    invType = "1";
                }
                else if (Sk2V == skill2Version.V2)
                {
                    itemDescription = "First Evolution of skill 2";
                    itemPrice = "2500";
                    skillLevel = "2";
                    invType = "1";
                }
                else if (Sk2V == skill2Version.V3)
                {
                    itemDescription = "Final Evolution of skill 2";
                    itemPrice = "3000";
                    skillLevel = "3";
                    invType = "1";
                }
                else if (Sk2V == skill2Version.VF)
                {
                    itemDescription = "Final Evolution of skill 2";
                    itemPrice = "3000";
                    skillLevel = "4";
                    invType = "1";
                }
                break;
            case "Skill3":
                itemName = name;
                if (Sk3V == skill3Version.V1)
                {
                    itemDescription = "Description of skill 3";
                    itemPrice = "3000";
                    skillLevel = "1";
                    invType = "1";
                }
                else if (Sk3V == skill3Version.V2)
                {
                    itemDescription = "First Evolution of skill 3";
                    itemPrice = "3500";
                    skillLevel = "2";
                    invType = "1";
                }
                else if (Sk3V == skill3Version.V3)
                {
                    itemDescription = "Final Evolution of skill 3";
                    itemPrice = "4000";
                    skillLevel = "3";
                    invType = "1";
                }
                else if (Sk3V == skill3Version.VF)
                {
                    itemDescription = "Final Evolution of skill 3";
                    itemPrice = "4000";
                    skillLevel = "4";
                    invType = "1";
                }
                break;
            case "Equipment1":
                itemName = name;
                itemDescription = "Description of equipment 1";
                itemPrice = "1000";
                invType = "0";
                break;
            case "Equipment2":
                itemName = name;
                itemDescription = "Description of equipment 2";
                itemPrice = "2000";
                invType = "0";
                break;
            case "Equipment3":
                itemName = name;
                itemDescription = "Description of equipment 3";
                itemPrice = "3000";
                invType = "0";
                break;
            case "Equipment4":
                itemName = name;
                itemDescription = "Description of equipment 4";
                itemPrice = "4000";
                invType = "0";
                break;
            case "Equipment5":
                itemName = name;
                itemDescription = "Description of equipment 5";
                itemPrice = "5000";
                invType = "0";
                break;
            case "Equipment6":
                itemName = name;
                itemDescription = "Description of equipment 6";
                itemPrice = "6000";
                invType = "0";
                break;
        }
        finalArray[0] = itemName;
        finalArray[1] = itemDescription;
        finalArray[2] = itemPrice;
        finalArray[3] = skillLevel;
        finalArray[4] = invType;
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
