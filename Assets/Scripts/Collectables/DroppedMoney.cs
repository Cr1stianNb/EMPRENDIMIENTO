using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedMoney : MonoBehaviour
{
    [SerializeField] private GameObject gameObject;
    public float money;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")){

            if(other.CompareTag("Player")){
            GameObject[] players;
            players = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject player in players){
            
                player.GetComponent<PlayerInventory>().PlayerBank += money;
            
                Destroy(gameObject);
                }
            }

        }
    }
}

