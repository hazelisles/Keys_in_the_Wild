using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : StateMachineBehaviour
{
    private EnemyNPC enemy;
    private float timeout = 0;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeout = 0;
        enemy = animator.gameObject.GetComponentInParent<EnemyNPC>();
        enemy.Agent.ResetPath();

        enemy.DetermineNextWaypoint();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeout += Time.deltaTime;
        if (timeout > enemy.PrizeRadius)
        {
            timeout = 0;
            enemy.DetermineNextWaypoint();
        }
        enemy.Agent.SetDestination(enemy.GetCurrentWaypoint()); // tell the agent where to go
        //enemy.transform.LookAt(enemy.GetCurrentWaypoint());
        // if reached a waypoint
        if (enemy.Agent.remainingDistance <= enemy.Agent.stoppingDistance)
        {
            Debug.Log("RD:" + enemy.Agent.remainingDistance + ", stopping distance:" + enemy.Agent.stoppingDistance);
            animator.SetBool("isPatrolling", false);    // transition to idle
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
