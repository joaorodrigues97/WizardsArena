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
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
    private ParticleSystem ps;
    private MinionHealth minionHealth;
    private TowerHeath towerHeath;
    private PlayerAttack1 playerAttack1;
    

    void Start()
    {
        part = GetComponent<ParticleSystem>();
        
    }
    void OnParticleCollision(GameObject other)
    {
        Debug.Log("Entrei no collision: "+other);
        if(other.CompareTag("DragonP1") || other.CompareTag("DragonP2"))
        {
            minionHealth = other.GetComponent<MinionHealth>();
            minionHealth.TakeDamage(30);
        }
        if (other.CompareTag("TurretP1") || other.CompareTag("TurretP2"))
        {
            towerHeath = other.GetComponent<TowerHeath>();
            towerHeath.TakeDamage(30);
        }

       
    }
}
