using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Recipe",menuName ="Create New Recipe")]
public class RecipesSO : ScriptableObject
{
    public string RecipeName;
    public int OnionNeeded;
    public int CarrotNeeded;
    public int CabbageNeeded;
    public int HungerHeal;
    public bool isUnlocked;

    public Dictionary<Vegetable.VegetableType,int> recipe= new Dictionary<Vegetable.VegetableType, int>();
    public void OnEnable(){
        recipe.Add(Vegetable.VegetableType.Onion,OnionNeeded);
        recipe.Add(Vegetable.VegetableType.Carrot,CarrotNeeded);
        recipe.Add(Vegetable.VegetableType.Cabbage,CabbageNeeded);
    }
    
}
