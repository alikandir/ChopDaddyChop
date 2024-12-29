using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PlayerInventory", menuName = "New PlayerInventory")]
public class PlayerInventorySO : ScriptableObject
{
    private Dictionary<Vegetable.VegetableType, int> inventory = new Dictionary<Vegetable.VegetableType, int>();
    public Dictionary<Vegetable.VegetableType,int> GetInventory()
    {
        return inventory;
    }
    private void OnEnable() {
        ResetInventory();
    }

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
        Debug.Log("Item is not in inventory");
        return 0;
        
    }
    public void ResetInventory()
    {
        inventory.Clear();
    }
}
