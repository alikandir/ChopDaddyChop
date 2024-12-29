using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;
    public GameState currentGameState;
    public event Action<GameState> OnGameStateChanged;
    public int _daysPassed=0;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject); 
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public enum GameState
    {
        MainMenu,
        InEncounter,
        InTown,
        BetweenEncounter,
        GameOver
    }
    public void SetGameState(GameState state)
    {
        currentGameState = state;
        print(currentGameState);
        OnGameStateChanged?.Invoke(currentGameState);
    }
    
    
    public void PassDay(){
        _daysPassed++;
        print("Day passed" + _daysPassed.ToString());
    }

}
