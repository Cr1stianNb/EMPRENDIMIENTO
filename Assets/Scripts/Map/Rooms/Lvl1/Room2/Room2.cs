using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room2 : MonoBehaviour
{
    [SerializeField] public GameObject[] enemies;
    public GameObject slime;
    private int count;
    void Start()
    {
        count = 0;
        while(count < enemies.Length)
        {
            enemies[count].SetActive(true);
            count++;
        }
    }
}
