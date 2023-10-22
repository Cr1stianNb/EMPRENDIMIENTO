using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabilidadJefe:MonoBehaviour{
    [SerializeField] private float dano;
    [SerializeField] private Vector2 dimensionesCaja;
    [SerializeField] private Transform posicionCaja;
    [SerializeField] private float tiempoDeVida;
    private AudioController controller;
    [SerializeField] private AudioClip abilities;
    private void Start()
    {
      controller = FindObjectOfType<AudioController>();
      controller.PlaySfx(abilities);
      Destroy(gameObject,tiempoDeVida);
    }
     public void Golpe()
     {
        Collider2D[] objetos = Physics2D.OverlapBoxAll(posicionCaja.position, dimensionesCaja, 0f);

        foreach (Collider2D colisiones in objetos)
        {
            if(colisiones.CompareTag("Player"))
            {
                colisiones.GetComponent<PlayerHealth>().UpdateHealth(-dano);
            }
        }
     }

     public void Sound()
     {
        controller.PlaySfx(abilities);
     }

   private void OnDrawGizmos()
   {
      Gizmos.color = Color.yellow;
      Gizmos.DrawWireCube(posicionCaja.position, dimensionesCaja);
   }
}
