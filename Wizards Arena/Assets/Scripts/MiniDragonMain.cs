using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class MiniDragonMain : MonoBehaviourPunCallbacks
{
    public GameObject player;
    public GameObject[] minions;
    public GameObject[] turrets;
    public GameObject[] waypoints;
    public int currentTarget = 0;
    public NavMeshAgent navMeshAgent;

    public GameObject minionFollow;
    public GameObject turretFollow;

    private Animator animator;
    private float distanceToPlayer;
    public float distanceToMinion;
    public float distanceToTurret;



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        
        if (gameObject.CompareTag("DragonP1"))
        {
            waypoints = GameObject.FindGameObjectsWithTag("WayPointMin1");
        }
        else if (gameObject.CompareTag("DragonP2"))
        {
            waypoints = GameObject.FindGameObjectsWithTag("WayPointMin2");
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.CompareTag("DragonP1"))
        {
            minions = GameObject.FindGameObjectsWithTag("DragonP2");
            turrets = GameObject.FindGameObjectsWithTag("TurretP2");
        }else if (gameObject.CompareTag("DragonP2"))
        {
            minions = GameObject.FindGameObjectsWithTag("DragonP1");
            turrets = GameObject.FindGameObjectsWithTag("TurretP1");
        }



        // distanceToPlayer = (transform.position - player.transform.position).magnitude;
        //animator.SetFloat("distanceToPlayer", distanceToPlayer);
        if (minions.Length != 0)
        {
            minionFollow = GetClosestEnemy(minions);
            distanceToMinion = (transform.position - minionFollow.transform.position).magnitude;
            animator.SetFloat("distanceToMinion", distanceToMinion);
        }
        else
        {
            animator.SetFloat("distanceToMinion",100);
        }

        if (turrets.Length != 0)
        {
            turretFollow = GetClosestEnemy(turrets);
            distanceToTurret = (transform.position - turretFollow.transform.position).magnitude;
            animator.SetFloat("distanceToTurret", distanceToTurret);
        }
        else
        {
            animator.SetFloat("distanceToTurret", 100);
        }



    }

    public void MoveToNextWayPoint()
    {
        currentTarget = (currentTarget + 1) % waypoints.Length;
        navMeshAgent.SetDestination(waypoints[currentTarget].transform.position);
    }

    GameObject GetClosestEnemy(GameObject[] enemies)
    {
        GameObject bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (GameObject potentialTarget in enemies)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }

        return bestTarget;
    }


    public void enemyDamage()
    {
        if (minions.Length != 0)
        {
            minionFollow.GetComponent<MinionHealth>().TakeDamage(5);
        }
    }
}
