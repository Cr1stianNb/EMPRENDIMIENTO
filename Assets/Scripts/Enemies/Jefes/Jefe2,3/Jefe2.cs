using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Jefe2:MonoBehaviour
{
    public GameObject target;
    Vector3 enemyToPlayer;
    private Animator animator;
    public GameObject jugador;
    public GameObject farthest;
    public NavMeshAgent agent;
    public GameObject boss;
    [Header("Ataque")]
    [SerializeField] private Transform controladorAtaque;
    [SerializeField] private float radioAtaque;
    [SerializeField] private float danoAtaque;

    private void Start()
    {
        Doors.cantEnemy += 1;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        animator = GetComponent<Animator>();
        jugador = FindClosestEnemy();
        farthest = FindFarthestEnemy();
    }

    private void Update()
    {
    jugador = FindClosestEnemy();
    farthest = FindFarthestEnemy();
    float distanciaJugador=Vector2.Distance(transform.position,jugador.transform.position);
    animator.SetFloat("distanciaJugador",distanciaJugador);
    Animation();
    }

    private void Animation()
    {
        target = FindClosestEnemy();
        agent.SetDestination(target.transform.position);
        enemyToPlayer = target.transform.position - boss.transform.position;
        if (Mathf.Abs(enemyToPlayer.x) > Mathf.Abs(enemyToPlayer.y))
        {
            animator.SetBool("isWalkingUp", false);
            animator.SetBool("isWalkingDown", false);
            if (enemyToPlayer.x > 0)
            {
                animator.SetBool("isWalkingRight", true);
                animator.SetBool("isWalkingLeft", false);
                animator.SetInteger("Direction", 2);
            }
            else
            {
                animator.SetBool("isWalkingLeft", true);
                animator.SetBool("isWalkingRight", false);
                animator.SetInteger("Direction", 4);
            }
        }
        else if (Mathf.Abs(enemyToPlayer.y) > Mathf.Abs(enemyToPlayer.x))
        {
            animator.SetBool("isWalkingLeft", false);
            animator.SetBool("isWalkingRight", false);
            if (enemyToPlayer.y > 0)
            {
                animator.SetBool("isWalkingUp", true);
                animator.SetBool("isWalkingDown", false);
                animator.SetInteger("Direction", 1);
            }
            else
            {
                animator.SetBool("isWalkingUp", false);
                animator.SetBool("isWalkingDown", true);
                animator.SetInteger("Direction", 3);
            }

        }
    }

    public void Ataque()
    {
        Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorAtaque.position, radioAtaque);
        foreach(Collider2D colision in objetos)
        {
            if(colision.CompareTag("Player"))
            {
                colision.GetComponent<PlayerHealth>().UpdateHealth(-danoAtaque);
            }
        }
    }

   public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Player");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = boss.transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    public GameObject FindFarthestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Player");
        GameObject farthest = null;
        float distance = 0;
        Vector3 position = boss.transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance > distance)
            {
                farthest = go;
                distance = curDistance;
            }
        }
        return farthest;
    }
   private void OnDrawGizmos()
   {
      Gizmos.color = Color.red;
      Gizmos.DrawWireSphere(controladorAtaque.position, radioAtaque);
   }
}     