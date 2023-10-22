using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Jefe_IdleBehaviour:StateMachineBehaviour
{
    [SerializeField] private int max;
    // OnStateEnter is called whenatransition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator,AnimatorStateInfo stateInfo,int layerIndex)
    {
         animator.SetInteger("numeroAleatorio",Random.Range(0,max));
    }
}