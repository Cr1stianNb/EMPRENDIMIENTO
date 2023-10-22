using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    public GameObject current;
    public GameObject HUD;
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] AudioClip[] sfx;
    [SerializeField] AudioClip clip;
    [SerializeField] AudioController controller;
    int randomSfx;

    private void Start(){
        health = maxHealth;
        HUD.GetComponent<Health>().SetMaxHealth(health);
    }

    public void UpdateHealth(float mod){
        if(health > 0)
        {
            if (mod < 0f)
            {
                randomSfx = Random.Range(0, 3);
                controller.PlaySfx(sfx[randomSfx]);
            }
            if(mod > 0f)
            {
                controller.PlaySfx(clip);
            }
            health += mod;

            if(health > maxHealth){
                health = maxHealth;
            }
            else if(health <= 0f){
                health = 0f;
                current.GetComponent<PlayerController>().Die();
            }
            HUD.GetComponent<Health>().SetHealth(health);
        }
    }
}
