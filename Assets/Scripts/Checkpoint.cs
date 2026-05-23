using UnityEngine;

public class Checkpoint : MonoBehaviour
{
   
    public int numero;

    private void OnTriggerEnter(Collider other)
    {
        var jugador = other.GetComponent<PlayerController>();
        if (jugador == null) return;
        
       
        if (!jugador.HasStateAuthority) return;
        
        if (jugador.CheckpointIndex < numero)
        {
            jugador.CheckpointIndex = numero;
            jugador.ultimaPosicionCheckpoint = transform.position + Vector3.up * 1f;
            Debug.Log("Checkpoint " + numero + " alcanzado!");
        }
      

      
    }
}