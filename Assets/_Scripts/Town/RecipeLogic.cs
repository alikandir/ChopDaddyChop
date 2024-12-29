using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RecipeLogic : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] private RecipesSO _recipe;
    private GameObject _descriptionPanel;
    private TextMeshProUGUI _dishName;
    private TextMeshProUGUI _hungerFixed;
    private TextMeshProUGUI _onionNeeded;
    private TextMeshProUGUI _carrotNeeded;
    private TextMeshProUGUI _cabbageNeeded;
    private Button _unlockButton;
    private TextMeshProUGUI _unlockButtonText;
    private void Awake() {
        _descriptionPanel = this.gameObject.transform.Find("Canvas/DescriptionPanel").gameObject;
        _dishName = _descriptionPanel.transform.Find("DishNameText").GetComponent<TextMeshProUGUI>();
        _hungerFixed = _descriptionPanel.transform.Find("HungerFixedText").GetComponent<TextMeshProUGUI>();
        _onionNeeded = _descriptionPanel.transform.Find("Ingredients/OnionAmount").GetComponent<TextMeshProUGUI>();
        _carrotNeeded = _descriptionPanel.transform.Find("Ingredients/CarrotAmount").GetComponent<TextMeshProUGUI>();
        _cabbageNeeded = _descriptionPanel.transform.Find("Ingredients/CabbageAmount").GetComponent<TextMeshProUGUI>();
        _unlockButton = _descriptionPanel.transform.Find("UnlockButton").GetComponent<Button>();
        _unlockButtonText = _unlockButton.transform.Find("UnlockButtonText").GetComponent<TextMeshProUGUI>();
    }
    private void Start() {
        _dishName.text = _recipe.RecipeName;
        _hungerFixed.text = "Hunger Heal: "+ _recipe.HungerHeal.ToString();
        _cabbageNeeded.text = "x"+ _recipe.CabbageNeeded.ToString();
        _carrotNeeded.text = "x"+ _recipe.CarrotNeeded.ToString();
        _onionNeeded.text = "x"+ _recipe.OnionNeeded.ToString();
        _unlockButtonText.text = _recipe.RecipeName.ToString() +"\n" + "Hunger Heal: "+ _recipe.HungerHeal.ToString() + "\n"+ "Unlock For: ";
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        _unlockButton.gameObject.SetActive(!_recipe.isUnlocked);
        _descriptionPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Pointer Exit");
        _descriptionPanel.SetActive(false);
    }
}
