using Fusion;
using UnityEngine;

public class Camara : NetworkBehaviour
{
    // camara
    public Vector3 offset = new Vector3(0f, 8f, -6f);
    public float velocidad = 5f;

    private Camera camara;

  
    public override void Spawned()
    {
        
        if (HasInputAuthority)
        {
            camara = Camera.main;
        }
    }

    private void Update()
    {
        
        if (!HasInputAuthority) return;
        if (camara == null) return;
        Vector3 posPlayer = transform.position + offset;
        camara.transform.position = Vector3.Lerp(camara.transform.position, posPlayer, velocidad * Time.deltaTime);
        camara.transform.LookAt(transform.position);
    }
}