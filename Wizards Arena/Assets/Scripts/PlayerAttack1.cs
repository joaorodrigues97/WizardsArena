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
    public GameObject skill3;
    public GameObject skill2;
    public GameObject skill2Continue;
    public GameObject skill1;
    private Button basicButton;
    private Button skill1Btn;
    private Button skill2Btn;
    private Button skill3Btn;

    private Animator animation;

    private PlayerPowers PP;

    Ray RayOrigin;
    Vector3 TargetDirection;
    private Vector3 HitPoint;

    public GameObject aim;

    private AudioSettingsScene sfxAudio;

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

    private bool isCooldownSkill3;
    private float cooldownTimeSkill3;
    private float cooldownTimerSkill3;
    private Image imageCooldownSkill3;
    private TMP_Text textCooldownSkill3;

    private int hearthShakeDmg;

    // Start is called before the first frame update
    void Start()
    {
        sfxAudio = GameObject.FindGameObjectWithTag("SFX").GetComponent<AudioSettingsScene>();

        isCooldownBasic = false;
        cooldownTimeBasic = 1f;
        cooldownTimerBasic = 0f;

        isCooldownSkill1 = false;
        cooldownTimeSkill1 = 40f;
        cooldownTimerSkill1 = 0f;

        isCooldownSkill2 = false;
        cooldownTimeSkill2 = 40f;
        cooldownTimerSkill2 = 0f;

        isCooldownSkill3 = false;
        cooldownTimeSkill3 = 40f;
        cooldownTimerSkill3 = 0f;

        PV = GetComponent<PhotonView>();
        PP = GetComponent<PlayerPowers>();
        animation = GetComponent<Animator>();
        aim = GameObject.FindGameObjectWithTag("Aim");
        basicButton = GameObject.FindGameObjectWithTag("BasicAttack").GetComponent<Button>();
        skill1Btn = GameObject.FindGameObjectWithTag("Skill1").GetComponent<Button>();
        skill2Btn = GameObject.FindGameObjectWithTag("Skill2").GetComponent<Button>();
        skill3Btn = GameObject.FindGameObjectWithTag("Skill3").GetComponent<Button>();

        basicButton.onClick.AddListener(onClickBasic);
        skill1Btn.onClick.AddListener(onClickSkill1);
        skill2Btn.onClick.AddListener(onClickSkill2);
        skill3Btn.onClick.AddListener(onClickSkill3);

        imageCooldownBasic = GameObject.FindGameObjectWithTag("imageCoolBasic").GetComponent<Image>();
        textCooldownBasic = GameObject.FindGameObjectWithTag("textCoolBasic").GetComponent<TMP_Text>();

        imageCooldownSkill1 = GameObject.FindGameObjectWithTag("imageCoolSkill1").GetComponent<Image>();
        textCooldownSkill1 = GameObject.FindGameObjectWithTag("textCoolSkill1").GetComponent<TMP_Text>();

        imageCooldownSkill2 = GameObject.FindGameObjectWithTag("imageCoolSkill2").GetComponent<Image>();
        textCooldownSkill2 = GameObject.FindGameObjectWithTag("textCoolSkill2").GetComponent<TMP_Text>();

        imageCooldownSkill3 = GameObject.FindGameObjectWithTag("imageCoolSkill3").GetComponent<Image>();
        textCooldownSkill3 = GameObject.FindGameObjectWithTag("textCoolSkill3").GetComponent<TMP_Text>();

        textCooldownBasic.gameObject.SetActive(false);
        imageCooldownBasic.fillAmount = 0.0f;

        textCooldownSkill1.gameObject.SetActive(false);
        imageCooldownSkill1.fillAmount = 0.0f;

        textCooldownSkill2.gameObject.SetActive(false);
        imageCooldownSkill2.fillAmount = 0.0f;

        textCooldownSkill3.gameObject.SetActive(false);
        imageCooldownSkill3.fillAmount = 0.0f;

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
        if (isCooldownSkill3)
        {
            applyCooldownSkill3();
        }
    }

    public void setCooldown(int newCooldown)
    {
        cooldownTimeSkill1 = newCooldown;
        cooldownTimeSkill2 = newCooldown;
        cooldownTimeSkill3 = newCooldown;
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
        else if (button.Equals("Skill3"))
        {
            GameObject.FindGameObjectWithTag("Skill3").GetComponent<Image>().sprite = img;
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

    void applyCooldownSkill3()
    {
        cooldownTimerSkill3 -= Time.deltaTime;

        if (cooldownTimerSkill3 < 0.0f)
        {
            isCooldownSkill3 = false;
            textCooldownSkill3.gameObject.SetActive(false);
            imageCooldownSkill3.fillAmount = 0.0f;
            skill3Btn.interactable = true;
        }
        else
        {
            textCooldownSkill3.text = Mathf.RoundToInt(cooldownTimerSkill3).ToString();
            imageCooldownSkill3.fillAmount = cooldownTimerSkill3 / cooldownTimeSkill3;
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

            if (PP.playerInv["Skill1V1"] == true)
            {
                hearthShakeDmg = 40;
            }
            else if (PP.playerInv["Skill1V2"] == true)
            {
                hearthShakeDmg = 55;
            }
            else if (PP.playerInv["Skill1V3"] == true)
            {
                hearthShakeDmg = 70;
            }

            if (PP.playerInv["Skill1V1"] || PP.playerInv["Skill1V2"] || PP.playerInv["Skill1V3"])
            {
                sfxAudio.skill1Sound();
                animation.SetTrigger("Ground");
                PV.RPC("RPC_Skill1", RpcTarget.All);
                isCooldownSkill1 = true;
                textCooldownSkill1.gameObject.SetActive(true);
                cooldownTimerSkill1 = cooldownTimeSkill1;
                skill1Btn.interactable = false;
            }


        }


    }

    public int getHearthShakeDmg()
    {
        return hearthShakeDmg;
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

            int lifeAdd;

            if (PP.playerInv["Skill2V1"] == true)
            {
                lifeAdd = 100;
                sfxAudio.skill2Sound();
                animation.SetTrigger("Heal");
                PV.RPC("RPC_Skill2", RpcTarget.All);
                PH.addHealth(lifeAdd);
            }
            else if (PP.playerInv["Skill2V2"] == true)
            {
                lifeAdd = 180;
                sfxAudio.skill2Sound();
                animation.SetTrigger("Heal");
                PV.RPC("RPC_Skill2", RpcTarget.All);
                PH.addHealth(lifeAdd);
            }
            else if (PP.playerInv["Skill2V3"] == true)
            {
                lifeAdd = 250;
                sfxAudio.skill2Sound();
                animation.SetTrigger("Heal");
                PV.RPC("RPC_Skill2", RpcTarget.All);
                PH.addHealth(lifeAdd);
            }
            
            if (PP.playerInv["Skill2V1"] || PP.playerInv["Skill2V2"] || PP.playerInv["Skill2V3"]) {
                StartCoroutine(stopLevitate());
                isCooldownSkill2 = true;
                textCooldownSkill2.gameObject.SetActive(true);
                cooldownTimerSkill2 = cooldownTimeSkill2;
                skill2Btn.interactable = false;
            }
        }
    }


    public void onClickSkill3()
    {

        //IR BUSCAR O ARRAY LIST PP DE PODERES
        //VERIFICAR QUAL A VERSAO DA SKILL 1, E ALTERAR A IMAGEM
        //APLICAR A FUNÇÃO COM A RESPETIVA % DA VERSAO EM QUE ESTEJA
        //PP.addHealth(x);

        if (PhotonNetwork.LocalPlayer == PhotonNetwork.MasterClient)
        {
            PlayerAttack1 playerA = GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerAttack1>();
            PlayerHealth playerH = GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerHealth>();
            int lifeTaken;
            if (PP.playerInv["Skill3V1"] == true)
            {
                lifeTaken = 100;
                playerA.skillTrois();
                playerH.TakeDamageSkill3(lifeTaken);
            }
            else if (PP.playerInv["Skill3V2"] == true)
            {
                lifeTaken = 150;
                playerA.skillTrois();
                playerH.TakeDamageSkill3(lifeTaken);
            }
            else if (PP.playerInv["Skill3V3"] == true)
            {
                lifeTaken = 180;
                playerA.skillTrois();
                playerH.TakeDamageSkill3(lifeTaken);
            }
        }
        else
        {
            PlayerAttack1 playerA = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerAttack1>();
            PlayerHealth playerH = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerHealth>(); ;
            int lifeTaken;
            if (PP.playerInv["Skill3V1"] == true)
            {
                lifeTaken = 100;
                playerA.skillTrois();
                playerH.TakeDamageSkill3(lifeTaken);
            }
            else if (PP.playerInv["Skill3V2"] == true)
            {
                lifeTaken = 150;
                playerA.skillTrois();
                playerH.TakeDamageSkill3(lifeTaken);
            }
            else if (PP.playerInv["Skill3V3"] == true)
            {
                lifeTaken = 180;
                playerA.skillTrois();
                playerH.TakeDamageSkill3(lifeTaken);
            }
        }

        if(PP.playerInv["Skill3V1"] || PP.playerInv["Skill3V2"] || PP.playerInv["Skill3V3"])
        {
            sfxAudio.skill3Sound();
            animation.SetTrigger("Ulti");
            isCooldownSkill3 = true;
            textCooldownSkill3.gameObject.SetActive(true);
            cooldownTimerSkill3 = cooldownTimeSkill3;
            skill3Btn.interactable = false;
        }
    }

    public void skillTrois()
    {
        
            //audio.Play("Skill3");
            //animation.SetTrigger("Ground");
        PV.RPC("RPC_Skill3", RpcTarget.All);
        
    }


    IEnumerator stopLevitate()
    {
        yield return new WaitForSeconds(1.5f);
        animation.SetTrigger("HealLeave");
    }

    IEnumerator playBasic()
    {
        yield return new WaitForSeconds(.5f);
        sfxAudio.basicSound();
    }

    IEnumerator playSkill1()
    {
        yield return new WaitForSeconds(.7f);
        skill1.GetComponent<ParticleSystem>().Play();
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
        StartCoroutine(playBasic());
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
        StartCoroutine(playSkill1());
    }

    [PunRPC]
    void RPC_Skill3()
    {
        skill3.GetComponent<ParticleSystem>().Play();
    }
}