using Fusion;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    public static GameManager Instance { get; private set; }

    public float timerPartida = 120f;

    [Networked] public float RemainingTime { get; set; }
    [Networked] public int GamePhase { get; set; }
    [Networked] public int WinnerPlayerId { get; set; }

    public override void Spawned()
    {
        Instance = this;

        if (Runner.IsServer)
        {
            RemainingTime = timerPartida;
            GamePhase = 0;
            WinnerPlayerId = -1;
        }
    }

    public override void FixedUpdateNetwork()
    {
        if (!Runner.IsServer) return;
        if (GamePhase != 1) return;

        RemainingTime -= Runner.DeltaTime;

        if (RemainingTime <= 0)
        {
            RemainingTime = 0;
            TimeIsUp();
        }
    }

    private void TimeIsUp()
    {
        int maxCheckpoints = -1;
        int ganadorId = -1;

        foreach (var player in Runner.ActivePlayers)
        {
            if (Runner.TryGetPlayerObject(player, out var obj))
            {
                var controller = obj.GetComponent<PlayerController>();
                if (controller != null && controller.CheckpointIndex > maxCheckpoints)
                {
                    maxCheckpoints = controller.CheckpointIndex;
                    ganadorId = player.PlayerId;
                }
            }
        }

        Rpc_GameOver(ganadorId);
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void Rpc_StartGame()
    {
        GamePhase = 1;
        Debug.Log("Start!");
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void Rpc_PlayerFinished(int jugadorId)
    {
        WinnerPlayerId = jugadorId;
        GamePhase = 2;
        Debug.Log("Finish!");
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void Rpc_GameOver(int ganadorId)
    {
        WinnerPlayerId = ganadorId;
        GamePhase = 2;
        Debug.Log("Time is Up!");
    }
}