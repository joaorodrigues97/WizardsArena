using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionController : MonoBehaviour
{
    //private CharacterController controller = null;

    public Transform[] waypoints;
    public int speed;

    private int waypointIndex;
    private float dist;

    void Start()
    {
        //controller = GetComponent<CharacterController>();
        waypointIndex = 0;
        transform.LookAt(waypoints[waypointIndex].position);
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(transform.position, waypoints[waypointIndex].position);
        if (dist < 1f)
        {
            IncreaseIndex();
        }
        Patrol();

        //controller.Move(new Vector3(-1,0,0) * 5 * Time.deltaTime);
    }

    void Patrol()
    {
        //controller.SimpleMove(Vector3.forward * speed * Time.deltaTime);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void IncreaseIndex()
    {
        waypointIndex++;
        if (waypointIndex >= waypoints.Length)
        {
            waypointIndex = 0;
        }
        transform.LookAt(waypoints[waypointIndex].position);
    }
}
