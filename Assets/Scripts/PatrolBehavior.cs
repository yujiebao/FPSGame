using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PatrolBehavior : StateMachineBehaviour
{
    private float timer;
    private List<Transform> wayPoints = new List<Transform>();
    private NavMeshAgent agent;
    private float ChaseRange = 10f;
    private Transform Player;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      //  Transform wayPoint = GameObject.FindGameObjectWithTag("WayPoint").transform;
       timer = 0;
       Transform wayPoint1 = GameObject.FindWithTag("WayPoints").transform;
       foreach(Transform wayPoint in wayPoint1)
       {
         wayPoints.Add(wayPoint);
       }
       agent = animator.GetComponent<NavMeshAgent>(); 
       agent.isStopped = false;
      //  agent.SetDestination(wayPoints[0].position);
       agent.SetDestination(wayPoints[Random.Range(0,wayPoints.Count)].position);
       Player = GameObject.FindWithTag("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

      if(agent.remainingDistance < agent.stoppingDistance)  //agent.remainingDistance到目标点的剩余距离   agent.stoppingDistance停止距离
      {
        agent.SetDestination(wayPoints[Random.Range(0,wayPoints.Count)].position);
      }
       timer += Time.deltaTime;
       if(timer > 10)
       {
       animator.SetBool("IsPatrol",false);
       }

       float distance = Vector3.Distance(animator.transform.position,Player.position);
       if(distance < ChaseRange)
       {
        animator.SetBool("IsChasing",true);
       }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      agent.SetDestination(animator.transform.position);
      // agent.destination = animator.transform.position;
      //  agent.isStopped = true;
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
