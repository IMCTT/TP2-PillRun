using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // UI
    public TMP_Text timer;
    public TMP_Text fase;
    public TMP_Text ganador;
    public Button startButton;

    private void Start()
    {
        
        startButton.onClick.AddListener(OnBotonIniciarPresionado);
        ganador.gameObject.SetActive(false);
        startButton.gameObject.SetActive(false);
        
    }

    private void OnBotonIniciarPresionado()
    {
        // solo funciona con el host
        if (GameManager.Instance != null && GameManager.Instance.Runner.IsServer)
        {
            GameManager.Instance.Rpc_StartGame();
        }
    }

    private void Update()
    {
        if (GameManager.Instance == null) return;

        // solo le aparece el boton al host
        bool soyHost = GameManager.Instance.Runner.IsServer;
        startButton.gameObject.SetActive(soyHost);

        // falta hacer el tiempo
        float tiempo = GameManager.Instance.RemainingTime;
     

        // estados de partida
        switch (GameManager.Instance.GamePhase)
        {
            case 0:
                fase.text = "Waiting";
                ganador.gameObject.SetActive(false);
                break;
            case 1:
                fase.text = "In game";
                ganador.gameObject.SetActive(false);
                break;
            case 2:
                fase.text = "Game Finished";
                ganador.gameObject.SetActive(true);
                ganador.text = "Winner: Player " + GameManager.Instance.WinnerPlayerId;
                break;
        }
    }
}