/*This script created by using docs.unity3d.com/ScriptReference/MonoBehaviour.OnParticleCollision.html*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParticleCollisionInstance : MonoBehaviour
{
    public GameObject[] EffectsOnCollision;
    public float Offset = 0;
    public float DestroyTimeDelay = 5;
    public bool UseWorldSpacePosition;
    public bool UseFirePointRotation;
    private ParticleSystem part;
    private MinionHealth minionHealth;
    private TowerHeath towerHeath;
    

    void Start()
    {
        part = GetComponent<ParticleSystem>();
        
    }
    void OnParticleCollision(GameObject other)
    {
        Debug.Log("Entrei no collision: "+other);
        if(other.CompareTag("DragonP1") && part.transform.parent.parent.CompareTag("Player2"))
        {
            minionHealth = other.GetComponent<MinionHealth>();
            minionHealth.TakeDamage(30);
        }else if (other.CompareTag("DragonP2") && part.transform.parent.parent.CompareTag("Player1"))
        {
            minionHealth = other.GetComponent<MinionHealth>();
            minionHealth.TakeDamage(30);
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
