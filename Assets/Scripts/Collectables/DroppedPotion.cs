using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DroppedPotion : MonoBehaviour
{
    public TMP_Text potionHUD;
    public int potion;

    /*private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Destroy(gameObject);
            PlayerInventory.PlayerPotions += potion;
        }
    }*/
}
