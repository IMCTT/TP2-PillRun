using Fusion;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;

    private NetworkCharacterController NCC;

    [Networked] public int CheckpointIndex { get; set; }
    [Networked] public NetworkBool IsFinished { get; set; }

    private void Awake()
    {
        NCC = GetComponent<NetworkCharacterController>();
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData input))
        {
            Vector3 move = new Vector3(input.Direction.x, 0, input.Direction.y) * moveSpeed;
            NCC.Move(move * Runner.DeltaTime);

            if (input.Jump && NCC.Grounded)
            {
                NCC.Jump(NCC.Grounded,jumpForce);
            }
        }
    }
}