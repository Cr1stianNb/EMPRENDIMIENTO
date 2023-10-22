using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    public float PlayerBank;
    public float PlayerPotions;
    public TMP_Text potionHUD;
    public GameObject player;
    [SerializeField] GameObject TimeOut;
    private bool drankPotion = false;

    private string potionPref;
    private string moneyPref;

    void Start()
    {
        PlayerPotions = 0;
        PlayerBank = 0;

        potionHUD.text = "Potions: " + PlayerPotions;
    }
    
    public void Potion()
    {
        if(PlayerPotions > 0 && !drankPotion)
        {
            player.GetComponent<PlayerHealth>().UpdateHealth(20);
            PlayerPotions--;
            potionHUD.text = "Potions: " + PlayerPotions;
            StartCoroutine(CooldownPotion());
        }  
    }

    private IEnumerator CooldownPotion()
    {
        drankPotion = true;
        TimeOut.SetActive(true);
        yield return new WaitForSeconds(2);
        TimeOut.SetActive(false);
        drankPotion = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Potion"))
        {
            Destroy(other.gameObject);
            PlayerPotions += 1;
            potionHUD.text = "Potions: " + PlayerPotions;
        }
    }

    public void OverrideInventory(float potions, float money){

        PlayerPotions = potions;
        PlayerBank = money;

    }

}
