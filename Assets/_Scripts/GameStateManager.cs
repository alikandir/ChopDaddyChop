using System;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;
    public GameState currentGameState;
    public event Action<GameState> OnGameStateChanged;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public enum GameState
    {
        MainMenu,
        InEncounter,
        InTown,
        GameOver
    }
    public void SetGameState(GameState state)
    {
        currentGameState = state;
        OnGameStateChanged?.Invoke(currentGameState);
    }
}
