using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestMove : MonoBehaviour
{

    public float speed;
    [SerializeField] private FixedJoystick joystick;


    // Update is called once per frame
    private void Update()
    {
        float xMovement = joystick.Horizontal;
        float zMovement = joystick.Vertical;

        transform.position += new Vector3(xMovement, 0f, zMovement) * speed * Time.deltaTime;
    }
}
