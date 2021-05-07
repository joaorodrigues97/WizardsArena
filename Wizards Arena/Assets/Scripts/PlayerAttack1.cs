using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PlayerAttack1 : MonoBehaviourPunCallbacks
{

    public Transform FirePoint;
    private PhotonView PV;
    public GameObject ps;
    public Button basicButton;

    private Animator animation;

    Ray RayOrigin;
    Vector3 TargetDirection;
    private Vector3 HitPoint;

    public GameObject aim;



    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        animation = GetComponent<Animator>();
        aim = GameObject.FindGameObjectWithTag("Aim");
        basicButton = GameObject.FindGameObjectWithTag("BasicAttack").GetComponent<Button>();
    }

    void Update()
    {

        
        basicButton.onClick.AddListener(onClickBasic);

        

    }


    public void onClickBasic()
    {
        if (PV.IsMine)
        {
            if (!animation.GetCurrentAnimatorStateInfo(0).IsName("Run"))
            {
                if (!ps.GetComponent<ParticleSystem>().isPlaying)
                {
                    RayOrigin = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, (Screen.height / 2) + 25, 0));
                    //RayOrigin = Camera.main.ScreenPointToRay(aim.transform.position);
                    PV.RPC("RPC_Shoot", RpcTarget.All, RayOrigin.origin, RayOrigin.direction);
                    animation.SetTrigger("Attack1");
                }
            }
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

        }

        ps.GetComponent<ParticleSystem>().Play();

    }
}