using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class MinionHealth : MonoBehaviourPunCallbacks, IPunObservable
{

    public int health = 100;
    public Slider healthDragon;
    public bool wasHit;
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
        SetHealth(health);
    }

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        wasHit = false;
        //timer = 0f;
        //waitTime = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (wasHit)
        {
            Debug.Log("ACERTEI");
            StartCoroutine(TakeDamageDragons(30));
            
            wasHit = false;
        }
    }

    public void SetHealth(int health)
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
        SetHealth(health);
        yield return new WaitForSeconds(1);
    }

    public void MinionDie()
    {
        if (PV.IsMine)
        {
            PhotonNetwork.Destroy(PV);
        }
        
        //Destroy(gameObject);
    }

}
