using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using System;
using UnityEngine;

public class HS_CameraController : MonoBehaviour
{
    //camera holder
    public Transform Holder;
    public Vector3 cameraPos = new Vector3(0, 0, 0);
    public float currDistance = 5.0f;
    public float xRotate = 250.0f;
    public float yRotate = 120.0f;
    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;
    public float prevDistance;
    private float x = 0.0f;
    private float y = 0.0f;

    private float sensiX = 0.007f;
    private float sensiY = 0.007f;

    [SerializeField] private FixedJoystick joystick;

    //For camera colliding
    RaycastHit hit;
    public LayerMask collidingLayers = ~0; //Target marker can only collide with scene layer
    private float distanceHit;

    void Start()
    {
        var angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        joystick = GameObject.Find("Fixed Joystick Rotate").GetComponent<FixedJoystick>();
    }

    private void Awake()
    {
        SetCameraTarget(Holder);
    }

    public void SetCameraTarget(Transform playerTransform)
    {
        Holder = playerTransform;

    }

    void LateUpdate()
    {
        if (currDistance < 2)
        {
            currDistance = 2;
        }
        
        // (currDistance - 2) / 3.5f - constant for far camera position
        var targetPos = Holder.position + new Vector3(0, (distanceHit - 2) / 3f + cameraPos[1], 0);

        //currDistance -= Input.GetAxis("Mouse ScrollWheel") * 2;
        if (Holder)
        {
            var pos = Input.mousePosition;
            float dpiScale = 1;
            if (Screen.dpi < 1) dpiScale = 1;
            if (Screen.dpi < 200) dpiScale = 1;
            else dpiScale = Screen.dpi / 1000f;
            if (pos.x < 380 * dpiScale && Screen.height - pos.y < 250 * dpiScale) return;
            x += (float)(joystick.Horizontal * xRotate * sensiX);
            y -= (float)(joystick.Vertical * yRotate * sensiY);
            y = ClampAngle(y, -10f, 20f);
            var rotation = Quaternion.Euler(y, x, 0);
            var position = rotation * new Vector3(0, 0, -currDistance) + targetPos;
            //If camera collide with collidingLayers move it to this point.
            if (Physics.Raycast(targetPos, position - targetPos, out hit, (position - targetPos).magnitude, collidingLayers))
            {
                transform.position = hit.point;
                //Min(4) distance from ground for camera target point
                distanceHit = Mathf.Clamp(Vector3.Distance(targetPos, hit.point), 4, 600);

            }
            else
            {
                transform.position = position;
                distanceHit = currDistance;
            }
            transform.rotation = rotation;
        }
        else
        {
            //Cursor.visible = true;
            //Cursor.lockState = CursorLockMode.None;
        }

        /*if (prevDistance != currDistance)
        {
            prevDistance = currDistance;
            var rot = Quaternion.Euler(y, x, 0);
            // (currDistance - 2) / 3.5f - constant for far camera position
            var po = rot * new Vector3(0, 0, -currDistance) + targetPos;
            transform.rotation = rot;
            transform.position = po;
        }*/
    }

    static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
        {
            angle += 360;
        }
        if (angle > 360)
        {
            angle -= 360;
        }
        return Mathf.Clamp(angle, min, max);
    }
}