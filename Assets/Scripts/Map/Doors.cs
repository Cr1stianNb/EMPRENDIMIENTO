using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
     [SerializeField] public Animator animator;
     [SerializeField] private GameObject collider;
     [SerializeField] private GameObject spawner;
     [SerializeField] private GameObject items;
     [SerializeField] Collider2D area;
     [SerializeField] private bool spawn;
     public bool roomFinished = false;
     [Header("BossFight")]
     [SerializeField] private bool bossFight;
     [SerializeField] private AudioClip bossMusic;
     private bool coop;
     public static int cantEnemy;
     private int PlayersInArea;
     private bool[] MarkedPlayers = new bool[2]{false, false};
     [Header("Players")]
     public static Collider2D player1;
     public static Collider2D player2;
     [SerializeField] AudioClip[] sfx;
     private AudioController controller;



    void Start()
    {
          controller = FindObjectOfType<AudioController>();
          cantEnemy = 0;
          coop = MainMenu.coop;
          if(items != null)
          {
               items.SetActive(false);
          } 
    }

    void Update()
    {
          if(cantEnemy == 0)
          {
              animator.SetBool("Open", true);
              collider.GetComponent<BoxCollider2D>().enabled = false;
          }
          else
          {
               animator.SetBool("Open", false);
               collider.GetComponent<BoxCollider2D>().enabled = true;
          }
          if(spawn)
          {
               if(spawner.activeSelf && cantEnemy == 0)
               {
                    roomFinished = true;
               }
               if(!roomFinished && cantEnemy == 0)
               {
                    if(!coop)
                    {
                         OnePlayerDoors();
                    }
                    else if(coop)
                    {
                         CoopDoors();
                    }
               }
               if(roomFinished)
               {
                    if(items != null)
                    {
                         items.SetActive(true);
                    }    
               }
          }
    }
    private void OnePlayerDoors()
    {
          if(area.IsTouching(player1))
          {
               spawner.SetActive(true);
               if(bossFight)
               {
                    AudioSource audio = GameObject.Find("BackgroundMusic").GetComponent<AudioSource>();
                    audio.clip = bossMusic;
                    audio.Play();
               }
          }
    }

    private void CoopDoors()
    {
          if (area.IsTouching(player1) && !MarkedPlayers[0])
          {
               MarkedPlayers[0] = true;
          }
          if (area.IsTouching(player2) && !MarkedPlayers[1])
          {
               MarkedPlayers[1] = true;
          }

          if (!area.IsTouching(player1))
          {
               MarkedPlayers[0] = false;
          }
          if (!area.IsTouching(player2))
          {
               MarkedPlayers[1] = false;
          }
          if (MarkedPlayers[0] && MarkedPlayers[1])
          {
               spawner.SetActive(true);
               if(bossFight)
               {
                    AudioSource audio = GameObject.Find("BackgroundMusic").GetComponent<AudioSource>();
                    audio.clip = bossMusic;
                    audio.Play();
                    audio.UnPause();
               }
          }
     }

    private void OpenDoorSfx()
    {
        controller.PlaySfx(sfx[0]);
    }

    private void CloseDoorSfx()
    {
        controller.PlaySfx(sfx[1]);
    }
}