using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerAttack2 : MonoBehaviourPunCallbacks
{

    public Transform FirePoint;
    private PhotonView PV;
    public GameObject ps;

    private Animator animation;

    Ray RayOrigin;
    Vector3 TargetDirection;
    private bool IsHit;
    private Vector3 HitPoint;


    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        animation = GetComponent<Animator>();
        IsHit = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine)
        {

            IsHit = false;
            
            RayOrigin = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            if (Physics.Raycast(RayOrigin, out RaycastHit hit, 100f))
            {
                if (!ps.GetComponent<ParticleSystem>().isPlaying)
                {
                    HitPoint = hit.point;
                }
                TargetDirection = HitPoint - FirePoint.transform.position;
                //ps.transform.position = new Vector3(FirePoint.transform.position.x - 1, FirePoint.transform.position.y, FirePoint.transform.position.z);
                
                IsHit = true;
            }
                
            
            
            if (Input.GetMouseButtonDown(0))
            {
                //PV.RPC("RPC_Shoot", RpcTarget.All);
                if (IsHit && !ps.GetComponent<ParticleSystem>().isPlaying)
                {
                    PV.RPC("RPC_Shoot", RpcTarget.All);
                    animation.SetTrigger("Attack1");
                }
            
            }
            if (Input.GetKeyDown("4"))
            {
                animation.SetTrigger("Heal");
            }
            
        }
    }

    [PunRPC]
    void RPC_Shoot()
    {

        ps.transform.rotation = Quaternion.LookRotation(TargetDirection);
        ps.GetComponent<ParticleSystem>().Play();
        
        
    }

}
