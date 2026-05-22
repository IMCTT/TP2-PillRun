using Fusion;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    
    
    public float velocidad = 6f;
    
    public float jumpforce = 5f;

    
    private NetworkCharacterController ncc;

    // cosas de fusion
    [Networked] public int CheckpointIndex { get; set; }
    [Networked] public NetworkBool IsFinished { get; set; }

  
    private void Awake()
    {
        
        ncc = GetComponent<NetworkCharacterController>();
    }
    
    public override void Spawned()
    {
        if (HasInputAuthority)
        {
            var inputHandler = GetComponent<InputHandler>();
            if (inputHandler != null)
                Runner.AddCallbacks(inputHandler);
        }
    }

    public override void FixedUpdateNetwork()
    {
        if (GameManager.Instance == null || GameManager.Instance.GamePhase != 1) return;
        
        if (GetInput(out NetworkInputData input))
        {
            Vector3 movimiento = new Vector3(input.Direction.x, 0, input.Direction.y);
            ncc.Move(movimiento);

            if (input.Jump && ncc.Grounded)
            {
                ncc.Jump();
            }
        }
    }
    public override void Render()
    {
        // la interpolacion visual la maneja el NCC automaticamente
    }
}