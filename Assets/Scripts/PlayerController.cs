using Fusion;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    
    
    public float velocidad = 5f;
    
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
        Debug.Log("Spawned! HasInputAuthority: " + HasInputAuthority);
    
        if (HasInputAuthority)
        {
            var inputHandler = GetComponent<InputHandler>();
            Debug.Log("InputHandler encontrado: " + (inputHandler != null));
            if (inputHandler != null)
                Runner.AddCallbacks(inputHandler);
        }
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData input))
        {
            Vector3 movimiento = new Vector3(input.Direction.x, 0, input.Direction.y) * velocidad;
            ncc.Move(movimiento * Runner.DeltaTime);

            if (input.Jump && ncc.Grounded)
            {
                ncc.Velocity = new Vector3(ncc.Velocity.x, jumpforce, ncc.Velocity.z);
            }
        }
    }
}