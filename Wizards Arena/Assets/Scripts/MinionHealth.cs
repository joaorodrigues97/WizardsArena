using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BehaviorDesigner.Runtime;
using Photon.Pun;

public class MinionHealth : MonoBehaviourPunCallbacks, IPunObservable
{

    private int health = 100;
    public Slider healthDragon;
    public bool wasHit;
    public BehaviorTree dragonTree;
    private int minionDamage;
    //private float timer;
    //private float waitTime;


    private PhotonView PV;
    

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(health);
        }
        else
        {
            health = (int)stream.ReceiveNext();
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        SetHealthBar(health);
    }

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        dragonTree = GetComponent<BehaviorTree>();
        wasHit = false;
        //timer = 0f;
        //waitTime = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        var myIntVariable = (SharedInt)dragonTree.GetVariable("health");
        myIntVariable.Value = health;
        var target = (SharedGameObject)dragonTree.GetVariable("TargetTurret");
        

        if (wasHit)
        {
            StartCoroutine(TakeDamageDragons(minionDamage));
            
            wasHit = false;
        }
    }

    public void SetHealthBar(int health)
    {
        healthDragon.value = health;
    }

    public void SetMaxHealth(int health)
    {
        healthDragon.maxValue = health;
        healthDragon.value = health;
    }

    IEnumerator TakeDamageDragons(int damage)
    {
        health -= damage;
        SetHealthBar(health);
        yield return new WaitForSeconds(1);
    }

    public void MinionDie()
    {
        if (PV.IsMine)
        {
            PhotonNetwork.Destroy(PV);
        }
        
    }

    public void setDamage(int damage)
    {
        minionDamage = damage;
    }


    public int getMinionHealth()
    {
        return health;
    }

    public void setMinionHealth(int healthMinion)
    {
        health = healthMinion;
    }
}
