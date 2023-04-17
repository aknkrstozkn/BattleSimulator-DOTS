using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject preGameUI;
    [SerializeField] private GameObject inGameUI;
    
    [SerializeField] private GameObject endGameUI;
    [SerializeField] private GameObject winText;
    [SerializeField] private GameObject lostText;

    private void Awake()
    {
        Init();
        SignUpEvents();
    }

    private void Init()
    {
        preGameUI.SetActive(true);
        inGameUI.SetActive(false);
        endGameUI.SetActive(false);
    }

    private void SignUpEvents()
    {
        GameManager.GameStarted += OnGameStarted;
        GameManager.GameWon += OnGameWon;
        GameManager.GameLost += OnGameLost;
    }

    private void OnGameStarted()
    {
        preGameUI.SetActive(false);
        inGameUI.SetActive(true);
    }
    
    private void OnGameWon()
    {
        endGameUI.SetActive(true);
        inGameUI.SetActive(false);
        lostText.SetActive(false);
        winText.SetActive(true);
    }

    private void OnGameLost()
    {
        endGameUI.SetActive(true);
        inGameUI.SetActive(false);
        lostText.SetActive(true);
        winText.SetActive(false);
    }

    public void OnClickStartGame()
    {
        GameManager.RaiseGameStarted();
    }

    public void OnClickRestartGame()
    {
        GameManager.RestartGame();
    }
}
