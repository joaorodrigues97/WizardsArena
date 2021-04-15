using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviourPunCallbacks
{
    [SerializeField] private float speed = 0f;
    [SerializeField] private FixedJoystick joystick;
    [SerializeField] private GameObject cam;
    private Vector3 moveDirection = Vector3.zero;
    private GameObject projectile;

    private PhotonView PV;

    private CharacterController controller = null;

    private Transform target;
    public GameObject[] screenTargets;
    public Vector2 uiOffset;
    public Image aim;
    private bool activeTarget = false;
    public LayerMask collidingLayer = ~0;
    public Vector3 desiredMoveDirection;
    public float InputX;
    public float InputZ;
    public bool blockRotationPlayer;
    public float desiredRotationSpeed = 0.1f;
    public float velocity = 9;

    public GameObject[] PrefabsCast;
    private ParticleSystem currEffect;
    private ParticleSystem Effect;
    public GameObject[] Prefabs;
    private float secondLayerWeight = 0;
    private float fireCountdown = 0f;
    public float fireRate = 0.1f;
    public Transform FirePoint;

    public List<ParticleCollisionEvent> collisionEvents;

    [SerializeField] private GameObject particle = null;

    public GameObject EffectInit;
    public GameObject healthBar;


    private Animator animation;


    private void Start()
    {
        PV = GetComponent<PhotonView>();
        controller = GetComponent<CharacterController>();
        joystick = GameObject.Find("Fixed Joystick").GetComponent<FixedJoystick>();
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        animation = GetComponent<Animator>();
        aim = GameObject.FindGameObjectWithTag("Aim").GetComponent<Image>();
        Effect = PrefabsCast[0].GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();

    }


    // Update is called once per frame
    void Update()
    {
        screenTargets = GameObject.FindGameObjectsWithTag("DragonP2");

        if (PV.IsMine)
        {
            //target = screenTargets[targetIndex()].transform;
            healthBar.SetActive(false);
            PlayerMoveAndRotation();

            //TakeInput();
            if (Input.GetMouseButtonDown(0))
            {
                
                //animation.SetTrigger("Attack1");
                //PV.RPC("RPC_Shoot", RpcTarget.All);
                
            }
            if (Input.GetKeyDown("4"))
            {
                
                animation.SetTrigger("Heal");
                //PV.RPC("RPC_Heal", RpcTarget.All);
                   
            }
           
        }
    }

    void PlayerMoveAndRotation()
    {
        //InputX = Input.GetAxis("Horizontal");
        //InputZ = Input.GetAxis("Vertical");

        InputX = joystick.Horizontal;
        InputZ = joystick.Vertical;

        var camera = Camera.main;
        var forward = cam.transform.forward;
        var right = cam.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        //Movement vector
        desiredMoveDirection = forward * InputZ + right * InputX;

        //Character diagonal movement faster fix
        desiredMoveDirection.Normalize();

       
        //You can use desiredMoveDirection if using InputMagnitude instead of Horizontal&Vertical axis
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(forward), desiredRotationSpeed);
        //Limit back speed

        if (InputX != 0 || InputZ != 0)
        {
            if (InputZ < -0.5)
            {
                
                animation.SetBool("Run", true);
                controller.SimpleMove(desiredMoveDirection * Time.deltaTime * (velocity / 1.5f));

            }

            //else if (InputX < -0.1 || InputX > 0.1)
            //    controller.Move(desiredMoveDirection * Time.deltaTime * (velocity / 1.2f));
            else
            {
                
                animation.SetBool("Run", true);
                controller.SimpleMove(desiredMoveDirection * Time.deltaTime * velocity);

            }
        }
        else
        {
            animation.SetBool("Run", false);
        }
        
    }

    /*private void TakeInput()
    {
        

        moveDirection = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        moveDirection = cam.transform.TransformDirection(moveDirection);
        moveDirection *= speed;
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Camera.main.transform.localEulerAngles.y, transform.localEulerAngles.z);
        //controller.Move(movement * speed * Time.deltaTime);
        //controller.SimpleMove(movement * speed * Time.deltaTime);
        controller.SimpleMove(moveDirection * Time.deltaTime);
    }*/

    

    public int targetIndex()
    {
        float[] distances = new float[screenTargets.Length];

        for (int i = 0; i < screenTargets.Length; i++)
        {
            distances[i] = Vector2.Distance(Camera.main.WorldToScreenPoint(screenTargets[i].transform.position), new Vector2(Screen.width / 2, Screen.height / 2));
        }

        float minDistance = Mathf.Min(distances);
        int index = 0;

        for (int i = 0; i < distances.Length; i++)
        {
            if (minDistance == distances[i])
                index = i;
        }
        
        return index;
    }

    public IEnumerator playAnimationBasic()
    {
        yield return new WaitForSeconds(0.4f);

        
        
        

    }

    public IEnumerator playAnimationHeal()
    {
        yield return new WaitForSeconds(0.4f);

        

    }

    /*public void Hit(GameObject other)
    {

        int numCollisionEvents = Effect.GetCollisionEvents(other, collisionEvents);

        //var instance = Instantiate(effect, collisionEvents[i].intersection + collisionEvents[i].normal * Offset, new Quaternion()) as GameObject;
        for (int i = 0; i < numCollisionEvents; i++)
        {
            Prefabs[0].transform.position = collisionEvents[i].intersection;
            Prefabs[0].transform.rotation = new Quaternion();
            PV.RPC("RPC_Hit", RpcTarget.All);
        }
        

        

        


    }

    /*[PunRPC]
    void RPC_Shoot()
    {



        EffectInit = PrefabsCast[0];

        EffectInit.transform.position = FirePoint.transform.position;
        EffectInit.transform.rotation = cam.transform.rotation;

        

        Effect.Play();

       


    }
    [PunRPC]
    void RPC_Heal()
    {
        Effect = Prefabs[1].GetComponent<ParticleSystem>();
        Effect.Play();

        PrefabsCast[1].transform.parent = null;
        currEffect = PrefabsCast[1].GetComponent<ParticleSystem>();
        currEffect.Play();
    }

    [PunRPC]
    void RPC_Hit()
    {
        Prefabs[0].GetComponent<ParticleSystem>().Play();
    }*/
}
