using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : StateMachineBehaviour
{
    private EnemyNPC enemy;
    private float timer = 0.7f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.gameObject.GetComponentInParent<EnemyNPC>();
        timer = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy.Agent.SetDestination(enemy.transform.position);   // stand in place when idle

        timer += Time.deltaTime;
        if (timer > enemy.IdleTime)
        {
            animator.SetBool("isPatrolling", true); // transition to patrol state
        }
        else if (enemy.GetDistanceFromPlayer() < enemy.ChaseRange)
        {
            animator.SetBool("isChasing", true);    // transition to chase state            
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
