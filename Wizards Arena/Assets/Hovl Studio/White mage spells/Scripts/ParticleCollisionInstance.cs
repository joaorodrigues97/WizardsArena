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
    private TowerHeath towerHeath;
    public bool canShoot;
    

    void Start()
    {
        part = GetComponent<ParticleSystem>();
        canShoot = false;
        
    }
    void OnParticleCollision(GameObject other)
    {
        
        Debug.Log("Entrei no collision: "+other);
        if(other.CompareTag("DragonP1") && part.transform.parent.parent.CompareTag("Player2"))
        {
            minionHealth = other.GetComponent<MinionHealth>();
            //minionHealth.TakeDamage(30);
            minionHealth.wasHit = true;
        }else if (other.CompareTag("DragonP2") && part.transform.parent.parent.CompareTag("Player1"))
        {
            minionHealth = other.GetComponent<MinionHealth>();
            //minionHealth.TakeDamage(30);
            minionHealth.wasHit = true;
        }

        if (other.CompareTag("TurretP1") && part.transform.parent.parent.CompareTag("Player2"))
        {
            towerHeath = other.GetComponent<TowerHeath>();
            towerHeath.TakeDamage(30);
        }else if (other.CompareTag("TurretP2") && part.transform.parent.parent.CompareTag("Player1"))
        {
            towerHeath = other.GetComponent<TowerHeath>();
            towerHeath.TakeDamage(30);
        }
        
    }
}
