using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class jefe_CaminarBehaviour: StateMachineBehaviour{
    
    private GameObject target;
    private Jefe jefe;
    private GameObject boss;
    
    private NavMeshAgent agent;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
    {
        jefe = animator.GetComponent<Jefe>();
        agent = jefe.agent;
        boss = jefe.boss;

    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) 
    {
        jefe.MirarJugador();
        target = jefe.FindClosestEnemy();
        agent.isStopped = false;
        agent.SetDestination(target.transform.position);

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) 
    {
        // rb2D.velocity = new Vector2(0, rb2D.velocity.y);
        agent.isStopped = true;
    }
}