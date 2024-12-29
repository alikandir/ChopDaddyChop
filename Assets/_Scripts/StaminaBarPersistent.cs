using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StaminaBarManager : MonoBehaviour
{
    public static StaminaBarManager instance;
    public static float Stamina;
    public float MaxStamina;
    private Slider slider;
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
    private void Start() {
        slider=this.GetComponentInChildren<Slider>();
        ResetStamina();
    }
    public void DecreaseStamina(float amount){
        Stamina-=amount;
        UpdateStaminaUI();
        if(Stamina<=0){
            SceneManager.LoadScene("TownScene");
        }
    }
    public float GetStamina(){
        return Stamina;
    }
    public void ResetStamina(){
        Stamina=MaxStamina;
        UpdateStaminaUI();
    }
    public void UpdateStaminaUI()
    {
        slider.value = Stamina/MaxStamina;
    }
}