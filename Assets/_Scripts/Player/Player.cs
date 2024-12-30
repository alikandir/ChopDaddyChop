using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    [SerializeField] private float _damage;
    public float GetPlayerDamage(){return _damage;}
    
    private GameStateManager gameStateManager;
    [SerializeField] private PlayerInventorySO playerInventory;
    [SerializeField] private TownInventorySO townInventory;
    
    private void Awake()
    {
        gameStateManager = GameStateManager.instance;
    }
    private void Start() {
        
    }
    // Didn't use it at the end.
    // private void OnEnable()
    // {
    //     gameStateManager.OnGameStateChanged += OnGameStateChanged;
    // }
    // private void OnDisable()
    // {
    //     gameStateManager.OnGameStateChanged -= OnGameStateChanged;
    // }
    // private void OnGameStateChanged(GameStateManager.GameState state)
    // {
    //     switch (state)
    //     {
    //         case GameStateManager.GameState.InEncounter:
    //             Debug.Log("Player is in encounter");
    //             break;
    //         case GameStateManager.GameState.InTown:
                
                
    //             break;
    //         case GameStateManager.GameState.MainMenu:
    //             Debug.Log("Player is in main menu");
    //             break;
    //         case GameStateManager.GameState.GameOver:
    //             Debug.Log("Player is in game over");
    //             break;
    //     }
    // }
    
    public void TakeDamage(float _incomingDamage){
        StaminaBarManager.instance.DecreaseStamina(_incomingDamage);
       
    }
    public void AddItemToInventory(Vegetable.VegetableType type, int _rewardQuantity){
        playerInventory.AddItem(type, _rewardQuantity);
        
    }
    
    
}
