using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    public float rotationSpeed = 1500f;
    public float deadZoneDegrees = 15f;


    private Transform cameraT;
    private Vector3 cameraDirection;
    private Vector3 playerDirection;
    private Quaternion targetRotation;


    // Start is called before the first frame update
    void Start()
    {
        cameraT = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();

    }

    // Update is called once per frame

    private void Update()
    {

        cameraDirection = new Vector3(cameraT.forward.x, 0f, cameraT.forward.z);
        playerDirection = new Vector3(transform.forward.x, 0f, transform.forward.z);

        if (Vector3.Angle(cameraDirection, playerDirection) > deadZoneDegrees)
        {
            targetRotation = Quaternion.LookRotation(cameraDirection, transform.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

    }
}
