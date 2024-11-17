using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "TownInventory", menuName = "TownInventory")]
public class TownInventorySO : ScriptableObject
{
    private Dictionary<String, int> inventory = new Dictionary<String, int>();

    public void AddItem(String item, int quantity)
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
    public void RemoveItem(String item, int quantity)
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
    public int GetQuantity(String item)
    {
        if (inventory.ContainsKey(item))
        {
            return inventory[item];
        }
        Debug.Log("Item not found in inventory");
        return 0;
        
    }
    public void ResetInventory()
    {
        inventory.Clear();
    }
}
