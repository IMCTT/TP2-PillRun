using UnityEngine;

public class Finish : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var jugador = other.GetComponent<PlayerController>();
        if (jugador == null) return;
        if (!jugador.HasStateAuthority) return;
        if (jugador.IsFinished) return;
        
        jugador.IsFinished = true;

        // le mando al GM que termino uno
        if (GameManager.Instance != null && GameManager.Instance.Runner.IsServer)
        {
            GameManager.Instance.Rpc_PlayerFinished(jugador.Runner.LocalPlayer.PlayerId);
        }
    }
}