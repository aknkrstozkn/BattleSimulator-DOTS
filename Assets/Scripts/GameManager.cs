using System;
using UnityEngine;

/// <summary>
/// Simply holds game events
/// </summary>
public class GameManager : MonoBehaviour
{
    public static event Action GameStarted;
    public static event Action GameWon;
    public static event Action GameLost;
    public static event Action GameDraw;

    public static event Action GameReloaded;

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
    
    public static void RaiseGameDraw()
    {
        GameDraw?.Invoke();
    }

    public static void RaiseGameReloaded()
    {
        GameReloaded?.Invoke();
    }

    private void OnDestroy()
    {
        GameStarted = null;
        GameWon = null;
        GameLost = null;
        GameReloaded = null;
        GameDraw = null;
    }
}
