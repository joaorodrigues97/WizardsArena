using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;
    [SerializeField] private FixedJoystick joystick;

    public Vector3 _cameraOffset;
    [Range(0.01f, 1.0f)]

    public float smoothFactor = 0.5f;
    
    public float Yaxis;
    public float Xaxis;
    public float rotationSense = 8f;

    public float x;
    public float y;
    public float z;


    private void Start()
    {
        joystick = GameObject.Find("Fixed Joystick Rotate").GetComponent<FixedJoystick>();
        target = GameObject.Find("Sphere").transform;
        //_cameraOffset = new Vector3(target.position.x, target.position.y + 8.0f, target.position.z + 7.0f);
    }

    
    private void Awake()
    {
        SetCameraTarget(target); 
    }

    public void SetCameraTarget(Transform playerTransform)
    {
        target = playerTransform;
        
    }

    private void Update()
    {




        /*Yaxis += joystick.Horizontal * rotationSense;
        Xaxis -= joystick.Vertical * rotationSense;

        Vector3 targetRotation = new Vector3(Xaxis, Yaxis);
        transform.eulerAngles = targetRotation;*/

        _cameraOffset = Quaternion.AngleAxis(joystick.Horizontal * rotationSense, Vector3.up) * _cameraOffset;
        _cameraOffset = Quaternion.AngleAxis(joystick.Vertical * rotationSense, Vector3.right) * _cameraOffset;
        transform.position = target.position + _cameraOffset;
         
        transform.LookAt(target.position);
        //transform.LookAt(new Vector3(target.position.x + x, target.position.y + y, target.position.z + z));


    }

    private void LateUpdate()
    {
        Vector3 newPos = target.position + _cameraOffset;
        transform.position = Vector3.Slerp(transform.position,newPos,smoothFactor);
    }


}
