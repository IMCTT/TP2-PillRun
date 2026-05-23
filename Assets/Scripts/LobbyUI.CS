using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LobbyUI : MonoBehaviour, INetworkRunnerCallbacks
{
    // referencias a los objetos de la UI, los asigno desde el inspector
    public TMP_InputField user;
    public Button playButton;

    public TMP_Text title;
    public NetworkObject pill;
    
    public NetworkObject gameManager;
    private NetworkRunner runner;
    
    private void Awake()
    {
        runner = GetComponent<NetworkRunner>();
        if (runner == null)
            runner = gameObject.AddComponent<NetworkRunner>();
    
        
       
    }

    private void Start()
    {
        playButton.onClick.AddListener(OnPlayButtonPressed);
    }

    private async void OnPlayButtonPressed()
    {
        
        playButton.interactable = false;
        

        runner.ProvideInput = true;

        // agarra la scene 1
        var escena = SceneRef.FromIndex(1);
    
        var infoEscena = new NetworkSceneInfo();
        infoEscena.AddSceneRef(escena);

        // si no hay un host el primero que entra hace de host
        await runner.StartGame(new StartGameArgs
        {
            GameMode = GameMode.AutoHostOrClient, 
            SessionName = "PillRunRoom", 
            Scene = infoEscena,
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        });
    }

    // Fusion llama este metodo cuando un jugador se une a la partida
    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        
        if (runner.IsServer)
        {
            
            if (GameManager.Instance == null)
            {
                runner.Spawn(gameManager, Vector3.zero, Quaternion.identity);
            }

            
            Vector3 spawner = new Vector3(Random.Range(-1f, 1f), 1f, 0f);
            runner.Spawn(pill, spawner, Quaternion.identity, player);
        }
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        // aca tengo que sacar al jugador
    }

    // callbacks obligatorios vacios
    public void OnInput(NetworkRunner runner, NetworkInput input) { }
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
    public void OnShutdown(NetworkRunner runner, ShutdownReason reason) { }
    public void OnConnectedToServer(NetworkRunner runner) { }
    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason) { }
    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, System.ArraySegment<byte> data) { }
    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress) { }
    public void OnSceneLoadDone(NetworkRunner runner) { }
    public void OnSceneLoadStart(NetworkRunner runner) { }
    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
}