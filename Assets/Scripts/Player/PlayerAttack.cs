using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] public GameObject player;
    public bool attacking;
    public bool canAttack = true;
    [SerializeField] private BoxCollider2D hitbox;
    private float playerAnimation;
    [SerializeField] private float cooldown = 0.8f;
    [SerializeField] private Animator animator;
    [SerializeField] private Animator weapons;
    [SerializeField] AudioClip[] sfx;
    [SerializeField] AudioController controller;
    int randomSfx;

    // Start is called before the first frame update
    void Start()
    {
        attacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AttackAnimation()
    {
        if(canAttack)
        {
            randomSfx = Random.Range(0, 3);
            controller.PlaySfx(sfx[randomSfx]);
            attacking = true;
            hitbox.enabled = true;
            animator.SetInteger("Direction", player.GetComponent<PlayerController>().lastMovement);
            weapons.SetInteger("Direction", player.GetComponent<PlayerController>().lastMovement);
            animator.SetBool("isAttacking", true);
            weapons.SetBool("isAttacking", true);
        }
    }

    public void EndAnimation()
    {
        animator.SetBool("isAttacking", false);
        weapons.SetBool("isAttacking", false);
        attacking = false;
        hitbox.enabled = false;
        StartCoroutine(AttackCooldown(cooldown));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy" && attacking == true)
        {
            other.GetComponent<EnemyHealth>().UpdateHealth(-player.GetComponent<PlayerController>().damage);
        }
    }

    public IEnumerator AttackCooldown(float cooldown)
    {
        canAttack = false;
        yield return new WaitForSeconds(cooldown);
        canAttack = true;
    }

}
