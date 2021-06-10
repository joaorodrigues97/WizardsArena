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
        //Effect = PrefabsCast[0].GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();

    }


    // Update is called once per frame
    void Update()
    {
        screenTargets = GameObject.FindGameObjectsWithTag("DragonP2");

        if (PV.IsMine)
        {
            healthBar.SetActive(false);
            PlayerMoveAndRotation();
           
        }
    }

    void PlayerMoveAndRotation()
    {

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

        if (InputX != 0 || InputZ != 0)
        {
            if (InputZ < -0.5)
            {
                
                animation.SetBool("Run", true);
                controller.SimpleMove(desiredMoveDirection * Time.deltaTime * (velocity / 1.5f));

            }

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

}
