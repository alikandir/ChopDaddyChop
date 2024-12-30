using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TownCookingLogic : MonoBehaviour
{
    [SerializeField] private TownInventorySO _townInventory;
    [SerializeField] private PlayerInventorySO _playerInventory;
    [SerializeField] private float _townHunger;
    [SerializeField] private float _maxTownHunger;
    [SerializeField] private float _townHungerDailyChange;
    private GameStateManager gameStateManager;
    [SerializeField] private TextMeshProUGUI _onionAmount;
    [SerializeField] private TextMeshProUGUI _carrotAmount;
    [SerializeField] private TextMeshProUGUI _cabbageAmount;
    [SerializeField] private TextMeshProUGUI _currentDayText;
    [SerializeField] private Slider _townHungerSlider;
    
    private void Start() {
        gameStateManager = GameStateManager.instance;
        _townHunger=gameStateManager.GetTownHunger();
        UpdateTownInventoryUI();
        _townHunger-=_townHungerDailyChange;
        gameStateManager.PassDay();
        _currentDayText.text = "Day "+gameStateManager._daysPassed.ToString();
        UpdateTownHungerUI();
        if (StaminaBarManager.instance.GetStamina()>0){
            AddPlayerInventoryToTownInventory();
        }
        else _playerInventory.ResetInventory();
        
        StaminaBarManager.instance.ResetStamina();

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
    // }
    public void OnCookButtonPressed(RecipesSO recipe)
    {   
        bool allItemsAvailable=false;
        foreach (var element in recipe.recipe){
            Vegetable.VegetableType type = element.Key; 
            int requiredQuantity = element.Value;
            if(_townInventory.GetQuantity(type)<requiredQuantity){
                return;
            }
        }
        allItemsAvailable=true;
        if(allItemsAvailable){
            foreach(var element in recipe.recipe){
                Vegetable.VegetableType type = element.Key; 
                int requiredQuantity = element.Value;
                _townInventory.RemoveItem(type,requiredQuantity);
                
            }
            if (recipe.isUnlocked==false){
                recipe.isUnlocked=true;
                UpdateTownInventoryUI();
                return;
            }
            _townHunger+=recipe.HungerHeal;
            UpdateTownHungerUI();
            UpdateTownInventoryUI();
        }
    }

    public void OnGoExpeditionButtonPressed(){
        GameStateManager.instance.SetGameState(GameStateManager.GameState.InEncounter);
        SceneManager.LoadScene("BattleEncounterScene");
        
    }
    private void UpdateTownInventoryUI(){
        _onionAmount.text = _townInventory.GetQuantity(Vegetable.VegetableType.Onion).ToString();
        _carrotAmount.text = _townInventory.GetQuantity(Vegetable.VegetableType.Carrot).ToString();
        _cabbageAmount.text = _townInventory.GetQuantity(Vegetable.VegetableType.Cabbage).ToString();
        
    }
    private void UpdateTownHungerUI(){
        _townHungerSlider.value = _townHunger/_maxTownHunger;
    }

    public void AddPlayerInventoryToTownInventory()
    {
        
        foreach (var item in _playerInventory.GetInventory())
        {
            _townInventory.AddItem(item.Key, item.Value);
            
        }
        _playerInventory.ResetInventory();
        UpdateTownInventoryUI();
    }
}
