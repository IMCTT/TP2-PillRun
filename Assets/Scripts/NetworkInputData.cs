using Fusion;
using UnityEngine;

// aca guardo los inputs
public struct NetworkInputData : INetworkInput
{
    public Vector2 Direction; 
    public NetworkBool Jump;  
}