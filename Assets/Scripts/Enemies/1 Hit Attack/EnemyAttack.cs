using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public bool playerInRange = false;
    public bool canAttack;
    public bool attacking = false;
    public GameObject enemy;
    private float enemyCooldown;
    private float enemyAtqAnim;
    private float damage;
    [SerializeField] private Animator animator;

    void Start()
    {
        enemyCooldown = enemy.GetComponent<AgentScript>().enemyCooldown;
        damage = enemy.GetComponent<AgentScript>().damage;
        canAttack = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) // si entra al rango de ataque, ataca
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = true;

        }
    }

    void OnTriggerExit2D(Collider2D other) // si sale del rango de ataque, no ataca
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    public IEnumerator AttackCooldown(float cooldown)
    {
        canAttack = false;
        yield return new WaitForSeconds(cooldown);
        canAttack = true;
    }

    public void AttackAnimation()
    {
        animator.SetBool("isAttacking", true);
        attacking = true;
    }

    public void EndAnimation()
    {
        animator.SetBool("isAttacking", false);
        StartCoroutine(AttackCooldown(enemyCooldown));
        attacking = false;
    }

    public void DealDamage()
    {
        if (playerInRange)
        {
            enemy.GetComponent<AgentScript>().target.GetComponent<PlayerHealth>().UpdateHealth(-damage);
        }
    }
}
