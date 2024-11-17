using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PlayerInventory", menuName = "New PlayerInventory")]
public class PlayerInventorySO : ScriptableObject
{
    private Dictionary<string, int> inventory = new Dictionary<string, int>();
    public Dictionary<string,int> GetInventory()
    {
        return inventory;
    }
    

    public void AddItem(string item, int quantity)
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
    public void RemoveItem(string item, int quantity)
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
    public int GetQuantity(string item)
    {
        if (inventory.ContainsKey(item))
        {
            return inventory[item];
        }
        Debug.Log("Item is not in inventory");
        return 0;
        
    }
    public void ResetInventory()
    {
        inventory.Clear();
    }
}
