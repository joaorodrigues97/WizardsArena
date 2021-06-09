using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;

public class PlayerAttack1 : MonoBehaviourPunCallbacks
{

    public Transform FirePoint;
    private PhotonView PV;
    public GameObject ps;
    public GameObject skill2;
    public GameObject skill2Continue;
    public GameObject skill1;
    private Button basicButton;
    private Button skill1Btn;
    private Button skill2Btn;

    private Animator animation;

    private PlayerPowers PP;

    Ray RayOrigin;
    Vector3 TargetDirection;
    private Vector3 HitPoint;

    public GameObject aim;


    private Image imageCooldownBasic;
    private TMP_Text textCooldownBasic;
    private bool isCooldownBasic;
    private float cooldownTimeBasic;
    private float cooldownTimerBasic;

    private bool isCooldownSkill1;
    private float cooldownTimeSkill1;
    private float cooldownTimerSkill1;
    private Image imageCooldownSkill1;
    private TMP_Text textCooldownSkill1;

    private bool isCooldownSkill2;
    private float cooldownTimeSkill2;
    private float cooldownTimerSkill2;
    private Image imageCooldownSkill2;
    private TMP_Text textCooldownSkill2;

    // Start is called before the first frame update
    void Start()
    {
        isCooldownBasic = false;
        cooldownTimeBasic = 2f;
        cooldownTimerBasic = 0f;

        isCooldownSkill1 = false;
        cooldownTimeSkill1 = 40f;
        cooldownTimerSkill1 = 0f;

        isCooldownSkill2 = false;
        cooldownTimeSkill2 = 40f;
        cooldownTimerSkill2 = 0f;

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

        imageCooldownBasic = GameObject.FindGameObjectWithTag("imageCoolBasic").GetComponent<Image>();
        textCooldownBasic = GameObject.FindGameObjectWithTag("textCoolBasic").GetComponent<TMP_Text>();

        imageCooldownSkill1 = GameObject.FindGameObjectWithTag("imageCoolSkill1").GetComponent<Image>();
        textCooldownSkill1 = GameObject.FindGameObjectWithTag("textCoolSkill1").GetComponent<TMP_Text>();

        imageCooldownSkill2 = GameObject.FindGameObjectWithTag("imageCoolSkill2").GetComponent<Image>();
        textCooldownSkill2 = GameObject.FindGameObjectWithTag("textCoolSkill2").GetComponent<TMP_Text>();

        textCooldownBasic.gameObject.SetActive(false);
        imageCooldownBasic.fillAmount = 0.0f;

        textCooldownSkill1.gameObject.SetActive(false);
        imageCooldownSkill1.fillAmount = 0.0f;

        textCooldownSkill2.gameObject.SetActive(false);
        imageCooldownSkill2.fillAmount = 0.0f;
    }

    void Update()
    {
        if (isCooldownBasic)
        {
            applyCooldownBasic();
        }
        if (isCooldownSkill1)
        {
            applyCooldownSkill1();
        }
        if (isCooldownSkill2)
        {
            applyCooldownSkill2();
        }
    }

    public void setCooldown(int newCooldown)
    {
        cooldownTimeSkill1 = newCooldown;
        cooldownTimeSkill2 = newCooldown;
    }

    public void changeImg(Sprite img, string button)
    {
        if (button.Equals("Skill1"))
        {
            GameObject.FindGameObjectWithTag("Skill1").GetComponent<Image>().sprite = img;
        }
        else if (button.Equals("Skill2"))
        {
            GameObject.FindGameObjectWithTag("Skill2").GetComponent<Image>().sprite = img;
        }
    }

    void applyCooldownBasic()
    {
        cooldownTimerBasic -= Time.deltaTime;

        if (cooldownTimerBasic < 0.0f)
        {
            isCooldownBasic = false;
            textCooldownBasic.gameObject.SetActive(false);
            imageCooldownBasic.fillAmount = 0.0f;
            basicButton.interactable = true;
        }
        else
        {
            textCooldownBasic.text = Mathf.RoundToInt(cooldownTimerBasic).ToString();
            imageCooldownBasic.fillAmount = cooldownTimerBasic / cooldownTimeBasic;
        }
    }

    void applyCooldownSkill1()
    {
        cooldownTimerSkill1 -= Time.deltaTime;

        if (cooldownTimerSkill1 < 0.0f)
        {
            isCooldownSkill1 = false;
            textCooldownSkill1.gameObject.SetActive(false);
            imageCooldownSkill1.fillAmount = 0.0f;
            skill1Btn.interactable = true;
        }
        else
        {
            textCooldownSkill1.text = Mathf.RoundToInt(cooldownTimerSkill1).ToString();
            imageCooldownSkill1.fillAmount = cooldownTimerSkill1 / cooldownTimeSkill1;
        }
    }

    void applyCooldownSkill2()
    {
        cooldownTimerSkill2 -= Time.deltaTime;

        if (cooldownTimerSkill2 < 0.0f)
        {
            isCooldownSkill2 = false;
            textCooldownSkill2.gameObject.SetActive(false);
            imageCooldownSkill2.fillAmount = 0.0f;
            skill2Btn.interactable = true;
        }
        else
        {
            textCooldownSkill2.text = Mathf.RoundToInt(cooldownTimerSkill2).ToString();
            imageCooldownSkill2.fillAmount = cooldownTimerSkill2 / cooldownTimeSkill2;
        }
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
                    isCooldownBasic = true;
                    textCooldownBasic.gameObject.SetActive(true);
                    cooldownTimerBasic = cooldownTimeBasic;
                    basicButton.interactable = false;
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


            //animation.SetTrigger("Attack1");
            PV.RPC("RPC_Skill1", RpcTarget.All);
            isCooldownSkill1 = true;
            textCooldownSkill1.gameObject.SetActive(true);
            cooldownTimerSkill1 = cooldownTimeSkill1;
            skill1Btn.interactable = false;

        }


    }

    public void onClickSkill2()
    {
        if (PV.IsMine)
        {
            //IR BUSCAR O ARRAY LIST PP DE PODERES
            //VERIFICAR QUAL A VERSAO DA SKILL 1, E ALTERAR A IMAGEM



            //animation.SetTrigger("Attack1");
            //PlayerPowers PP = GetComponent<PlayerPowers>();
            PlayerHealth PH = GetComponent<PlayerHealth>();
            int health = PH.getCurrentHealth();
            
            int percent;

            if (PP.playerInv["Skill2V1"] == true)
            {
                percent = 5;
                PV.RPC("RPC_Skill2", RpcTarget.All);
                PH.addHealth((int)(health * (percent / 100.0f)));
            }
            else if (PP.playerInv["Skill2V2"] == true)
            {
                percent = 10;
                PV.RPC("RPC_Skill2", RpcTarget.All);
                PH.addHealth((int)(health * (percent / 100.0f)));
            }
            else if (PP.playerInv["Skill2V3"] == true)
            {
                percent = 15;
                PV.RPC("RPC_Skill2", RpcTarget.All);
                PH.addHealth((int)(health * (percent / 100.0f)));
            }

            isCooldownSkill2 = true;
            textCooldownSkill2.gameObject.SetActive(true);
            cooldownTimerSkill2 = cooldownTimeSkill2;
            skill2Btn.interactable = false;
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
    void RPC_Skill2()
    {
        skill2.GetComponent<ParticleSystem>().Play();
        skill2Continue.GetComponent<ParticleSystem>().Play();
    }


    [PunRPC]
    void RPC_Skill1()
    {
        skill1.GetComponent<ParticleSystem>().Play();
    }
}