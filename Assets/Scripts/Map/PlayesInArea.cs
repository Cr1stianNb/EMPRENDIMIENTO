using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayesInArea : MonoBehaviour
{
    [SerializeField]
    Collider2D area;
    [SerializeField]
    Collider2D player1;
    [SerializeField]
    Collider2D player2;
    [SerializeField]
    Animator doorAnimator;
    private int PlayersInArea;
    private bool[] MarkedPlayers;
    public bool roomFinished = false;

    void Start()
    {
        PlayersInArea = 0;
        MarkedPlayers = new bool[2]{false, false};
    }
    // Update is called once per frame
    void Update()
    {
        if (!roomFinished)
        {
            if (area.IsTouching(player1) && !MarkedPlayers[0])
            {
                PlayersInArea++;
                MarkedPlayers[0] = true;
            }
            if (area.IsTouching(player2) && !MarkedPlayers[1])
            {
                PlayersInArea++;
                MarkedPlayers[1] = true;
            }

            if (!area.IsTouching(player1))
                PlayersInArea--;

            if (!area.IsTouching(player2))
                PlayersInArea--;

            if (PlayersInArea == 2)
            {
                doorAnimator.SetBool("Open", true);
            }
        }
    }
}
