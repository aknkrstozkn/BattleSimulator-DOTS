using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static event Action GameStarted;
    public static event Action GameWon;
    public static event Action GameLost;

    public static void RaiseGameStarted()
    {
        GameStarted?.Invoke();
    }

    public static void RaiseGameWon()
    {
        GameWon?.Invoke();
    }

    public static void RaiseGameLost()
    {
        GameLost?.Invoke();
    }

    public static void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    private void OnDestroy()
    {
        GameStarted = null;
        GameWon = null;
        GameLost = null;
    }
}
