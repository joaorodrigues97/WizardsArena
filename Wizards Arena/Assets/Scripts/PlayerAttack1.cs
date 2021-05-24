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
    public GameObject skill1;
    public GameObject skill1Continue;
    public GameObject skill2;
    public Button basicButton;
    public Button skill1Btn;
    public Button skill2Btn;

    private Animator animation;

    private PlayerPowers PP;

    Ray RayOrigin;
    Vector3 TargetDirection;
    private Vector3 HitPoint;

    public GameObject aim;


    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        PP = GetComponent<PlayerPowers>();
        animation = GetComponent<Animator>();
        aim = GameObject.FindGameObjectWithTag("Aim");
        basicButton = GameObject.FindGameObjectWithTag("BasicAttack").GetComponent<Button>();
        skill1Btn = GameObject.FindGameObjectWithTag("Skill1").GetComponent<Button>();
        skill2Btn = GameObject.FindGameObjectWithTag("Skill2").GetComponent<Button>();

        basicButton.onClick.AddListener(onClickBasic);
        skill1Btn.onClick.AddListener(onClickSkill1);
        skill2Btn.onClick.AddListener(onClickSkill2);
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

    public void onClickSkill1()
    {
        if (PV.IsMine)
        {
            //IR BUSCAR O ARRAY LIST PP DE PODERES
            //VERIFICAR QUAL A VERSAO DA SKILL 1, E ALTERAR A IMAGEM
            //APLICAR A FUNÇÃO COM A RESPETIVA % DA VERSAO EM QUE ESTEJA
            //PP.addHealth(x);

            PV.RPC("RPC_Skill1", RpcTarget.All);
            //animation.SetTrigger("Attack1"); 
        }


    }

    public void onClickSkill2()
    {
        if (PV.IsMine)
        {
            //IR BUSCAR O ARRAY LIST PP DE PODERES
            //VERIFICAR QUAL A VERSAO DA SKILL 1, E ALTERAR A IMAGEM
            

            PV.RPC("RPC_Skill2", RpcTarget.All);
            //animation.SetTrigger("Attack1");
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

    [PunRPC]
    void RPC_Skill1()
    {
        skill1.GetComponent<ParticleSystem>().Play();
        skill1Continue.GetComponent<ParticleSystem>().Play();
    }


    [PunRPC]
    void RPC_Skill2()
    {
        skill2.GetComponent<ParticleSystem>().Play();
    }
}