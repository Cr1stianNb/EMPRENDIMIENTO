using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public GameObject HUD;
    [SerializeField] GameObject current;
    private float enemyHealth = 0f;
    [SerializeField] private float maxHealth = 20f;
    [SerializeField] private bool boss = false;
    [SerializeField] public float speed;
    [SerializeField] public float stunnedSpeed;
    public bool stunned;
    public bool attacked;
    private NavMeshAgent agent;
    [SerializeField] AudioClip[] sfx;
    private AudioController controller;
    private int randomSfx;
    [SerializeField] public float stun = 3f;
    // Start is called before the first frame update
    void Start()
    {
        controller = FindObjectOfType<AudioController>();
        agent = GetComponent<NavMeshAgent>();
        enemyHealth = maxHealth;
        HUD.GetComponent<HealthEnemy>().SetMaxHealth(enemyHealth);
        attacked = false;
        stunned = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(attacked)
        {
            if(!boss)
            {
                if(!current.GetComponent<AgentScript>().target.GetComponent<PlayerController>().attacking)
                {
                    attacked = false;
                }
            }
            if(boss)
            {
                if(!current.GetComponent<Jefe>().jugador.GetComponent<PlayerController>().attacking)
                {
                    attacked = false;
                }
            }
        }
    }

    public void UpdateHealth(float mod){
        if(attacked == false)
        {
            randomSfx = Random.Range(0, 3);
            controller.PlaySfx(sfx[randomSfx]);
            enemyHealth += mod;
            attacked = true;
            if(enemyHealth > maxHealth)
            {
                enemyHealth = maxHealth;
            }
            else if(enemyHealth <= 0f){
                enemyHealth = 0f;
                Die();
            }
            HUD.GetComponent<HealthEnemy>().SetHealth(enemyHealth);
            StartCoroutine(Stunned(stun));
        }
    }

    
    public IEnumerator Stunned(float cooldown)
    {
        if(stunned == false && enemyHealth > 0f)
        {
            agent.speed = stunnedSpeed;
            stunned = true;
            yield return new WaitForSeconds(cooldown);
            agent.speed = speed;
            stunned = false;
        }
    }

    private void Die () {
        current.GetComponent<Animator>().SetTrigger("Muerte");
        Debug.Log("Un enemigo ha muerto");
        Doors.cantEnemy -= 1;
        Debug.Log("Hay " + Doors.cantEnemy + " enemigos");
        Destroy(gameObject);
    }
}
