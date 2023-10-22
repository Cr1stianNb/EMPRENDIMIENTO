using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Jefe:MonoBehaviour
{
   private Animator animator;
   public GameObject jugador;
   public GameObject farthest;
   private bool mirandoDerecha=true;
   public NavMeshAgent agent;
   public GameObject boss;
   private AudioController controller;
   [Header("Ataque")]
   [SerializeField] private Transform controladorAtaque;
   [SerializeField] private float radioAtaque;
   [SerializeField] private float danoAtaque;
   [SerializeField] private AudioClip attack;

   private void Start()
   {
        controller = FindObjectOfType<AudioController>();
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
    }

    public void MirarJugador()
    {
        if((jugador.transform.position.x > transform.position.x && mirandoDerecha) || (jugador.transform.position.x < transform.position.x && !mirandoDerecha))
        {
                mirandoDerecha = !mirandoDerecha;
                transform.eulerAngles= new Vector3(0, transform.eulerAngles.y + 180, 0);
        }
    }

    public void OnShield()
    {
        boss.GetComponent<EnemyHealth>().enabled = false;
    }

    public void OffShield()
    {
        boss.GetComponent<EnemyHealth>().enabled = true;
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

    public void Sound()
    {
        controller.PlaySfx(attack);
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