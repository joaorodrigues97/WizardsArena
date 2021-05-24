using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerPowers : MonoBehaviourPunCallbacks, IPunObservable
{

    public Dictionary<string, bool> playerInv1, playerInv2;
    private PlayerHealth playerH;
    private bool isLifeSteal;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(playerInv1);
            stream.SendNext(playerInv2);
        }
        else
        {
            playerInv1 = (Dictionary<string, bool>)stream.ReceiveNext();
            playerInv2 = (Dictionary<string, bool>)stream.ReceiveNext();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerInv1 = new Dictionary<string, bool>();
        playerInv1.Add("Skill1V1", false);
        playerInv1.Add("Skill1V2", false);
        playerInv1.Add("Skill1V3", false);
        playerInv1.Add("Skill2V1", false);
        playerInv1.Add("Skill2V2", false);
        playerInv1.Add("Skill2V3", false);
        playerInv1.Add("Skill3V1", false);
        playerInv1.Add("Skill3V2", false);
        playerInv1.Add("Skill3V3", false);
        playerInv2 = new Dictionary<string, bool>(playerInv1);

        playerH = GetComponent<PlayerHealth>();

        isLifeSteal = false;
    }

    public void addExtras(string extra, string version, string player)
    {
        if (player.Equals("Player1"))
        {
            switch (extra)
            {
                case "Skill1":
                    if (version.Equals("V1"))
                    {
                        //DAR 50 DANO
                        //Debug.Log("PLAYER1 - +50Dano");
                        playerInv1[extra + "V1"] = true;
                    }
                    else if (version.Equals("V2"))
                    {
                        //DAR 80 DANO
                        //Debug.Log("PLAYER1 - +80Dano");
                        playerInv1[extra + "V1"] = false;
                        playerInv1[extra + "V2"] = true;
                    }
                    else if (version.Equals("V3"))
                    {
                        //DAR 120 DANO
                        //Debug.Log("PLAYER1 - +120Dano");
                        playerInv1[extra + "V2"] = false;
                        playerInv1[extra + "V3"] = true;
                    }
                    break;
                case "Skill2":
                    if (version.Equals("V1"))
                    {
                        //IR BUSCAR O CURRENT HEALTH E AUMENTAR 5%
                        //Debug.Log("PLAYER1 - +5%HEALTH");
                        playerInv1[extra + "V1"] = true;
                    }
                    else if (version.Equals("V2"))
                    {
                        //IR BUSCAR O CURRENT HEALTH E AUMENTAR 10%
                        //Debug.Log("PLAYER1 - +10%HEALTH");
                        playerInv1[extra + "V1"] = false;
                        playerInv1[extra + "V2"] = true;
                    }
                    else if (version.Equals("V3"))
                    {
                        //IR BUSCAR O CURRENT HEALTH E AUMENTAR 15%
                        //Debug.Log("PLAYER1 - +15%HEALTH");
                        playerInv1[extra + "V2"] = false;
                        playerInv1[extra + "V3"] = true;
                    }
                    break;
                case "Skill3":
                    if (version.Equals("V1"))
                    {
                        //ULTIMATE 25% DAMAGE
                        //Debug.Log("PLAYER1 - +25%DAMAGE");
                        playerInv1[extra + "V1"] = true;
                    }
                    else if (version.Equals("V2"))
                    {
                        //ULTIMATE 30% DAMAGE
                        //Debug.Log("PLAYER1 - +30%DAMAGE");
                        playerInv1[extra + "V1"] = false;
                        playerInv1[extra + "V2"] = true;
                    }
                    else if (version.Equals("V3"))
                    {
                        //ULTIMATE 40% DAMAGE
                        //Debug.Log("PLAYER1 - +40%DAMAGE");
                        playerInv1[extra + "V2"] = false;
                        playerInv1[extra + "V3"] = true;
                    }
                    break;
                case "Equipment1":
                    //DIMINUI O COOLDOWN EM 10%
                    //Debug.Log("PLAYER1 - -10%CD");

                    //LEMBRETE FAZER DEPOIS
                    break;
                case "Equipment2":
                    //AUMENTA O DAMAGE 15%
                    //Debug.Log("PLAYER1 - +15%DAMAGE");
                    int baseDmgEquip2 = playerH.getPlayerDamage();
                    int newBaseDmgEquip2 = baseDmgEquip2 + (baseDmgEquip2 * (15 / 100));
                    playerH.setPlayerDamage(newBaseDmgEquip2);
                    break;
                case "Equipment3":
                    //LIFE STEAL DE 35% DO DAMAGE FEITO
                    //Debug.Log("PLAYER1 - +LIFE STEAL");
                    Debug.Log("ENTREI NO 3");
                    isLifeSteal = true;



                    //FICAMOS AQUI A TENTAR VER O BUG DO BOOL


                    //WARNING
                    //WARNINGGGGGGGGGGG

                    break;
                case "Equipment4":
                    //TENTAR AUMENTAR A RESISTENCIA 15
                    //Debug.Log("PLAYER1 - +15 RESISTENCIA");
                    playerH.addResistence(15);
                    break;
                case "Equipment5":
                    //AUMENTA O DAMAGE 30%
                    //Debug.Log("PLAYER1 - +30%DAMAGE");
                    int baseDmgEquip5 = playerH.getPlayerDamage();
                    int newBaseDmgEquip5 = baseDmgEquip5 + (baseDmgEquip5 * (30 / 100));
                    playerH.setPlayerDamage(newBaseDmgEquip5);
                    break;
                case "Equipment6":
                    //AUMENTA A CURRENT LIFE E A MAX HEALTH
                    //Debug.Log("PLAYER1 - +HEAL OVERTIME");
                    changeLifeEquip6(20);
                    break;
            }
        } else if (player.Equals("Player2"))
        {
            switch (extra)
            {
                case "Skill1":
                    if (version.Equals("V1"))
                    {
                        //DAR 50 DANO
                        //Debug.Log("PLAYER1 - +50Dano");
                        playerInv2[extra + "V1"] = true;
                    }
                    else if (version.Equals("V2"))
                    {
                        //DAR 80 DANO
                        //Debug.Log("PLAYER1 - +80Dano");
                        playerInv2[extra + "V1"] = false;
                        playerInv2[extra + "V2"] = true;
                    }
                    else if (version.Equals("V3"))
                    {
                        //DAR 120 DANO
                        //Debug.Log("PLAYER1 - +120Dano");
                        playerInv2[extra + "V2"] = false;
                        playerInv2[extra + "V3"] = true;
                    }
                    break;
                case "Skill2":
                    if (version.Equals("V1"))
                    {
                        //IR BUSCAR O CURRENT HEALTH E AUMENTAR 5%
                        //Debug.Log("PLAYER1 - +5%HEALTH");
                        playerInv2[extra + "V1"] = true;
                    }
                    else if (version.Equals("V2"))
                    {
                        //IR BUSCAR O CURRENT HEALTH E AUMENTAR 10%
                        //Debug.Log("PLAYER1 - +10%HEALTH");
                        playerInv2[extra + "V1"] = false;
                        playerInv2[extra + "V2"] = true;
                    }
                    else if (version.Equals("V3"))
                    {
                        //IR BUSCAR O CURRENT HEALTH E AUMENTAR 15%
                        //Debug.Log("PLAYER1 - +15%HEALTH");
                        playerInv2[extra + "V2"] = false;
                        playerInv2[extra + "V3"] = true;
                    }
                    break;
                case "Skill3":
                    if (version.Equals("V1"))
                    {
                        //ULTIMATE 25% DAMAGE
                        //Debug.Log("PLAYER1 - +25%DAMAGE");
                        playerInv2[extra + "V1"] = true;
                    }
                    else if (version.Equals("V2"))
                    {
                        //ULTIMATE 30% DAMAGE
                        //Debug.Log("PLAYER1 - +30%DAMAGE");
                        playerInv2[extra + "V1"] = false;
                        playerInv2[extra + "V2"] = true;
                    }
                    else if (version.Equals("V3"))
                    {
                        //ULTIMATE 40% DAMAGE
                        //Debug.Log("PLAYER1 - +40%DAMAGE");
                        playerInv2[extra + "V2"] = false;
                        playerInv2[extra + "V3"] = true;
                    }
                    break;
                case "Equipment1":
                    //DIMINUI O COOLDOWN EM 10%
                    //Debug.Log("PLAYER1 - -10%CD");

                    //LEMBRETE FAZER DEPOIS
                    break;
                case "Equipment2":
                    //AUMENTA O DAMAGE 15%
                    //Debug.Log("PLAYER1 - +15%DAMAGE");
                    int baseDmgEquip2 = playerH.getPlayerDamage();
                    int newBaseDmgEquip2 = baseDmgEquip2 + (baseDmgEquip2 * (15 / 100));
                    playerH.setPlayerDamage(newBaseDmgEquip2);
                    break;
                case "Equipment3":
                    //LIFE STEAL DE 35% DO DAMAGE FEITO
                    //Debug.Log("PLAYER1 - +LIFE STEAL");
                    isLifeSteal = true;
                    break;
                case "Equipment4":
                    //TENTAR AUMENTAR A RESISTENCIA 15
                    //Debug.Log("PLAYER1 - +15 RESISTENCIA");
                    playerH.addResistence(15);
                    break;
                case "Equipment5":
                    //AUMENTA O DAMAGE 30%
                    //Debug.Log("PLAYER1 - +30%DAMAGE");
                    int baseDmgEquip5 = playerH.getPlayerDamage();
                    int newBaseDmgEquip5 = baseDmgEquip5 + (baseDmgEquip5 * (30 / 100));
                    playerH.setPlayerDamage(newBaseDmgEquip5);
                    break;
                case "Equipment6":
                    //AUMENTA A CURRENT LIFE E A MAX HEALTH
                    //Debug.Log("PLAYER1 - +HEAL OVERTIME");
                    changeLifeEquip6(20);
                    break;
            }

        }

    }

    public bool getLifeStealStatus()
    {
        Debug.Log("LIFE STEAL:" + isLifeSteal);
        return isLifeSteal;
    }

    public void addHealth(int healthToAdd)
    {
        int maxHealth =playerH.getMaxHealth();
        int currentHealth = playerH.getCurrentHealth();

        if(currentHealth + healthToAdd <= maxHealth){
            playerH.setCurrentHealth(currentHealth + healthToAdd);
        }else{
            playerH.setCurrentHealth(maxHealth);
        }
    }

    public void changeLifeEquip6(int percentage){

        int maxHealth = playerH.getMaxHealth();
        int newMaxHealth = maxHealth + (maxHealth * (percentage / 100));
        playerH.setMaxHealth(newMaxHealth);
        addHealth((maxHealth*(percentage/100))/2);
    }

    public void changeCoolDown(int newCooldown)
    {
        //Player.cooldown = newCooldown
    }

    public void changeBaseDamage(int newDamage)
    {
        //Player.damage = newDamage
    }

    public void changeResistence(int rPercentage)
    {
        //METER NO PLAYER HEALTH UMA VARIAVEL RESISTENCE
        //NO PARTICLE COLLISION QUANDO UM DOS PLAYERS DA DAMAGE, IR BUSCAR A RESSISTENCE E FAZER Player1.TakeDamage(Damage-Player1.resistence)
        // OU ENTAO Player1.TakeDamage(Damage-(Player1.resistence*Damage)) SE FOR COM PERCENTAGENS
        //VERIFICAR A RESISTENCIA COM AS TORRES
    }

}
