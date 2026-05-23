using UnityEngine;

public class Respawn : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var jugador = other.GetComponent<PlayerController>();
        if (jugador == null) return;
        if (!jugador.HasStateAuthority) return;

        jugador.Respawn();
    }
}