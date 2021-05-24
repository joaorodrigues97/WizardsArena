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
        if (gameObject.tag.Equals("EarthShatter"))
        {
            Debug.Log("ACERTOU COM O HEARTHSHATTER: "+other);
            
            part.GetComponent<ParticleSystem>().Stop();
            return;
        }
        
        Debug.Log("Entrei no collision: "+other);
        if(other.CompareTag("DragonP1") && part.transform.parent.parent.CompareTag("Player2"))
        {
            minionHealth = other.GetComponent<MinionHealth>();
            minionHealth.setDamage(part.transform.parent.parent.GetComponent<PlayerHealth>().getPlayerDamage());
            minionHealth.wasHit = true;
        }else if (other.CompareTag("DragonP2") && part.transform.parent.parent.CompareTag("Player1"))
        {
            minionHealth = other.GetComponent<MinionHealth>();
            minionHealth.setDamage(part.transform.parent.parent.GetComponent<PlayerHealth>().getPlayerDamage());
            minionHealth.wasHit = true;
        }

        if (other.CompareTag("TurretP1") && part.transform.parent.parent.CompareTag("Player2"))
        {
            towerHeath = other.GetComponent<TurretHealth>();
            towerHeath.TakeDamage(part.transform.parent.parent.GetComponent<PlayerHealth>().getPlayerDamage());
        }else if (other.CompareTag("TurretP2") && part.transform.parent.parent.CompareTag("Player1"))
        {
            towerHeath = other.GetComponent<TurretHealth>();
            towerHeath.TakeDamage(part.transform.parent.parent.GetComponent<PlayerHealth>().getPlayerDamage());
        }

        // ALTERAR DANO DAS TORRES BASEADO NO DANO BASICO DE CADA PLAYER
        if (other.CompareTag("Player1") && part.transform.parent.parent.CompareTag("Player2"))
        {
            player = other.GetComponent<PlayerHealth>();
            int player1Res = player.getResistence();
            int player2Damage = part.transform.parent.parent.GetComponent<PlayerHealth>().getPlayerDamage();
            int totalDamage = player2Damage - (player2Damage * (player1Res/100));
            player.TakeDamage(totalDamage);
            Debug.Log("LIFE STEAL STATUS: "+ part.transform.parent.parent.GetComponent<PlayerPowers>().getLifeStealStatus());
            if (part.transform.parent.parent.GetComponent<PlayerPowers>().getLifeStealStatus())
            {
                Debug.Log("VIDA ANTES: "+ part.transform.parent.parent.GetComponent<PlayerHealth>().getCurrentHealth());
                int percent = 35;
                part.transform.parent.parent.GetComponent<PlayerPowers>().addHealth(totalDamage * (percent / 100));
                Debug.Log("FIZ LIFE STEAL");
                Debug.Log("VIDA DEPOIS: " + part.transform.parent.parent.GetComponent<PlayerHealth>().getCurrentHealth());
            }

        }
        else if (other.CompareTag("Player2") && part.transform.parent.parent.CompareTag("Player1"))
        {
            player = other.GetComponent<PlayerHealth>();
            int player2Res = player.getResistence();
            int player1Damage = part.transform.parent.parent.GetComponent<PlayerHealth>().getPlayerDamage();
            int totalDamage = player1Damage - (player1Damage * (player2Res / 100));
            player.TakeDamage(totalDamage);
            Debug.Log("LIFE STEAL STATUS: " + part.transform.parent.parent.GetComponent<PlayerPowers>().getLifeStealStatus());
            if (part.transform.parent.parent.GetComponent<PlayerPowers>().getLifeStealStatus())
            {
                Debug.Log("VIDA ANTES: " + part.transform.parent.parent.GetComponent<PlayerHealth>().getCurrentHealth());
                int percent = 35;
                part.transform.parent.parent.GetComponent<PlayerPowers>().addHealth(totalDamage * (percent / 100));
                Debug.Log("FIZ LIFE STEAL");
                Debug.Log("VIDA DEPOIS: " + part.transform.parent.parent.GetComponent<PlayerHealth>().getCurrentHealth());
            }
        }

    }
}
