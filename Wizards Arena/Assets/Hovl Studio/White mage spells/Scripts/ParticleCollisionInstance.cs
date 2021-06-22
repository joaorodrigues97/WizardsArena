/*This script created by using docs.unity3d.com/ScriptReference/MonoBehaviour.OnParticleCollision.html*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;


public class ParticleCollisionInstance : MonoBehaviourPunCallbacks
{
    public GameObject[] EffectsOnCollision;
    public float Offset = 0;
    public float DestroyTimeDelay = 5;
    public bool UseWorldSpacePosition;
    public bool UseFirePointRotation;
    private ParticleSystem part;
    private MinionHealth minionHealth;
    private TurretHealth towerHeath;
    public bool canShoot;
    private PlayerHealth player;
    

    void Start()
    {
        part = GetComponent<ParticleSystem>();
        canShoot = false;
        
    }

    void OnParticleCollision(GameObject other)
    {
        /*if (gameObject.tag.Equals("EarthShatter"))
        {
            Debug.Log("HEARTHSHATTER TAG: "+ part.transform.parent.parent.tag +" OTHER: "+other.tag);
            //Debug.Log("ACERTOU COM O HEARTHSHATTER: "+other);
            if (other.CompareTag("DragonP1") && part.transform.parent.parent.CompareTag("Player2"))
            {
                minionHealth = other.GetComponent<MinionHealth>();
                minionHealth.setDamage(part.transform.parent.parent.GetComponent<PlayerAttack1>().getHearthShakeDmg());
                minionHealth.wasHit = true;
                Debug.Log("HEARTHSHATTER MINION: " + other);
            }
            else if (other.CompareTag("DragonP2") && part.transform.parent.parent.CompareTag("Player1"))
            {
                minionHealth = other.GetComponent<MinionHealth>();
                minionHealth.setDamage(part.transform.parent.parent.GetComponent<PlayerAttack1>().getHearthShakeDmg());
                minionHealth.wasHit = true;
                Debug.Log("HEARTHSHATTER MINION: " + other);
            }

            if (other.CompareTag("Player1") && part.transform.parent.parent.CompareTag("Player2"))
            {
                player = other.GetComponent<PlayerHealth>();
                int player1Res = player.getResistence();
                int player2Damage = part.transform.parent.parent.GetComponent<PlayerAttack1>().getHearthShakeDmg();
                Debug.Log("HEARTHSHATTER DMG: " + player2Damage);
                int totalDamage = (int)(player2Damage - (player2Damage * (player1Res / 100.0f)));
                Debug.Log("HEARTHSHATTER TOTAL DMG: " + totalDamage);
                player.TakeDamage(totalDamage);
                Debug.Log("HEARTHSHATTER PLAYER: " + other+"; ENEMY LIFE: "+player.getCurrentHealth());
            }
            else if (other.CompareTag("Player2") && part.transform.parent.parent.CompareTag("Player1"))
            {
                player = other.GetComponent<PlayerHealth>();
                int player2Res = player.getResistence();
                int player1Damage = part.transform.parent.parent.GetComponent<PlayerAttack1>().getHearthShakeDmg();
                Debug.Log("HEARTHSHATTER DMG: " + player1Damage);
                int totalDamage = (int)(player1Damage - (player1Damage * (player2Res / 100.0f)));
                Debug.Log("HEARTHSHATTER TOTAL DMG: " + totalDamage);
                player.TakeDamage(totalDamage);
                Debug.Log("HEARTHSHATTER PLAYER: " + other + "; ENEMY LIFE: " + player.getCurrentHealth());
            }

            part.GetComponent<ParticleSystem>().Stop();
            return;
        }*/
        
        Debug.Log("Entrei no collision: "+other);
        if(other.CompareTag("DragonP1") && part.transform.parent.parent.CompareTag("Player2"))
        {
            minionHealth = other.GetComponent<MinionHealth>();
            minionHealth.setDamage(part.transform.parent.parent.GetComponent<PlayerHealth>().getPlayerDamage());
            minionHealth.wasHit = true;
            return;
        }else if (other.CompareTag("DragonP2") && part.transform.parent.parent.CompareTag("Player1"))
        {
            minionHealth = other.GetComponent<MinionHealth>();
            minionHealth.setDamage(part.transform.parent.parent.GetComponent<PlayerHealth>().getPlayerDamage());
            minionHealth.wasHit = true;
            return;
        }

        if (other.CompareTag("TurretP1") && part.transform.parent.parent.CompareTag("Player2"))
        {
            towerHeath = other.GetComponent<TurretHealth>();
            towerHeath.TakeDamage(part.transform.parent.parent.GetComponent<PlayerHealth>().getPlayerDamage());
            return;
        }
        else if (other.CompareTag("TurretP2") && part.transform.parent.parent.CompareTag("Player1"))
        {
            towerHeath = other.GetComponent<TurretHealth>();
            towerHeath.TakeDamage(part.transform.parent.parent.GetComponent<PlayerHealth>().getPlayerDamage());
            return;
        }

        // ALTERAR DANO DAS TORRES BASEADO NO DANO BASICO DE CADA PLAYER
        if (other.CompareTag("Player1") && part.transform.parent.parent.CompareTag("Player2"))
        {
            player = other.GetComponent<PlayerHealth>();
            int player1Res = player.getResistence();
            int player2Damage = part.transform.parent.parent.GetComponent<PlayerHealth>().getPlayerDamage();
            Debug.Log("PLAYER DAMAGE: "+player2Damage);
            int totalDamage = (int)(player2Damage - (player2Damage * (player1Res/100.0f)));
            Debug.Log("PLAYER TOTAL DAMAGE: " + totalDamage);
            player.TakeDamage(totalDamage);
            bool lifeSteal = part.transform.parent.parent.GetComponent<PlayerPowers>().getLifeStealStatus();
            if (lifeSteal)
            {
                int percent = 35;
                part.transform.parent.parent.GetComponent<PlayerHealth>().addHealth((int)(totalDamage * (percent / 100.0f)));
            }
            return;

        }
        else if (other.CompareTag("Player2") && part.transform.parent.parent.CompareTag("Player1"))
        {
            
            player = other.GetComponent<PlayerHealth>();
            int player2Res = player.getResistence();
            int player1Damage = part.transform.parent.parent.GetComponent<PlayerHealth>().getPlayerDamage();
            Debug.Log("PLAYER DAMAGE: " + player1Damage);
            int totalDamage = (int)(player1Damage - (player1Damage * (player2Res / 100.0f)));
            Debug.Log("PLAYER TOTAL DAMAGE: " + totalDamage);
            player.TakeDamage(totalDamage);
            bool lifeSteal = part.transform.parent.parent.GetComponent<PlayerPowers>().getLifeStealStatus();
            if (lifeSteal)
            {
                int percent = 35;
                part.transform.parent.parent.GetComponent<PlayerHealth>().addHealth((int)(totalDamage * (percent / 100.0f)));
            }
            return;


        }

    }
}
