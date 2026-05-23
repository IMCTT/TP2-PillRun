using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
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
        if (GameManager.Instance != null && GameManager.Instance.Runner.IsServer)
        {
            GameManager.Instance.Rpc_StartGame();
        }
    }

    private void Update()
    {
        if (GameManager.Instance == null) return;

        // solo le aparece el boton al host cuando estamos en el lobby
        bool soyHost = GameManager.Instance.Runner.IsServer;
        startButton.gameObject.SetActive(soyHost && GameManager.Instance.GamePhase == 0);

        
        float tiempo = GameManager.Instance.RemainingTime;
        int minutos = Mathf.FloorToInt(tiempo / 60f);
        int segundos = Mathf.FloorToInt(tiempo % 60f);
        timer.text = minutos + ":" + segundos.ToString("00");

       
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