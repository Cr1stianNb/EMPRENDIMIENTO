using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class habilidadBehaviour : StateMachineBehaviour
{
    [SerializeField]private GameObject habilidad;
    [SerializeField]private float offsety;
    private Jefe jefe;
    private Transform jugador;
    // OnStateEnter is called whenatransition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator,AnimatorStateInfo stateInfo,int layerIndex)
    {
        jefe = animator.GetComponent<Jefe>();
        jugador = jefe.farthest.transform;
        
        jefe.MirarJugador();
        Vector2 posicionAparicion=new Vector2(jugador.position.x,jugador.position.y+offsety);
        Instantiate(habilidad,posicionAparicion,Quaternion.identity);
    }
}
