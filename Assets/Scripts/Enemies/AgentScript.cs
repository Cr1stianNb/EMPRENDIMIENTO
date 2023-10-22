using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentScript : MonoBehaviour
{
    public GameObject target;
    [SerializeField] GameObject current;
    [SerializeField] public float enemyCooldown = 5f;

    [SerializeField] public float damage = 5f;

    private NavMeshAgent agent;
    [SerializeField] private Animator animator;
    public GameObject hitbox;
    Vector3 enemyToPlayer;

    [SerializeField] AudioClip[] sfx;
    private AudioController controller;
    private int randomSfx;

    // Start is called before the first frame update
    void Start()
    {
        controller = FindObjectOfType<AudioController>();
        target = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        Doors.cantEnemy += 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(hitbox.GetComponent<EnemyAttack>().attacking == false)
        {
            Attack();
        }
        
        AnimationAndMovement();
        if(current.GetComponent<EnemyHealth>().attacked)
        {
            animator.SetBool("isAttacking", false);
            hitbox.GetComponent<EnemyAttack>().attacking = false;
        }
        
        
    }

    public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Player");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
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

    private void Attack()
    {
        
        if (hitbox.GetComponent<EnemyAttack>().playerInRange == true && hitbox.GetComponent<EnemyAttack>().canAttack == true)
        {
            hitbox.GetComponent<EnemyAttack>().AttackAnimation();
        }
    }

    public void DealDamage()
    {
        randomSfx = Random.Range(0, 2);
        controller.PlaySfx(sfx[randomSfx]);
        hitbox.GetComponent<EnemyAttack>().DealDamage();
    }

    public void EndAnimation()
    {
        hitbox.GetComponent<EnemyAttack>().EndAnimation();
    }

    private void AnimationAndMovement()
    {
        target = FindClosestEnemy();
        agent.SetDestination(target.transform.position);
        enemyToPlayer = target.transform.position - current.transform.position;
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

}
