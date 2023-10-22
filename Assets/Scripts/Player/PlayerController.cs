using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {

    private Vector2 movement = Vector2.zero;
    public float characterSpeed = 3f;
    public int lastMovement;
    [SerializeField] private Animator animator;
    [SerializeField] public float damage = 5f;
    [SerializeField] public GameObject hitbox;
    [SerializeField] public GameObject player;
    [SerializeField] public GameObject deathScreen;
    [SerializeField] private AudioClip deathMusic;
    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] AudioController controller;
    [Header("Action")]
    public bool attacking;
    private bool attack = false;
    private bool drinkPotion = false;
    private bool dodge = false;

    private float dashingTime = 0.2f;
    private float dashDistance = 1500f;
    private float dashingCooldown = 1f;
    private bool canDash = true;
    private bool isDashing = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        animator.ResetTrigger("Death");
        deathScreen.SetActive(false);
    }

    void FixedUpdate(){
        attacking = hitbox.GetComponent<PlayerAttack>().attacking;
        if(drinkPotion)
        {
            player.GetComponent<PlayerInventory>().Potion();
        }
        if(dodge)
        {
            if(!isDashing)
            {
                if(lastMovement == 0) // up
                {
                    
                }
                else if(lastMovement == 1) // right
                {
                    StartCoroutine(Dash(1f));
                }
                else if(lastMovement == 2) // down
                {
                    
                }
                else if(lastMovement == 3) // left
                {
                    StartCoroutine(Dash(-1f));
                }
            }
            
        }
        if (!attacking)
        {
            rigidbody.MovePosition(GetComponent<Rigidbody2D>().position + movement.normalized * characterSpeed * Time.fixedDeltaTime);
            Movement();
            if (attack)
            {
                hitbox.GetComponent<PlayerAttack>().AttackAnimation();
            }
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        attack = context.action.triggered;
    }

    public void OnPotion(InputAction.CallbackContext context)
    {
        drinkPotion = context.action.triggered;
    }

    public void OnDodge(InputAction.CallbackContext context)
    {
        dodge = context.action.triggered;
    }

    IEnumerator Dash(float direction)
    {
        canDash = false;
        isDashing = true;
        rigidbody.velocity = new Vector2(direction * dashDistance, 0f);
        yield return new WaitForSeconds(dashingTime);
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    private void Movement()
    {
        if (movement.y > 0 && Mathf.Abs(movement.y) > Mathf.Abs(movement.x))
        {
            animator.SetBool("isWalkingDown", false);
            animator.SetBool("isWalkingUp", true);
            lastMovement = 0;
        }
        if (movement.y < 0 && Mathf.Abs(movement.y) > Mathf.Abs(movement.x))
        {
            animator.SetBool("isWalkingUp", false);
            animator.SetBool("isWalkingDown", true);
            lastMovement = 2;
        }
        if (movement.y == 0 || Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
        {
            animator.SetBool("isWalkingUp", false);
            animator.SetBool("isWalkingDown", false);
        }
        if (movement.x > 0 && Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
        {
            animator.SetBool("isWalkingRight", true);
            animator.SetBool("isWalkingLeft", false);
            lastMovement = 1;
        }
        if (movement.x < 0 && Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
        {
            animator.SetBool("isWalkingRight", false);
            animator.SetBool("isWalkingLeft", true);
            lastMovement = 3;
            
        }
        if (movement.x == 0 || Mathf.Abs(movement.y) > Mathf.Abs(movement.x))
        {
            animator.SetBool("isWalkingRight", false);
            animator.SetBool("isWalkingLeft", false);
        }
    }

    public void Die()
    {
        animator.SetTrigger("Death");
        Time.timeScale = 0.5f;
        AudioListener.pause = true;
        hitbox.GetComponent<PlayerAttack>().attacking = true;
    }

    public void DeathScreen()
    {
        Time.timeScale = 0.0f;
        deathScreen.SetActive(true);
        AudioSource audio = GameObject.Find("BackgroundMusic").GetComponent<AudioSource>();
        audio.clip = deathMusic;
        audio.Play();
        AudioListener.pause = false;
    }
}