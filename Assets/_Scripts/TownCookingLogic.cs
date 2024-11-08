using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownCookingLogic : MonoBehaviour
{
    [SerializeField] private TownInventorySO _townInventory;
    private GameStateManager gameStateManager;
    private void Awake()
    {
        gameStateManager = GameStateManager.instance;
    }
    private void OnEnable()
    {
        gameStateManager.OnGameStateChanged += OnGameStateChanged;
    }
    private void OnDisable()
    {
        gameStateManager.OnGameStateChanged -= OnGameStateChanged;
    }
    private void OnGameStateChanged(GameStateManager.GameState state)
    {
        if (state == GameStateManager.GameState.InTown)
        {
            
        }
    }
    
}
