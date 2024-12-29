using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "TownInventory", menuName = "TownInventory")]
public class TownInventorySO : ScriptableObject
{
    private Dictionary<Vegetable.VegetableType, int> inventory = new Dictionary<Vegetable.VegetableType, int>();

    public void AddItem(Vegetable.VegetableType item, int quantity)
    {
        if (inventory.ContainsKey(item))
        {
            inventory[item] += quantity;
        }
        else
        {
            inventory.Add(item, quantity);
        }
    }
    private void OnEnable() {
        ResetInventory();
    }
    public void RemoveItem(Vegetable.VegetableType item, int quantity)
    {
        if (inventory.ContainsKey(item))
        {
            inventory[item] -= quantity;
            if (inventory[item] <= 0)
            {
                inventory.Remove(item);
            }
        }
    }
    public int GetQuantity(Vegetable.VegetableType item)
    {
        if (inventory.ContainsKey(item))
        {
            return inventory[item];
        }
        
        return 0;
        
    }
    public void ResetInventory()
    {
        inventory.Clear();
    }
}
