using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{

    public GameObject Player;
    public float potions;
    public float positionPlayer1X;
    public float positionPlayer1Y;

    void Update(){

        potions = Player.GetComponent<PlayerInventory>().PlayerPotions;
        positionPlayer1X = Player.GetComponent<Transform>().position.x;
        positionPlayer1Y = Player.GetComponent<Transform>().position.y;

    }

}
