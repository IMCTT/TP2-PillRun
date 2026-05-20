using UnityEngine;

public class Checkpoint : MonoBehaviour
{
   
    public int numero;

    private void OnTriggerEnter(Collider other)
    {
        var jugador = other.GetComponent<PlayerController>();
        if (jugador == null) return;
       
        if (!jugador.HasStateAuthority) return;
        Debug.Log("Checkpoint " + numero + " alcanzado!");

      
    }
}