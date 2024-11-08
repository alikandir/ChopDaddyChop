using UnityEngine;


public class Player : MonoBehaviour
{
    private GameStateManager gameStateManager;
    [SerializeField] private PlayerInventorySO playerInventory;
    [SerializeField] private TownInventorySO townInventory;
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
        switch (state)
        {
            case GameStateManager.GameState.InEncounter:
                Debug.Log("Player is in encounter");
                break;
            case GameStateManager.GameState.InTown:
                AddPlayerInventoryToTownInventory();
                break;
            case GameStateManager.GameState.MainMenu:
                Debug.Log("Player is in main menu");
                break;
            case GameStateManager.GameState.GameOver:
                Debug.Log("Player is in game over");
                break;
        }
    }
    public void AddPlayerInventoryToTownInventory()
    {
        foreach (var item in playerInventory.GetInventory())
        {
            townInventory.AddItem(item.Key, item.Value);
        }
        playerInventory.ResetInventory();
    }
    
}
