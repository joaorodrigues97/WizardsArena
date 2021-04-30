using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniDragonAttack : StateMachineBehaviour
{

    private MiniDragonMain dragon;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        dragon = animator.GetComponent<MiniDragonMain>();
        dragon.navMeshAgent.isStopped = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        /*if (dragon.minions.Length != 0)
        {
            dragon.gameObject.transform.rotation = Quaternion.LookRotation(dragon.minionFollow.transform.position - dragon.transform.position);
        }*/
        if (dragon.distanceToMinion <= 3 && dragon.minions.Length != 0)
        {
            dragon.gameObject.transform.rotation = Quaternion.LookRotation(dragon.minionFollow.transform.position - dragon.transform.position);
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        dragon.navMeshAgent.isStopped = false;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
