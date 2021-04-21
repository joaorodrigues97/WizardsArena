using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerAttack1 : MonoBehaviourPunCallbacks
{

    public Transform FirePoint;
    private PhotonView PV;
    public GameObject ps;

    private Animator animation;

    Ray RayOrigin;
    Vector3 TargetDirection;
    private bool IsHit;
    private Vector3 HitPoint;

    public GameObject aim;



    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        animation = GetComponent<Animator>();
        aim = GameObject.FindGameObjectWithTag("Aim");
        IsHit = false;
    }

    void Update()
    {

        if (PV.IsMine)
        {
            IsHit = false;


            if (Input.GetMouseButtonDown(0))
            {
                //PV.RPC("RPC_Shoot", RpcTarget.All);
                if (!ps.GetComponent<ParticleSystem>().isPlaying)
                {
                    RayOrigin = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, (Screen.height / 2)+25, 0));
                    //RayOrigin = Camera.main.ScreenPointToRay(aim.transform.position);
                    PV.RPC("RPC_Shoot", RpcTarget.All, RayOrigin.origin, RayOrigin.direction);
                    animation.SetTrigger("Attack1");
                }

            }
            if (Input.GetKeyDown("4"))
            {
                animation.SetTrigger("Heal");
            }

            //O FAZENDA E GANDA PANELEIRO
        }
    }



    [PunRPC]
    void RPC_Shoot(Vector3 origin, Vector3 direction)
    {
        Ray ray = new Ray(origin, direction);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            if (!ps.GetComponent<ParticleSystem>().isPlaying)
            {
                HitPoint = hit.point;
            }
            TargetDirection = HitPoint - FirePoint.transform.position;
            ps.transform.rotation = Quaternion.LookRotation(TargetDirection);
            //ps.transform.position = new Vector3(FirePoint.transform.position.x, FirePoint.transform.position.y, FirePoint.transform.position.z);

            IsHit = true;
        }

        ps.GetComponent<ParticleSystem>().Play();

    }
}